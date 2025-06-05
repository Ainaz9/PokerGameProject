using PokerGameRSF;

namespace PokerGame
{
    partial class LoginForm
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
            panelEntrance = new Panel();
            loginButtonPicture = new PictureBox();
            linkLabelToRegistration = new LinkLabel();
            textBoxPasswordLog = new CueTextBox();
            textBoxLoginLog = new CueTextBox();
            labelLogin = new Label();
            panelEntrance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)loginButtonPicture).BeginInit();
            SuspendLayout();
            // 
            // panelEntrance
            // 
            panelEntrance.BackColor = Color.White;
            panelEntrance.Controls.Add(loginButtonPicture);
            panelEntrance.Controls.Add(linkLabelToRegistration);
            panelEntrance.Controls.Add(textBoxPasswordLog);
            panelEntrance.Controls.Add(textBoxLoginLog);
            panelEntrance.Controls.Add(labelLogin);
            panelEntrance.Location = new Point(373, 67);
            panelEntrance.Name = "panelEntrance";
            panelEntrance.Size = new Size(449, 480);
            panelEntrance.TabIndex = 1;
            // 
            // loginButtonPicture
            // 
            loginButtonPicture.Image = PokerGameRSF.Properties.Resources.buttonLogin;
            loginButtonPicture.Location = new Point(115, 324);
            loginButtonPicture.Name = "loginButtonPicture";
            loginButtonPicture.Size = new Size(210, 62);
            loginButtonPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            loginButtonPicture.TabIndex = 7;
            loginButtonPicture.TabStop = false;
            loginButtonPicture.Click += loginButtonPicture_Click;
            // 
            // linkLabelToRegistration
            // 
            linkLabelToRegistration.Location = new Point(155, 405);
            linkLabelToRegistration.Name = "linkLabelToRegistration";
            linkLabelToRegistration.Size = new Size(155, 25);
            linkLabelToRegistration.TabIndex = 6;
            linkLabelToRegistration.TabStop = true;
            linkLabelToRegistration.Text = "У вас нет аккаунта?";
            linkLabelToRegistration.LinkClicked += linkLabelToRegistration_LinkClicked;
            // 
            // textBoxPasswordLog
            // 
            textBoxPasswordLog.Cue = null;
            textBoxPasswordLog.Font = new Font("Segoe UI", 13F);
            textBoxPasswordLog.Location = new Point(48, 237);
            textBoxPasswordLog.Name = "textBoxPasswordLog";
            textBoxPasswordLog.Size = new Size(358, 36);
            textBoxPasswordLog.TabIndex = 4;
            // 
            // textBoxLoginLog
            // 
            textBoxLoginLog.Cue = null;
            textBoxLoginLog.Font = new Font("Segoe UI", 13F);
            textBoxLoginLog.Location = new Point(48, 176);
            textBoxLoginLog.Name = "textBoxLoginLog";
            textBoxLoginLog.Size = new Size(358, 36);
            textBoxLoginLog.TabIndex = 1;
            // 
            // labelLogin
            // 
            labelLogin.Font = new Font("Segoe UI", 15F);
            labelLogin.Location = new Point(187, 69);
            labelLogin.Name = "labelLogin";
            labelLogin.Size = new Size(93, 47);
            labelLogin.TabIndex = 0;
            labelLogin.Text = "ВХОД";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = PokerGameRSF.Properties.Resources.фон_главное_меню_покер;
            ClientSize = new Size(1182, 628);
            Controls.Add(panelEntrance);
            Name = "LoginForm";
            Text = "Вход";
            panelEntrance.ResumeLayout(false);
            panelEntrance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)loginButtonPicture).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelEntrance;
        private CueTextBox textBoxPasswordLog;
        private CueTextBox textBoxLoginLog;
        private Label labelLogin;
        private LinkLabel linkLabelToRegistration;
        private PictureBox loginButtonPicture;
    }
}