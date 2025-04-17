using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Claims;
using System.ComponentModel;






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

            for (int i = 0; i < sourceImage.Height; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending) return null;
                for (int j = 0; j < sourceImage.Width; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixel(sourceImage, i, j));
                }
            }
            return resultImage;
        }

    }





    class InvertFilter : Filters 
        {

        //made new pixel color
        protected override Color calculateNewPixel(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb(255 -  sourceColor.R, 255 - sourceColor.G, 255 - sourceColor.B);
            return resultColor;
        }


    }




    


    
}
