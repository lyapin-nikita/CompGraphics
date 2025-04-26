using System.ComponentModel;

namespace lab1
{
    public partial class Form1 : Form
    {
        Bitmap image;

        public Form1()
        {
            InitializeComponent();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        private void îòêğûòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.png; *.jpg; *.bmp | All Files (*.*) | *.*";


            //ïğîâåğêà íà êîğğåêòíóş çàãğóçêó
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }

        }





        private void èíâåğñèÿToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new InvertFilter();
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }

        private void ÷åğíîåáåëîåToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GrayScaleFilter();
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }

        private void ñåïèÿToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SepiaFilter();
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }

        private void ğàçìûòèåToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BlurFilter();
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }
        private void ğàçìûòèåÃàóññàToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GaussianFilter();
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }
        private void ïîâûøåíèåßğêîñòèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BrightnessFilter();
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }
        private void ôèëüòğÑîáåëÿToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SobelFilter();
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }
        private void ïîâûøåíèåĞåçêîñòèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SharpnessFilter();
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }
        private void òèñíåíèåToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new EmbrossFilter();
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }

        private void ñâåòÿùèåñÿÊğàÿToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GlowingEdgesFilter();
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }

        private void ìåäèàííûéÔèëüòğToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new MedianFilter();
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }

        private void ïåğåíîñToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new ShiftFilter(-100, 0);
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }

        private void ïîâîğîòToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PointF sourceCenter = new PointF(image.Width / 2f, image.Height / 2f);
            Filters filter = new RotateFilter(sourceCenter, 45); // Óãîë â ãğàäóñàõ
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }
        private void ñòåêëîToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GlassFilter();
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }
        private void motionBlurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new MotionBlurFilter(5);
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }
        private void âîëíàToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new WaveFilter();
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }


}
