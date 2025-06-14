﻿using System;
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


        public virtual Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {

            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (int i = 0; i < sourceImage.Width; i++)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    int progress = (int)((float)i / sourceImage.Width * 100);
                    worker.ReportProgress(progress);
                }

                if (worker != null && worker.CancellationPending)
                {
                    return null;
                }

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixel(sourceImage, i, j));
                }
            }

            if (worker != null && worker.WorkerReportsProgress)
            {
                worker.ReportProgress(100); // Завершение на 100%
            }

            return resultImage;
        }

    }

    class ImageDetails
    {
        Bitmap source;
        public ImageDetails (Bitmap source)
        {
            this.source = source;
        }


        public int getMaxBrightness()
        {
            Color srsColor = source.GetPixel(0, 0);
            int res = (srsColor.R + srsColor.B + srsColor.G) / 3;


            for (int i = 0; i < source.Width; i++)
            {
                for (int j = 0; j < source.Height; j++)
                {
                    srsColor = source.GetPixel(i, j);
                    int brightSource = (srsColor.R + srsColor.B + srsColor.G) / 3;
                    if (brightSource > res) res = brightSource;
                }
            }
            
            return res;
        }

        public int getMinBrightness()
        {
            Color srsColor = source.GetPixel(0, 0);
            int res = (srsColor.R + srsColor.B + srsColor.G) / 3;


            for (int i = 0; i < source.Width; i++)
            {
                for (int j = 0; j < source.Height; j++)
                {
                    srsColor = source.GetPixel(i, j);
                    int brightSource = (srsColor.R + srsColor.B + srsColor.G) / 3;
                    if (brightSource < res) res = brightSource;
                }
            }

            return res;
        }
    }


    //класс для линейного растяжения
    class LinearExcention : Filters
    {
        //y - brightness of pixel
        int maxY = 255, minY = 0;

        public LinearExcention(int maxY, int minY)
        {
            this.maxY = maxY;
            this.minY = minY;
        }
        


        protected override Color calculateNewPixel(Bitmap source, int x, int y)
        {
            Color sourceColor = source.GetPixel(x, y);
            int Y = (sourceColor.R + sourceColor.G + sourceColor.B) / 3;
            int newBrightness = (Y - minY) * (255 / (maxY - minY));
            
            int r = Clamp(sourceColor.R + newBrightness, 0, 255);
            int g = Clamp(sourceColor.G + newBrightness, 0, 255);
            int b = Clamp(sourceColor.B + newBrightness, 0, 255);

            Color resColor = Color.FromArgb(r, g, b);
            return resColor;
            
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





    class SobelOperator : MatrixFilter
    {
        protected float[,] kernelX = null;
        protected float[,] kernelY = null;

        public SobelOperator()
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


    class SharaOperator : MatrixFilter
    {
        protected float[,] kernelX = null;
        protected float[,] kernelY = null;

        public SharaOperator()
        {
            
            kernelX = new float[,]
            {
                { 3, 0, -3 },
                { 10, 0, -10 },
                { 3, 0, -3 }
            };

            
            kernelY = new float[,]
            {
                { -3, -10, -3 },
                {  0,  0,  0 },
                {  3,  10,  3 }
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


    class PruittOperator : MatrixFilter
    {
        protected float[,] kernelX = null;
        protected float[,] kernelY = null;

        public PruittOperator()
        {

            kernelX = new float[,]
            {
                { 1, 0, -1 },
                { 1, 0, -1 },
                { 1, 0, -1 }
            };


            kernelY = new float[,]
            {
                { -1, -1, -1 },
                {  0,  0,  0 },
                {  1,  1,  1 }
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
        private MedianFilter medianFilter;
        private SobelOperator sobelFilter;
        private int intensityMultiplier;

        public GlowingEdgesFilter(int intensityMultiplier = 2)
        {
            this.medianFilter = new MedianFilter();
            this.sobelFilter = new SobelOperator();
            this.intensityMultiplier = intensityMultiplier;
        }

        protected override Color calculateNewPixel(Bitmap source, int x, int y)
        {
            // Применяем медианный фильтр
            Bitmap medianImage = medianFilter.processImage(source, null);

            // Применяем оператор Собеля для выделения краев
            Bitmap edgesImage = sobelFilter.processImage(medianImage, null);
            Color edgeColor = edgesImage.GetPixel(x, y);

            // Усиливаем яркость краев
            int intensity = (edgeColor.R + edgeColor.G + edgeColor.B) / 3;
            int enhancedIntensity = Clamp(intensity * intensityMultiplier, 0, 255);

            return Color.FromArgb(enhancedIntensity, enhancedIntensity, enhancedIntensity);
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            // Переопределяем processImage для оптимальной обработки всего изображения
            Bitmap medianImage = medianFilter.processImage(sourceImage, worker);
            if (worker != null && worker.CancellationPending) return null;

            Bitmap edgesImage = sobelFilter.processImage(medianImage, worker);
            if (worker != null && worker.CancellationPending) return null;

            Bitmap resultImage = new Bitmap(edgesImage.Width, edgesImage.Height);

            for (int i = 0; i < edgesImage.Width; i++)
            {
                if (worker != null && worker.WorkerReportsProgress)
                {
                    int progress = (int)((float)i / edgesImage.Width * 100);
                    worker.ReportProgress(progress);
                }

                if (worker != null && worker.CancellationPending)
                {
                    return null;
                }

                for (int j = 0; j < edgesImage.Height; j++)
                {
                    Color edgeColor = edgesImage.GetPixel(i, j);
                    int intensity = (edgeColor.R + edgeColor.G + edgeColor.B) / 3;
                    int enhancedIntensity = Clamp(intensity * intensityMultiplier, 0, 255);
                    resultImage.SetPixel(i, j, Color.FromArgb(enhancedIntensity, enhancedIntensity, enhancedIntensity));
                }
            }

            return resultImage;
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
                return Color.White;
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
                return Color.White;
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



    class WaveFilterVertical : Filters
    {
        
        private double amplitude;
        private double period;

        public WaveFilterVertical(double amplitude = 20, double period = 60)
        {
            
            this.amplitude = amplitude;
            this.period = period;
        }

        protected override Color calculateNewPixel(Bitmap source, int k, int l)
        {
            double newX = k;
            double newY = l;

            
            
                
                    newX = k + amplitude * Math.Sin(2 * Math.PI * l / period);
                    
                //case WaveType.Horizontal:
                //    newY = l + amplitude * Math.Sin(2 * Math.PI * k / (period / 2));
                //    break;
            

            
            int x = Clamp((int)newX, 0, source.Width - 1);
            int y = Clamp((int)newY, 0, source.Height - 1);

            return source.GetPixel(x, y);
        }
    }


    class WaveFilterHorizontal : Filters
    {
        private double amplitude;
        private double period;

        public WaveFilterHorizontal(double amplitude = 20, double period = 60)
        {

            this.amplitude = amplitude;
            this.period = period;
        }

        protected override Color calculateNewPixel(Bitmap source, int k, int l)
        {
            double newX = k;
            double newY = l;







            newY = l + amplitude * Math.Sin(2 * Math.PI * k / (period / 2));




            int x = Clamp((int)newX, 0, source.Width - 1);
            int y = Clamp((int)newY, 0, source.Height - 1);

            return source.GetPixel(x, y);
        }

    }




    class PerfectReflectorFilter : Filters
    {
        private float rMax = 0, gMax = 0, bMax = 0;
        

        protected override Color calculateNewPixel(Bitmap source, int x, int y)
        {
            CalculateMaxValues(source);

            Color sourceColor = source.GetPixel(x, y);

            // Масштабируем каналы
            int r = (int)(sourceColor.R * (255f / rMax));
            int g = (int)(sourceColor.G * (255f / gMax));
            int b = (int)(sourceColor.B * (255f / bMax));

            return Color.FromArgb(
                Clamp(r, 0, 255),
                Clamp(g, 0, 255),
                Clamp(b, 0, 255));
        }

        private void CalculateMaxValues(Bitmap source)
        {
            // Находим максимальные значения по каждому каналу
            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Color pixel = source.GetPixel(x, y);
                    if (pixel.R > rMax) rMax = pixel.R;
                    if (pixel.G > gMax) gMax = pixel.G;
                    if (pixel.B > bMax) bMax = pixel.B;
                }
            }

            // Гарантируем, что не будет деления на ноль
            if (rMax == 0) rMax = 1;
            if (gMax == 0) gMax = 1;
            if (bMax == 0) bMax = 1;
        }

        
    }




    abstract class MorphologicalFilters : Filters
    {
        protected bool[,] mask;


        public MorphologicalFilters() 
        {
            //маска 
            mask = new bool[3, 3]
                {
                    {true, true, true },
                    { true, true, true },
                    { true, true, true }
                };
        }
        

    }


    class ErosionFilter : MorphologicalFilters
    {
        protected override Color calculateNewPixel(Bitmap source, int x, int y)
        {
            int minR = 255, minG = 255, minB = 255;
            int maskWidth = mask.GetLength(0);
            int maskHeight = mask.GetLength(1);

            // Центр маски
            int centerX = maskWidth / 2;
            int centerY = maskHeight / 2;

            // Перебираем все элементы маски
            for (int i = 0; i < maskWidth; i++)
            {
                for (int j = 0; j < maskHeight; j++)
                {
                    // Если элемент маски активен
                    if (mask[i, j])
                    {
                        // Смещение относительно центра
                        int offsetX = i - centerX;
                        int offsetY = j - centerY;

                        // Координаты в исходном изображении
                        int srcX = x + offsetX;
                        int srcY = y + offsetY;

                        // Проверяем границы изображения
                        if (srcX >= 0 && srcX < source.Width &&
                            srcY >= 0 && srcY < source.Height)
                        {
                            Color pixel = source.GetPixel(srcX, srcY);
                            minR = Math.Min(minR, pixel.R);
                            minG = Math.Min(minG, pixel.G);
                            minB = Math.Min(minB, pixel.B);
                        }
                    }
                }
            }

            return Color.FromArgb(minR, minG, minB);
        }

    }


    class DilationFilter : MorphologicalFilters
    {
        protected override Color calculateNewPixel(Bitmap source, int x, int y)
        {
            int maxR = 0, maxG = 0, maxB = 0;
            int maskWidth = mask.GetLength(0);
            int maskHeight = mask.GetLength(1);

            // Центр маски
            int centerX = maskWidth / 2;
            int centerY = maskHeight / 2;

            // Перебираем все элементы маски
            for (int i = 0; i < maskWidth; i++)
            {
                for (int j = 0; j < maskHeight; j++)
                {
                    // Если элемент маски активен
                    if (mask[i, j])
                    {
                        // Смещение относительно центра
                        int offsetX = i - centerX;
                        int offsetY = j - centerY;

                        // Координаты в исходном изображении
                        int srcX = x + offsetX;
                        int srcY = y + offsetY;

                        // Проверяем границы изображения
                        if (srcX >= 0 && srcX < source.Width &&
                            srcY >= 0 && srcY < source.Height)
                        {
                            Color pixel = source.GetPixel(srcX, srcY);
                            maxR = Math.Max(maxR, pixel.R);
                            maxG = Math.Max(maxG, pixel.G);
                            maxB = Math.Max(maxB, pixel.B);
                        }
                    }
                }
            }

            return Color.FromArgb(maxR, maxG, maxB);
        }
    }


    class OpeningFilter : Filters
    {
        private bool[,] mask;

        public OpeningFilter()
        {
            // Маска по умолчанию (крест 3x3)
            mask = new bool[3, 3] {
                { false, true, false },
                { true, true, true },
                { false, true, false }
            };
        }

        

        protected override Color calculateNewPixel(Bitmap source, int x, int y)
        {
            // Для opening не используется напрямую, так как это составная операция
            return Color.Black;
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            // 1. Применяем эрозию
            ErosionFilter erosion = new ErosionFilter();
            Bitmap eroded = erosion.processImage(sourceImage, worker);

            if (worker != null && worker.CancellationPending)
                return null;

            // 2. Применяем дилатацию к результату эрозии
            DilationFilter dilation = new DilationFilter();
            return dilation.processImage(eroded, worker);
        }
    }

    // Класс для морфологического закрытия (closing = dilation -> erosion)
    class ClosingFilter : Filters
    {
        private bool[,] mask;

        public ClosingFilter()
        {
            // Маска по умолчанию (крест 3x3)
            mask = new bool[3, 3] {
                { false, true, false },
                { true, true, true },
                { false, true, false }
            };
        }

        

        protected override Color calculateNewPixel(Bitmap source, int x, int y)
        {
            // Для closing не используется напрямую, так как это составная операция
            return Color.Black;
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            // 1. Применяем дилатацию
            DilationFilter dilation = new DilationFilter();
            Bitmap dilated = dilation.processImage(sourceImage, worker);

            if (worker != null && worker.CancellationPending)
                return null;

            // 2. Применяем эрозию к результату дилатации
            ErosionFilter erosion = new ErosionFilter();
            return erosion.processImage(dilated, worker);
        }
    }




    //вычитание из исх изображения его открытие
    class TopHatFilter : Filters
    {
        private bool[,] mask; 
        private Bitmap openedImage; 

        public TopHatFilter()
        {
            
            this.mask = new bool[3, 3]
            {
                { true, true, true },
                { true, true, true },
                { true, true, true }
            };

            
        }

        //вычисление сужения и расширения
        private Bitmap ComputeOpening(Bitmap sourceImage)
        {
            ErosionFilter erosion = new ErosionFilter();
            DilationFilter dilation = new DilationFilter();
            Bitmap eroded = erosion.processImage(sourceImage, null);
            return dilation.processImage(eroded, null);
        }

        protected override Color calculateNewPixel(Bitmap source, int x, int y)
        {
            
            
            openedImage = ComputeOpening(source);
            

            Color srcColor = source.GetPixel(x, y);
            Color openedColor = openedImage.GetPixel(x, y);

            //top Hat = исходное - открытие
            int r = Clamp(srcColor.R - openedColor.R, 0, 255);
            int g = Clamp(srcColor.G - openedColor.G, 0, 255);
            int b = Clamp(srcColor.B - openedColor.B, 0, 255);



            return Color.FromArgb(r, g, b);
        }
    }

}
