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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuControl));
            splitContainer1 = new SplitContainer();
            panel1 = new Panel();
            languageButtonPictureBox = new PictureBox();
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
            ((System.ComponentModel.ISupportInitialize)languageButtonPictureBox).BeginInit();
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
            splitContainer1.BackColor = Color.White;
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = Color.White;
            splitContainer1.Panel1.Controls.Add(panel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = Color.White;
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            // 
            // panel1
            // 
            panel1.Controls.Add(languageButtonPictureBox);
            panel1.Controls.Add(crossPictureBox);
            panel1.Controls.Add(pictureBox1);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // languageButtonPictureBox
            // 
            languageButtonPictureBox.BackColor = Color.Transparent;
            languageButtonPictureBox.Image = Properties.Resources.russianFlag;
            resources.ApplyResources(languageButtonPictureBox, "languageButtonPictureBox");
            languageButtonPictureBox.Name = "languageButtonPictureBox";
            languageButtonPictureBox.TabStop = false;
            // 
            // crossPictureBox
            // 
            crossPictureBox.Image = Properties.Resources.cross2Poker;
            resources.ApplyResources(crossPictureBox, "crossPictureBox");
            crossPictureBox.Name = "crossPictureBox";
            crossPictureBox.TabStop = false;
            crossPictureBox.Click += CrossPictureBox_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Image = Properties.Resources.покер_лицо;
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            // 
            // splitContainer2
            // 
            resources.ApplyResources(splitContainer2, "splitContainer2");
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(exitButton);
            // 
            // splitContainer3
            // 
            resources.ApplyResources(splitContainer3, "splitContainer3");
            splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(flowLayoutPanel1);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.BackColor = Color.White;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.BackColor = Color.White;
            flowLayoutPanel1.Controls.Add(mainMenuButton);
            flowLayoutPanel1.Controls.Add(gameRoomButton);
            flowLayoutPanel1.Controls.Add(profileButton);
            flowLayoutPanel1.Controls.Add(rulesButton);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // mainMenuButton
            // 
            mainMenuButton.BackColor = Color.White;
            resources.ApplyResources(mainMenuButton, "mainMenuButton");
            mainMenuButton.FlatAppearance.MouseOverBackColor = Color.Gray;
            mainMenuButton.Name = "mainMenuButton";
            mainMenuButton.UseVisualStyleBackColor = false;
            mainMenuButton.Click += mainMenuButton_Click;
            // 
            // gameRoomButton
            // 
            gameRoomButton.BackColor = Color.White;
            gameRoomButton.FlatAppearance.MouseOverBackColor = Color.Gray;
            resources.ApplyResources(gameRoomButton, "gameRoomButton");
            gameRoomButton.Name = "gameRoomButton";
            gameRoomButton.UseVisualStyleBackColor = false;
            gameRoomButton.Click += gameRoomButton_Click;
            // 
            // profileButton
            // 
            profileButton.BackColor = Color.White;
            profileButton.FlatAppearance.MouseOverBackColor = Color.Gray;
            resources.ApplyResources(profileButton, "profileButton");
            profileButton.Name = "profileButton";
            profileButton.UseVisualStyleBackColor = false;
            profileButton.Click += profileButton_Click;
            // 
            // rulesButton
            // 
            rulesButton.BackColor = Color.White;
            rulesButton.FlatAppearance.MouseOverBackColor = Color.Gray;
            resources.ApplyResources(rulesButton, "rulesButton");
            rulesButton.Name = "rulesButton";
            rulesButton.UseVisualStyleBackColor = false;
            rulesButton.Click += rulesButton_Click;
            // 
            // exitButton
            // 
            exitButton.BackColor = Color.White;
            resources.ApplyResources(exitButton, "exitButton");
            exitButton.FlatAppearance.MouseOverBackColor = Color.Gray;
            exitButton.Name = "exitButton";
            exitButton.UseVisualStyleBackColor = false;
            exitButton.Click += exitButton_Click;
            // 
            // MenuControl
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(splitContainer1);
            Name = "MenuControl";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)languageButtonPictureBox).EndInit();
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
        private PictureBox languageButtonPictureBox;
    }
}
