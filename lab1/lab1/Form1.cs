using System.ComponentModel;
using System.Drawing.Imaging;
using System.IO;




namespace lab1
{
    public partial class Form1 : Form
    {
        Bitmap image;
        Bitmap firstImage;
        int modeCheckingImage_button2 = 0;

        public Form1()
        {
            InitializeComponent();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image files | *.png; *.jpg; *.bmp | All Files (*.*) | *.*";

                //проверка
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    image = new Bitmap(dialog.FileName);
                    firstImage = image;
                    pictureBox1.Image = image;
                    pictureBox1.Refresh();
                }
            }
        }


        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {


            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.png; *.jpg; *.bmp | All Files (*.*) | *.*";

            //проверка
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                firstImage = image;
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {


            //проверка
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Сначала откройте исходное изображение", "Изображение отсутствует",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Путь для сохранения";
            saveDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|Bitmap Image|*.bmp";
            saveDialog.FilterIndex = 1;
            saveDialog.RestoreDirectory = true;



            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    ImageFormat format = ImageFormat.Jpeg;
                    switch (Path.GetExtension(saveDialog.FileName).ToLower())
                    {
                        case ".png":
                            format = ImageFormat.Png;
                            break;
                        case ".bmp":
                            format = ImageFormat.Bmp;
                            break;
                    }

                    //сохранение изображения
                    pictureBox1.Image.Save(saveDialog.FileName, format);
                    MessageBox.Show("Нажмите ОК, чтобы продолжить", "Изображение сохранено",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void вернутьИсходноеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Сначала откройте исходное изображение", "Изображение отсутствует",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            image = firstImage;
            pictureBox1.Image = image;


        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Сначала откройте исходное изображение", "Изображение отсутствует",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            //0 - first, 1 - modificated
            if (modeCheckingImage_button2 == 0) modeCheckingImage_button2 = 1;
            else modeCheckingImage_button2 = 0;

            if (modeCheckingImage_button2 == 0)
            {
                pictureBox1.Image = firstImage;
            }
            else
            {
                pictureBox1.Image = image;
            }



        }











        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter = new InvertFilter();
            //Bitmap res = filter.processImage(image);
            //pictureBox1.Image = res;
            //pictureBox1.Refresh();

            Filters filter = new InvertFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void черноебелоеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter = new GrayScaleFilter();
            //Bitmap res = filter.processImage(image);
            //pictureBox1.Image = res;
            //pictureBox1.Refresh();

            Filters filter = new GrayScaleFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter = new SepiaFilter();
            //Bitmap res = filter.processImage(image);
            //pictureBox1.Image = res;
            //pictureBox1.Refresh();

            Filters filter = new SepiaFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter = new BlurFilter();
            //Bitmap res = filter.processImage(image);
            //pictureBox1.Image = res;
            //pictureBox1.Refresh();

            Filters filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }
        private void размытиеГауссаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter = new GaussianFilter();
            //Bitmap res = filter.processImage(image);
            //pictureBox1.Image = res;
            //pictureBox1.Refresh();

            Filters filter = new GaussianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }
        private void повышениеЯркостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter = new BrightnessFilter();
            //Bitmap res = filter.processImage(image);
            //pictureBox1.Image = res;
            //pictureBox1.Refresh();

            Filters filter = new BrightnessFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }
        private void фильтрСобеляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter = new SobelOperator();
            //Bitmap res = filter.processImage(image);
            //pictureBox1.Image = res;
            //pictureBox1.Refresh();

            Filters filter = new SobelOperator();
            backgroundWorker1.RunWorkerAsync(filter);
        }
        private void фильтрЩарраToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter = new SharaOperator();
            //Bitmap res = filter.processImage(image);
            //pictureBox1.Image = res;
            //pictureBox1.Refresh();

            Filters filter = new SharaOperator();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void фильтрПрюиттToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter = new PruittOperator();
            //Bitmap res = filter.processImage(image);
            //pictureBox1.Image = res;
            //pictureBox1.Refresh();

            Filters filter = new PruittOperator();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void повышениеРезкостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter = new SharpnessFilter();
            //Bitmap res = filter.processImage(image);
            //pictureBox1.Image = res;
            //pictureBox1.Refresh();

            Filters filter = new SharpnessFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }
        private void тиснениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter = new EmbrossFilter();
            //Bitmap res = filter.processImage(image);
            //pictureBox1.Image = res;
            //pictureBox1.Refresh();

            Filters filter = new EmbrossFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void светящиесяКраяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter = new GlowingEdgesFilter();
            //Bitmap res = filter.processImage(image);
            //pictureBox1.Image = res;
            //pictureBox1.Refresh();

            Filters filter = new GlowingEdgesFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void медианныйФильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter = new MedianFilter();
            //Bitmap res = filter.processImage(image);
            //pictureBox1.Image = res;
            //pictureBox1.Refresh();

            Filters filter = new MedianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void переносToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter = new ShiftFilter(-100, 0);
            //Bitmap res = filter.processImage(image);
            //pictureBox1.Image = res;
            //pictureBox1.Refresh();

            Filters filter = new ShiftFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void поворотToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter = new RotateFilter(sourceCenter, 45); // Угол в градусах
            //Bitmap res = filter.processImage(image);
            //pictureBox1.Image = res;
            //pictureBox1.Refresh();

            PointF sourceCenter = new PointF(image.Width / 2f, image.Height / 2f);
            Filters filter = new RotateFilter(sourceCenter, 45);
            backgroundWorker1.RunWorkerAsync(filter);
        }
        private void стеклоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter = new GlassFilter();
            //Bitmap res = filter.processImage(image);
            //pictureBox1.Image = res;
            //pictureBox1.Refresh();

            Filters filter = new GlassFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }
        private void motionBlurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter = new MotionBlurFilter(5);
            //Bitmap res = filter.processImage(image);
            //pictureBox1.Image = res;
            //pictureBox1.Refresh();

            Filters filter = new MotionBlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }
        private void волнаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Filters filter = new WaveFilter();
            //Bitmap res = filter.processImage(image);
            //pictureBox1.Image = res;
            //pictureBox1.Refresh();

            Filters filter = new WaveFilterVertical();
            backgroundWorker1.RunWorkerAsync(filter);
        }
        private void волнагоризонтальнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new WaveFilterHorizontal();
            backgroundWorker1.RunWorkerAsync(filter);
        }



        private void линейноеРастяжениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDetails imageInfo = new ImageDetails(image);
            int max = imageInfo.getMaxBrightness();
            int min = imageInfo.getMinBrightness();
            Filters filter = new LinearExcention(max, min);
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void идеальныйОтражательToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new PerfectReflectorFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }


















        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
            {
                image = newImage;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted_1(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            progressBar1.Value = 0;
        }



        private void сужениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new ErosionFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void расширениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new DilationFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void открытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new OpeningFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void закрытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new ClosingFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void topHatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new TopHatFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }
    }
}
