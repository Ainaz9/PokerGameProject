namespace PokerGameRSF
{
    partial class MenuControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            panel1 = new Panel();
            crossPictureBox = new PictureBox();
            pictureBox1 = new PictureBox();
            splitContainer2 = new SplitContainer();
            splitContainer3 = new SplitContainer();
            flowLayoutPanel1 = new FlowLayoutPanel();
            mainMenuButton = new Button();
            gameRoomButton = new Button();
            profileButton = new Button();
            rulesButton = new Button();
            exitButton = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)crossPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(panel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(196, 473);
            splitContainer1.SplitterDistance = 163;
            splitContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(crossPictureBox);
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(196, 163);
            panel1.TabIndex = 0;
            // 
            // crossPictureBox
            // 
            crossPictureBox.Image = Properties.Resources.cross;
            crossPictureBox.Location = new Point(144, 0);
            crossPictureBox.Name = "crossPictureBox";
            crossPictureBox.Size = new Size(52, 53);
            crossPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            crossPictureBox.TabIndex = 1;
            crossPictureBox.TabStop = false;
            crossPictureBox.Click += CrossPictureBox_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = Properties.Resources.покер_лицо;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(196, 163);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(exitButton);
            splitContainer2.Size = new Size(196, 306);
            splitContainer2.SplitterDistance = 220;
            splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.Location = new Point(0, 0);
            splitContainer3.Name = "splitContainer3";
            splitContainer3.Orientation = Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(flowLayoutPanel1);
            splitContainer3.Size = new Size(196, 220);
            splitContainer3.SplitterDistance = 145;
            splitContainer3.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(mainMenuButton);
            flowLayoutPanel1.Controls.Add(gameRoomButton);
            flowLayoutPanel1.Controls.Add(profileButton);
            flowLayoutPanel1.Controls.Add(rulesButton);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(196, 145);
            flowLayoutPanel1.TabIndex = 0;
            flowLayoutPanel1.WrapContents = false;
            // 
            // mainMenuButton
            // 
            mainMenuButton.Dock = DockStyle.Fill;
            mainMenuButton.Location = new Point(3, 3);
            mainMenuButton.Name = "mainMenuButton";
            mainMenuButton.Size = new Size(193, 29);
            mainMenuButton.TabIndex = 0;
            mainMenuButton.Text = "Главное меню";
            mainMenuButton.UseVisualStyleBackColor = true;
            mainMenuButton.Click += mainMenuButton_Click;
            // 
            // gameRoomButton
            // 
            gameRoomButton.Location = new Point(3, 38);
            gameRoomButton.Name = "gameRoomButton";
            gameRoomButton.Size = new Size(193, 29);
            gameRoomButton.TabIndex = 1;
            gameRoomButton.Text = "Игровой стол";
            gameRoomButton.UseVisualStyleBackColor = true;
            gameRoomButton.Click += gameRoomButton_Click;
            // 
            // profileButton
            // 
            profileButton.Location = new Point(3, 73);
            profileButton.Name = "profileButton";
            profileButton.Size = new Size(193, 29);
            profileButton.TabIndex = 2;
            profileButton.Text = "Профиль";
            profileButton.UseVisualStyleBackColor = true;
            profileButton.Click += profileButton_Click;
            // 
            // rulesButton
            // 
            rulesButton.Location = new Point(3, 108);
            rulesButton.Name = "rulesButton";
            rulesButton.Size = new Size(193, 29);
            rulesButton.TabIndex = 3;
            rulesButton.Text = "Правила игры";
            rulesButton.UseVisualStyleBackColor = true;
            rulesButton.Click += rulesButton_Click;
            // 
            // exitButton
            // 
            exitButton.Dock = DockStyle.Fill;
            exitButton.Location = new Point(0, 0);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(196, 82);
            exitButton.TabIndex = 0;
            exitButton.Text = "ВЫЙТИ ИЗ АККАУНТА";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += exitButton_Click;
            // 
            // MenuControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Name = "MenuControl";
            Size = new Size(196, 473);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)crossPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private SplitContainer splitContainer3;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button mainMenuButton;
        private Button gameRoomButton;
        private Button exitButton;
        private Button profileButton;
        private Button rulesButton;
        private Panel panel1;
        private PictureBox pictureBox1;
        private PictureBox crossPictureBox;
    }
}
