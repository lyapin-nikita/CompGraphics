









namespace lab2
{



    public partial class Form1 : Form
    {
        private Bin bin = new Bin();
        private View view = new View();
        private bool loaded = false;
        private int
            currentLayer = 0,
            FrameCount = 0;
        private DateTime NextFPSUpdate = DateTime.Now.AddSeconds(1);
        private bool textureMode = false;
        private bool needReload = false;



        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Application.Idle += Application_Idle;
        }

        private void Application_Idle(object? sender, EventArgs e)
        {
            displayFPS();
            glControl1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog stream = new OpenFileDialog();
            if (stream.ShowDialog() == DialogResult.OK)
            {
                string str = stream.FileName;
                bin.readBIN(str);
                view.SetupView(glControl1.Width, glControl1.Height);
                trackBar1.Maximum = Bin.Z - 1;
                loaded = true;
                glControl1.Invalidate();
            }
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (loaded)
            {
                if (textureMode)
                {
                    if (needReload)
                    {
                        view.GenerateTextureImage(currentLayer);
                        view.Load2dTexture();
                        needReload = false;
                    }
                    view.DrawTexture();
                }
                else
                {
                    view.DrawQuads(currentLayer);
                }
                glControl1.SwapBuffers();
            }
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            currentLayer = trackBar1.Value;
            needReload = true;
        }

        private void displayFPS()
        {
            if (DateTime.Now >= NextFPSUpdate)
            {
                this.Text = String.Format("CT Visualizer (fps={0})", FrameCount);
                NextFPSUpdate = DateTime.Now.AddSeconds(1);
                FrameCount = 0;
            }
            FrameCount++;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textureMode = checkBox1.Checked;
            needReload = true;
        }



        //minimum tf
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            UpdateTF();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            UpdateTF();
        }

        private void UpdateTF()
        {
            
            int min = trackBar2.Value;
            int width = trackBar3.Value;
            int max = min + width;  // Вычисляем максимум


            
            view.SetTF(min, width);

            
            if (textureMode)
                needReload = true;

            glControl1.Invalidate();
        }

    }
}
