namespace lab1
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
            pictureBox1 = new PictureBox();
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            открытьToolStripMenuItem = new ToolStripMenuItem();
            фильтрыToolStripMenuItem = new ToolStripMenuItem();
            точечныеToolStripMenuItem = new ToolStripMenuItem();
            инверсияToolStripMenuItem = new ToolStripMenuItem();
            черноебелоеToolStripMenuItem = new ToolStripMenuItem();
            сепияToolStripMenuItem = new ToolStripMenuItem();
            повышениеЯркостиToolStripMenuItem = new ToolStripMenuItem();
            медианныйФильтрToolStripMenuItem = new ToolStripMenuItem();
            стеклоToolStripMenuItem = new ToolStripMenuItem();
            матричныеToolStripMenuItem = new ToolStripMenuItem();
            размытиеToolStripMenuItem = new ToolStripMenuItem();
            размытиеГауссаToolStripMenuItem = new ToolStripMenuItem();
            фильтрСобеляToolStripMenuItem = new ToolStripMenuItem();
            повышениеРезкостиToolStripMenuItem = new ToolStripMenuItem();
            тиснениеToolStripMenuItem = new ToolStripMenuItem();
            светящиесяКраяToolStripMenuItem = new ToolStripMenuItem();
            motionBlurToolStripMenuItem = new ToolStripMenuItem();
            преобразованиеToolStripMenuItem = new ToolStripMenuItem();
            переносToolStripMenuItem = new ToolStripMenuItem();
            поворотToolStripMenuItem = new ToolStripMenuItem();
            волнаToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(297, 84);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(668, 448);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, фильтрыToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1266, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { открытьToolStripMenuItem });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(48, 20);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            открытьToolStripMenuItem.Size = new Size(121, 22);
            открытьToolStripMenuItem.Text = "Открыть";
            открытьToolStripMenuItem.Click += открытьToolStripMenuItem_Click;
            // 
            // фильтрыToolStripMenuItem
            // 
            фильтрыToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { точечныеToolStripMenuItem, матричныеToolStripMenuItem, преобразованиеToolStripMenuItem });
            фильтрыToolStripMenuItem.Name = "фильтрыToolStripMenuItem";
            фильтрыToolStripMenuItem.Size = new Size(69, 20);
            фильтрыToolStripMenuItem.Text = "Фильтры";
            // 
            // точечныеToolStripMenuItem
            // 
            точечныеToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { инверсияToolStripMenuItem, черноебелоеToolStripMenuItem, сепияToolStripMenuItem, повышениеЯркостиToolStripMenuItem, медианныйФильтрToolStripMenuItem, стеклоToolStripMenuItem, волнаToolStripMenuItem });
            точечныеToolStripMenuItem.Name = "точечныеToolStripMenuItem";
            точечныеToolStripMenuItem.Size = new Size(180, 22);
            точечныеToolStripMenuItem.Text = "Точечные";
            // 
            // инверсияToolStripMenuItem
            // 
            инверсияToolStripMenuItem.Name = "инверсияToolStripMenuItem";
            инверсияToolStripMenuItem.Size = new Size(189, 22);
            инверсияToolStripMenuItem.Text = "Инверсия";
            инверсияToolStripMenuItem.Click += инверсияToolStripMenuItem_Click;
            // 
            // черноебелоеToolStripMenuItem
            // 
            черноебелоеToolStripMenuItem.Name = "черноебелоеToolStripMenuItem";
            черноебелоеToolStripMenuItem.Size = new Size(189, 22);
            черноебелоеToolStripMenuItem.Text = "Черное-белое";
            черноебелоеToolStripMenuItem.Click += черноебелоеToolStripMenuItem_Click;
            // 
            // сепияToolStripMenuItem
            // 
            сепияToolStripMenuItem.Name = "сепияToolStripMenuItem";
            сепияToolStripMenuItem.Size = new Size(189, 22);
            сепияToolStripMenuItem.Text = "Сепия";
            сепияToolStripMenuItem.Click += сепияToolStripMenuItem_Click;
            // 
            // повышениеЯркостиToolStripMenuItem
            // 
            повышениеЯркостиToolStripMenuItem.Name = "повышениеЯркостиToolStripMenuItem";
            повышениеЯркостиToolStripMenuItem.Size = new Size(189, 22);
            повышениеЯркостиToolStripMenuItem.Text = "Повышение яркости";
            повышениеЯркостиToolStripMenuItem.Click += повышениеЯркостиToolStripMenuItem_Click;
            // 
            // медианныйФильтрToolStripMenuItem
            // 
            медианныйФильтрToolStripMenuItem.Name = "медианныйФильтрToolStripMenuItem";
            медианныйФильтрToolStripMenuItem.Size = new Size(189, 22);
            медианныйФильтрToolStripMenuItem.Text = "Медианный фильтр";
            медианныйФильтрToolStripMenuItem.Click += медианныйФильтрToolStripMenuItem_Click;
            // 
            // стеклоToolStripMenuItem
            // 
            стеклоToolStripMenuItem.Name = "стеклоToolStripMenuItem";
            стеклоToolStripMenuItem.Size = new Size(189, 22);
            стеклоToolStripMenuItem.Text = "Стекло";
            стеклоToolStripMenuItem.Click += стеклоToolStripMenuItem_Click;
            // 
            // матричныеToolStripMenuItem
            // 
            матричныеToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { размытиеToolStripMenuItem, размытиеГауссаToolStripMenuItem, фильтрСобеляToolStripMenuItem, повышениеРезкостиToolStripMenuItem, тиснениеToolStripMenuItem, светящиесяКраяToolStripMenuItem, motionBlurToolStripMenuItem });
            матричныеToolStripMenuItem.Name = "матричныеToolStripMenuItem";
            матричныеToolStripMenuItem.Size = new Size(180, 22);
            матричныеToolStripMenuItem.Text = "Матричные";
            // 
            // размытиеToolStripMenuItem
            // 
            размытиеToolStripMenuItem.Name = "размытиеToolStripMenuItem";
            размытиеToolStripMenuItem.Size = new Size(194, 22);
            размытиеToolStripMenuItem.Text = "Размытие";
            размытиеToolStripMenuItem.Click += размытиеToolStripMenuItem_Click;
            // 
            // размытиеГауссаToolStripMenuItem
            // 
            размытиеГауссаToolStripMenuItem.Name = "размытиеГауссаToolStripMenuItem";
            размытиеГауссаToolStripMenuItem.Size = new Size(194, 22);
            размытиеГауссаToolStripMenuItem.Text = "Размытие Гаусса";
            размытиеГауссаToolStripMenuItem.Click += размытиеГауссаToolStripMenuItem_Click;
            // 
            // фильтрСобеляToolStripMenuItem
            // 
            фильтрСобеляToolStripMenuItem.Name = "фильтрСобеляToolStripMenuItem";
            фильтрСобеляToolStripMenuItem.Size = new Size(194, 22);
            фильтрСобеляToolStripMenuItem.Text = "Фильтр Собеля";
            фильтрСобеляToolStripMenuItem.Click += фильтрСобеляToolStripMenuItem_Click;
            // 
            // повышениеРезкостиToolStripMenuItem
            // 
            повышениеРезкостиToolStripMenuItem.Name = "повышениеРезкостиToolStripMenuItem";
            повышениеРезкостиToolStripMenuItem.Size = new Size(194, 22);
            повышениеРезкостиToolStripMenuItem.Text = "Повышение резкости";
            повышениеРезкостиToolStripMenuItem.Click += повышениеРезкостиToolStripMenuItem_Click;
            // 
            // тиснениеToolStripMenuItem
            // 
            тиснениеToolStripMenuItem.Name = "тиснениеToolStripMenuItem";
            тиснениеToolStripMenuItem.Size = new Size(194, 22);
            тиснениеToolStripMenuItem.Text = "Тиснение";
            тиснениеToolStripMenuItem.Click += тиснениеToolStripMenuItem_Click;
            // 
            // светящиесяКраяToolStripMenuItem
            // 
            светящиесяКраяToolStripMenuItem.Name = "светящиесяКраяToolStripMenuItem";
            светящиесяКраяToolStripMenuItem.Size = new Size(194, 22);
            светящиесяКраяToolStripMenuItem.Text = "Светящиеся края";
            светящиесяКраяToolStripMenuItem.Click += светящиесяКраяToolStripMenuItem_Click;
            // 
            // motionBlurToolStripMenuItem
            // 
            motionBlurToolStripMenuItem.Name = "motionBlurToolStripMenuItem";
            motionBlurToolStripMenuItem.Size = new Size(194, 22);
            motionBlurToolStripMenuItem.Text = "Motion Blur";
            motionBlurToolStripMenuItem.Click += motionBlurToolStripMenuItem_Click;
            // 
            // преобразованиеToolStripMenuItem
            // 
            преобразованиеToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { переносToolStripMenuItem, поворотToolStripMenuItem });
            преобразованиеToolStripMenuItem.Name = "преобразованиеToolStripMenuItem";
            преобразованиеToolStripMenuItem.Size = new Size(180, 22);
            преобразованиеToolStripMenuItem.Text = "Преобразование";
            // 
            // переносToolStripMenuItem
            // 
            переносToolStripMenuItem.Name = "переносToolStripMenuItem";
            переносToolStripMenuItem.Size = new Size(122, 22);
            переносToolStripMenuItem.Text = "Перенос";
            переносToolStripMenuItem.Click += переносToolStripMenuItem_Click;
            // 
            // поворотToolStripMenuItem
            // 
            поворотToolStripMenuItem.Name = "поворотToolStripMenuItem";
            поворотToolStripMenuItem.Size = new Size(122, 22);
            поворотToolStripMenuItem.Text = "Поворот";
            поворотToolStripMenuItem.Click += поворотToolStripMenuItem_Click;
            // 
            // волнаToolStripMenuItem
            // 
            волнаToolStripMenuItem.Name = "волнаToolStripMenuItem";
            волнаToolStripMenuItem.Size = new Size(189, 22);
            волнаToolStripMenuItem.Text = "Волна";
            волнаToolStripMenuItem.Click += волнаToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(1266, 594);
            Controls.Add(pictureBox1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem открытьToolStripMenuItem;
        private ToolStripMenuItem фильтрыToolStripMenuItem;
        private ToolStripMenuItem точечныеToolStripMenuItem;
        private ToolStripMenuItem инверсияToolStripMenuItem;
        private ToolStripMenuItem матричныеToolStripMenuItem;
        private ToolStripMenuItem размытиеToolStripMenuItem;
        private ToolStripMenuItem размытиеГауссаToolStripMenuItem;
        private ToolStripMenuItem черноебелоеToolStripMenuItem;
        private ToolStripMenuItem сепияToolStripMenuItem;
        private ToolStripMenuItem повышениеЯркостиToolStripMenuItem;
        private ToolStripMenuItem фильтрСобеляToolStripMenuItem;
        private ToolStripMenuItem повышениеРезкостиToolStripMenuItem;
        private ToolStripMenuItem тиснениеToolStripMenuItem;
        private ToolStripMenuItem светящиесяКраяToolStripMenuItem;
        private ToolStripMenuItem медианныйФильтрToolStripMenuItem;
        private ToolStripMenuItem преобразованиеToolStripMenuItem;
        private ToolStripMenuItem переносToolStripMenuItem;
        private ToolStripMenuItem поворотToolStripMenuItem;
        private ToolStripMenuItem стеклоToolStripMenuItem;
        private ToolStripMenuItem motionBlurToolStripMenuItem;
        private ToolStripMenuItem волнаToolStripMenuItem;
    }
}
