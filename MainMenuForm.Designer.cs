namespace PokerGame
{
    partial class MainMenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuButtonPictureBox = new PictureBox();
            contentNameLabel = new Label();
            contentPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)menuButtonPictureBox).BeginInit();
            SuspendLayout();
            // 
            // menuButtonPictureBox
            // 
            menuButtonPictureBox.BackColor = Color.Transparent;
            menuButtonPictureBox.Image = PokerGameRSF.Properties.Resources._3lines;
            menuButtonPictureBox.Location = new Point(1093, -4);
            menuButtonPictureBox.Name = "menuButtonPictureBox";
            menuButtonPictureBox.Size = new Size(59, 44);
            menuButtonPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            menuButtonPictureBox.TabIndex = 7;
            menuButtonPictureBox.TabStop = false;
            menuButtonPictureBox.Click += menuButtonPictureBox_Click;
            // 
            // contentNameLabel
            // 
            contentNameLabel.AutoSize = true;
            contentNameLabel.BackColor = Color.Transparent;
            contentNameLabel.Location = new Point(468, 20);
            contentNameLabel.Name = "contentNameLabel";
            contentNameLabel.Size = new Size(126, 20);
            contentNameLabel.TabIndex = 0;
            contentNameLabel.Text = "ГЛАВНОЕ МЕНЮ";
            contentNameLabel.Click += label1_Click;
            // 
            // contentPanel
            // 
            contentPanel.Location = new Point(0, 59);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(1182, 596);
            contentPanel.TabIndex = 8;
            // 
            // MainMenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = PokerGameRSF.Properties.Resources.фон_главное_меню_покер;
            ClientSize = new Size(1182, 655);
            Controls.Add(contentPanel);
            Controls.Add(menuButtonPictureBox);
            Controls.Add(contentNameLabel);
            Name = "MainMenuForm";
            Text = "MainMenuForm";
            Load += MainMenuForm_Load;
            ((System.ComponentModel.ISupportInitialize)menuButtonPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox menuButtonPictureBox;
        private Label contentNameLabel;
        private Panel contentPanel;
    }
}