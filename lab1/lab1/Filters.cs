using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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


        public Bitmap processImage(Bitmap sourceImage)
        {
            Bitmap resultImage = new Bitmap (sourceImage.Width, sourceImage.Height);

            for (int i = 0; i < sourceImage.Height; i++)
            {
                for (int j = 0; j < sourceImage.Width; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixel(sourceImage, i, j));
                }
            }
            return resultImage;
        }

    }




    


    
}
