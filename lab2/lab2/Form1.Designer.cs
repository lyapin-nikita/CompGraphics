namespace lab2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            glControl1 = new OpenTK.GLControl.GLControl();
            button1 = new Button();
            trackBar1 = new TrackBar();
            checkBox1 = new CheckBox();
            trackBar2 = new TrackBar();
            trackBar3 = new TrackBar();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar3).BeginInit();
            SuspendLayout();
            // 
            // glControl1
            // 
            glControl1.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
            glControl1.APIVersion = new Version(3, 3, 0, 0);
            glControl1.Flags = OpenTK.Windowing.Common.ContextFlags.Default;
            glControl1.IsEventDriven = true;
            glControl1.Location = new Point(487, 130);
            glControl1.Name = "glControl1";
            glControl1.Profile = OpenTK.Windowing.Common.ContextProfile.Compatability;
            glControl1.SharedContext = null;
            glControl1.Size = new Size(884, 525);
            glControl1.TabIndex = 0;
            glControl1.Paint += glControl1_Paint;
            // 
            // button1
            // 
            button1.Location = new Point(487, 79);
            button1.Name = "button1";
            button1.Size = new Size(101, 45);
            button1.TabIndex = 1;
            button1.Text = "Загрузить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(766, 661);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(304, 45);
            trackBar1.TabIndex = 2;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(1211, 105);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(160, 19);
            checkBox1.TabIndex = 3;
            checkBox1.Text = "Визуализация текстурой";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // trackBar2
            // 
            trackBar2.Location = new Point(605, 79);
            trackBar2.Maximum = 5000;
            trackBar2.Name = "trackBar2";
            trackBar2.Size = new Size(233, 45);
            trackBar2.TabIndex = 4;
            trackBar2.Scroll += trackBar2_Scroll;
            // 
            // trackBar3
            // 
            trackBar3.Location = new Point(844, 79);
            trackBar3.Maximum = 5000;
            trackBar3.Minimum = 100;
            trackBar3.Name = "trackBar3";
            trackBar3.Size = new Size(305, 45);
            trackBar3.TabIndex = 5;
            trackBar3.Value = 2000;
            trackBar3.Scroll += trackBar3_Scroll;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(677, 50);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(77, 23);
            textBox1.TabIndex = 6;
            textBox1.Text = "Минимум TF";
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(962, 50);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(77, 23);
            textBox2.TabIndex = 7;
            textBox2.Text = "Ширина TF";
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1899, 815);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(trackBar3);
            Controls.Add(trackBar2);
            Controls.Add(checkBox1);
            Controls.Add(trackBar1);
            Controls.Add(button1);
            Controls.Add(glControl1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenTK.GLControl.GLControl glControl1;
        private Button button1;
        private TrackBar trackBar1;
        private CheckBox checkBox1;
        private TrackBar trackBar2;
        private TrackBar trackBar3;
        private TextBox textBox1;
        private TextBox textBox2;

    }
}
