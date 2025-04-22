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


        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.png; *.jpg; *.bmp | All Files (*.*) | *.*";


            //�������� �� ���������� ��������
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }

        }





        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new InvertFilter();
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }

        private void �����������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GrayScaleFilter();
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }

        private void �����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SepiaFilter();
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }

        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BlurFilter();
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }
        private void ��������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GaussianFilter();
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }
        private void ����������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BrightnessFilter();
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }
        private void ������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SobelFilter();
            Bitmap res = filter.processImage(image);
            pictureBox1.Image = res;
            pictureBox1.Refresh();
        }
        private void �����������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SharpnessFilter();
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
