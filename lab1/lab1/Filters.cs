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


        public Bitmap processImage(Bitmap sourceImage)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (int i = 0; i < sourceImage.Width; i++)
            {

                for (int j = 0; j < sourceImage.Height; j++)
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
    class GrayScaleFilter : Filters
    {

        protected override Color calculateNewPixel(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            float intensity = 0.36f * sourceColor.R + 0.53f * sourceColor.G + 0.11f * sourceColor.B;
            Color resultColor = Color.FromArgb((int)intensity, (int)intensity, (int)intensity);

            return resultColor;
        }

    }



    class SepiaFilter : Filters
    {


        protected override Color calculateNewPixel(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            float intensity = 0.36f * sourceColor.R + 0.53f * sourceColor.G + 0.11f * sourceColor.B;
            float k = 40f;
            int rColor = Clamp((int)(intensity + 2f * k), 0, 255);
            int gColor = Clamp((int)(intensity + 0.5f * k), 0, 255);
            int bColor = Clamp((int)(intensity - 1f * k), 0, 255);
            Color resultColor = Color.FromArgb(rColor, gColor, bColor);

            return resultColor;
        }
    }



    class BrightnessFilter : Filters
    {


        protected override Color calculateNewPixel(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            int k = 30;
            Color resultColor = Color.FromArgb
                (
                    Clamp(sourceColor.R + k, 0, 255),
                    Clamp(sourceColor.G + k, 0, 255),
                    Clamp(sourceColor.B + k, 0, 255)
                );
            return resultColor;
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
                for (int j = -rad; j <= rad; j++)
                {
                    kernel[i + rad, j + rad] = (float)Math.Exp((-(i * i + j * j) / (sigma * sigma)));
                    norm += kernel[i + rad, j + rad];
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    kernel[i, j] /= norm;
                }
            }


        }
    }





    class SobelFilter : MatrixFilter
    {
        protected float[,] kernelX = null;
        protected float[,] kernelY = null;

        public SobelFilter()
        {
            // Оператор Собеля по оси X
            kernelX = new float[,]
            {
            { -1, 0, 1 },
            { -2, 0, 2 },
            { -1, 0, 1 }
            };

            // Оператор Собеля по оси Y
            kernelY = new float[,]
            {
            { -1, -2, -1 },
            {  0,  0,  0 },
            {  1,  2,  1 }
            };
        }

        protected override Color calculateNewPixel(Bitmap source, int x, int y)
        {
            int radiusX = kernelX.GetLength(0) / 2;
            int radiusY = kernelX.GetLength(1) / 2;

            float resultRX = 0, resultGX = 0, resultBX = 0;
            float resultRY = 0, resultGY = 0, resultBY = 0;

            // Применяем оба оператора
            for (int l = -radiusY; l <= radiusY; l++)
            {
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, source.Width - 1);
                    int idY = Clamp(y + l, 0, source.Height - 1);
                    Color neighborColor = source.GetPixel(idX, idY);

                    // Применяем оператор по X
                    resultRX += neighborColor.R * kernelX[k + radiusX, l + radiusY];
                    resultGX += neighborColor.G * kernelX[k + radiusX, l + radiusY];
                    resultBX += neighborColor.B * kernelX[k + radiusX, l + radiusY];

                    // Применяем оператор по Y
                    resultRY += neighborColor.R * kernelY[k + radiusX, l + radiusY];
                    resultGY += neighborColor.G * kernelY[k + radiusX, l + radiusY];
                    resultBY += neighborColor.B * kernelY[k + radiusX, l + radiusY];
                }
            }

            // Объединяем результаты (корень из суммы квадратов)
            int resultR = (int)Math.Sqrt(resultRX * resultRX + resultRY * resultRY);
            int resultG = (int)Math.Sqrt(resultGX * resultGX + resultGY * resultGY);
            int resultB = (int)Math.Sqrt(resultBX * resultBX + resultBY * resultBY);

            return Color.FromArgb(
                Clamp(resultR, 0, 255),
                Clamp(resultG, 0, 255),
                Clamp(resultB, 0, 255)
            );
        }



    }


    class SharpnessFilter : MatrixFilter
    {
        public SharpnessFilter()
        {
            // Матрица для повышения резкости
            kernel = new float[,]
            {
            {  0, -1,  0 },
            { -1,  5, -1 },
            {  0, -1,  0 }
            };
        }
    }


    class EmbrossFilter : MatrixFilter
    {



        public EmbrossFilter()
        {
            kernel = new float[,]
            {
                { 0, 1, 0 },
                { 1, 0, -1 },
                { 0, -1, 0 }
            };
        }




        protected override Color calculateNewPixel(Bitmap source, int x, int y)
        {
            int radX = kernel.GetLength(0) / 2;
            int radY = kernel.GetLength(1) / 2;

            float resR = 0;
            float resG = 0;
            float resB = 0;

            for (int l = -radY; l <= radY; l++)
            {
                for (int h = -radX; h <= radX; h++)
                {
                    int idX = Clamp(x + h, 0, source.Width - 1);
                    int idY = Clamp(y + l, 0, source.Height - 1);

                    Color neiColor = source.GetPixel(idX, idY);



                    resR += neiColor.R * kernel[h + radX, l + radY];
                    resG += neiColor.G * kernel[h + radX, l + radY];
                    resB += neiColor.B * kernel[h + radX, l + radY];

                }

            }


            //сдвиг и нормировка
            int intensity = ((int)(resR + resB + resG) / 3) + 128;
            intensity = Clamp(intensity, 0, 255);



            Color resultColor = Color.FromArgb(intensity, intensity, intensity);
            return resultColor;

        }


    }



    class MedianFilter : Filters
    {
        protected override Color calculateNewPixel(Bitmap source, int x, int y)
        {
            int radius = 1;


            //реализация через list
            List<int> rValues = new List<int>();
            List<int> gValues = new List<int>();
            List<int> bValues = new List<int>();

            for (int l = -radius; l <= radius; l++)
            {
                for (int k = -radius; k <= radius; k++)
                {
                    int idX = Clamp(x + k, 0, source.Width - 1);
                    int idY = Clamp(y + l, 0, source.Height - 1);
                    Color pixel = source.GetPixel(idX, idY);

                    rValues.Add(pixel.R);
                    gValues.Add(pixel.G);
                    bValues.Add(pixel.B);
                }
            }

            rValues.Sort();
            gValues.Sort();
            bValues.Sort();



            //вычисляем медиану по отсортированному списку
            int medianIndex = rValues.Count / 2;
            return Color.FromArgb
            (
                rValues[medianIndex],
                gValues[medianIndex],
                bValues[medianIndex]
            );
        }
    }



    class GlowingEdgesFilter : Filters
    {

        //TODO реализовать медианный фильтр
        private MedianFilter medianFilter;
        private SobelFilter sobelFilter;


        public GlowingEdgesFilter()
        {
            medianFilter = new MedianFilter();
            sobelFilter = new SobelFilter();
        }



        protected override Color calculateNewPixel(Bitmap source, int x, int y)
        {
            //сначала применяем медианный фильтр и оператор собеля
            Bitmap resMedian = medianFilter.processImage(source);
            Bitmap resSobel = sobelFilter.processImage(resMedian);
            Color edgeColor = resSobel.GetPixel(x, y);


            int intensity = (int)(edgeColor.R + edgeColor.B + edgeColor.G);
            int upIntensity = (int)(intensity * 2);


            Color resColor = Color.FromArgb
                (
                    Clamp(upIntensity, 0, 255),
                    Clamp(upIntensity, 0, 255),
                    Clamp(upIntensity, 0, 255)

                );

            return resColor;

        }

    }



    class ShiftFilter : Filters
    {
        int shiftX = 0, shiftY = 0;

        public ShiftFilter(int shiftX = 50, int shiftY = 0)
        {
            this.shiftX = shiftX;
            this.shiftY = shiftY;
        }


        protected override Color calculateNewPixel(Bitmap source, int x, int y)
        {
            int newX = 0, newY = 0;
            newX = x + shiftX;
            newY = y + shiftY;

            if (newX <= 0 || newX >= source.Width || newY <= 0 || newY >= source.Height)
            {
                return Color.Gray;
            }


            return source.GetPixel(newX, newY);
        }





    }






    class RotateFilter : Filters
    {
        private PointF center;
        private float angle;

        public RotateFilter(PointF center, float angle)
        {
            this.center = center;
            this.angle = angle * (float)(Math.PI / 180); // преобразуем градусы в радианы
        }

        protected override Color calculateNewPixel(Bitmap source, int x, int y)
        {
            // Переводим координаты в систему с началом в центре
            float xRel = x - center.X;
            float yRel = y - center.Y;

            // Применяем матрицу поворота
            float newX = xRel * (float)Math.Cos(angle) - yRel * (float)Math.Sin(angle);
            float newY = xRel * (float)Math.Sin(angle) + yRel * (float)Math.Cos(angle);

            // Переводим обратно в абсолютные координаты
            newX += center.X;
            newY += center.Y;

            // Проверяем границы изображения
            if (newX < 0 || newX >= source.Width || newY < 0 || newY >= source.Height)
            {
                return Color.Gray;
            }

            // Берем ближайший пиксель (без интерполяции)
            return source.GetPixel((int)newX, (int)newY);
        }
    }


    class GlassFilter : Filters
    {
        private int radius;
        private Random random;

        public GlassFilter(int radius = 10)
        {
            this.radius = radius;
            this.random = new Random();
        }

        protected override Color calculateNewPixel(Bitmap source, int x, int y)
        {
            // Генерируем случайные смещения в пределах [-radius/2, radius/2]
            int offsetX = (int)((random.NextDouble() - 0.5) * radius);
            int offsetY = (int)((random.NextDouble() - 0.5) * radius);

            // Вычисляем новые координаты с учетом смещения
            int newX = Clamp(x + offsetX, 0, source.Width - 1);
            int newY = Clamp(y + offsetY, 0, source.Height - 1);

            // Возвращаем цвет пикселя из новых координат
            return source.GetPixel(newX, newY);
        }
    }


    class MotionBlurFilter : MatrixFilter
    {
        public MotionBlurFilter(int n = 5)
        {
            // Создаем квадратное ядро размером n x n
            kernel = new float[n, n];

            // Заполняем главную диагональ единицами
            for (int i = 0; i < n; i++)
            {
                kernel[i, i] = 1.0f;
            }

            // Нормируем ядро (делим каждый элемент на n)
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    kernel[i, j] /= n;
                }
            }
        }
    }



    class WaveFilter : Filters
    {
        public enum WaveType { Vertical, Horizontal }
        private WaveType type;
        private double amplitude;
        private double period;

        public WaveFilter(WaveType type = WaveType.Vertical,
                         double amplitude = 20,
                         double period = 60)
        {
            this.type = type;
            this.amplitude = amplitude;
            this.period = period;
        }

        protected override Color calculateNewPixel(Bitmap source, int k, int l)
        {
            double newX = k;
            double newY = l;

            // Применяем соответствующее волновое преобразование
            switch (type)
            {
                case WaveType.Vertical:
                    newX = k + amplitude * Math.Sin(2 * Math.PI * l / period);
                    break;
                case WaveType.Horizontal:
                    newY = l + amplitude * Math.Sin(2 * Math.PI * k / (period / 2));
                    break;
            }

            // Ограничиваем координаты размерами изображения
            int x = Clamp((int)newX, 0, source.Width - 1);
            int y = Clamp((int)newY, 0, source.Height - 1);

            return source.GetPixel(x, y);
        }
    }

}
