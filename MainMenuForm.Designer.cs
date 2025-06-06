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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenuForm));
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
            resources.ApplyResources(menuButtonPictureBox, "menuButtonPictureBox");
            menuButtonPictureBox.Name = "menuButtonPictureBox";
            menuButtonPictureBox.TabStop = false;
            menuButtonPictureBox.Click += menuButtonPictureBox_Click;
            // 
            // contentNameLabel
            // 
            resources.ApplyResources(contentNameLabel, "contentNameLabel");
            contentNameLabel.BackColor = Color.Transparent;
            contentNameLabel.Name = "contentNameLabel";
            contentNameLabel.Click += label1_Click;
            // 
            // contentPanel
            // 
            resources.ApplyResources(contentPanel, "contentPanel");
            contentPanel.Name = "contentPanel";
            // 
            // MainMenuForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = PokerGameRSF.Properties.Resources.фон_главное_меню_покер;
            Controls.Add(contentPanel);
            Controls.Add(menuButtonPictureBox);
            Controls.Add(contentNameLabel);
            Name = "MainMenuForm";
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