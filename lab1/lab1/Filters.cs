using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Claims;
using System.ComponentModel;
using System.Diagnostics.Contracts;






namespace lab1
{
    //abstract class filters
    abstract class Filters
    {


        protected abstract Color calculateNewPixel(Bitmap source, int x, int y);


        //checking over range
        public int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }


        public Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending) return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixel(sourceImage, i, j));
                }
            }
            worker.ReportProgress(100);
            return resultImage;
        }

    }





    class InvertFilter : Filters
    {

        //made new pixel color
        protected override Color calculateNewPixel(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb(255 - sourceColor.R, 255 - sourceColor.G, 255 - sourceColor.B);
            return resultColor;
        }


    }




    class MatrixFilter : Filters
    {
        protected MatrixFilter() { }
        public MatrixFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }



        protected float[,] kernel = null;
        protected override Color calculateNewPixel(Bitmap source, int x, int y)
        {
            int radX = kernel.GetLength(0) / 2;
            int radY = kernel.GetLength(1) / 2;

            float resR = 0;
            float resG = 0;
            float resB = 0;
            for (int l = -radY; l <= radY; l++)
            {
                for (int k = -radX; k <= radX; k++)
                {
                    int idX = Clamp(x + k, 0, source.Width - 1);
                    int idY = Clamp(y + l, 0, source.Height - 1);
                    Color neirColor = source.GetPixel(idX, idY);
                    resR += neirColor.R * kernel[k + radX, l + radY];
                    resG += neirColor.G * kernel[k + radX, l + radY];
                    resB += neirColor.B * kernel[k + radX, l + radY];
                }
            }




            return Color.FromArgb
                (
                    Clamp((int)resR, 0, 255),
                    Clamp((int)resG, 0, 255),
                    Clamp((int)resB, 0, 255)
                );
        }



    }




    class BlurFilter : MatrixFilter
    {
        public BlurFilter()
        {
            int sizeX = 3;
            int sizeY = 3;
            kernel = new float[sizeX, sizeY];
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    kernel[i, j] = 1.0f / (float)(sizeX * sizeY);
                }
            }

        }


    }


    class GaussianFilter : MatrixFilter
    {
        public GaussianFilter() { createGaussianKernel(3, 2); }
        
        public void createGaussianKernel(int rad, float sigma)
        {
            int size = 2 * rad + 1; //размер ядра
            kernel = new float[size, size]; //ядро фильтра
            float norm = 0; //коэф нормировки ядра

            for (int i = -rad; i <= rad; i++)
            {
                for(int j = -rad; j <= rad; j++)
                {
                    kernel[i + rad, j + rad] = (float)Math.Exp((-(i * i + j * j) / (sigma * sigma)));
                    norm += kernel[i + rad, j + rad];
                }
            }

            for (int i = 0; i < size; i++)
            { 
                for(int j = 0;j < size; j++)
                {
                    kernel[i, j] /= norm;
                }
            }
            

        }
    }






}
