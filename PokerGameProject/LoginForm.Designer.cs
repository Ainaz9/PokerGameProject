namespace PokerGameProject
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
            buttonLogin = new Button();
            textBoxPasswordLog = new TextBox();
            textBoxLoginLog = new TextBox();
            labelLogin = new Label();
            linkLabelToRegistration = new LinkLabel();
            panelEntrance.SuspendLayout();
            SuspendLayout();
            // 
            // panelEntrance
            // 
            panelEntrance.BackColor = Color.White;
            panelEntrance.Controls.Add(linkLabelToRegistration);
            panelEntrance.Controls.Add(buttonLogin);
            panelEntrance.Controls.Add(textBoxPasswordLog);
            panelEntrance.Controls.Add(textBoxLoginLog);
            panelEntrance.Controls.Add(labelLogin);
            panelEntrance.Location = new Point(372, 67);
            panelEntrance.Name = "panelEntrance";
            panelEntrance.Size = new Size(449, 480);
            panelEntrance.TabIndex = 1;
            // 
            // buttonLogin
            // 
            buttonLogin.Font = new Font("Segoe UI", 13F);
            buttonLogin.Location = new Point(102, 347);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(241, 55);
            buttonLogin.TabIndex = 5;
            buttonLogin.Text = "Войти";
            buttonLogin.UseVisualStyleBackColor = true;
            // 
            // textBoxPasswordLog
            // 
            textBoxPasswordLog.Font = new Font("Segoe UI", 13F);
            textBoxPasswordLog.Location = new Point(48, 238);
            textBoxPasswordLog.Name = "textBoxPasswordLog";
            textBoxPasswordLog.Size = new Size(358, 36);
            textBoxPasswordLog.TabIndex = 4;
            // 
            // textBoxLoginLog
            // 
            textBoxLoginLog.Font = new Font("Segoe UI", 13F);
            textBoxLoginLog.Location = new Point(48, 176);
            textBoxLoginLog.Name = "textBoxLoginLog";
            textBoxLoginLog.Size = new Size(358, 36);
            textBoxLoginLog.TabIndex = 1;
            // 
            // labelLogin
            // 
            labelLogin.Font = new Font("Segoe UI", 15F);
            labelLogin.Location = new Point(188, 69);
            labelLogin.Name = "labelLogin";
            labelLogin.Size = new Size(93, 47);
            labelLogin.TabIndex = 0;
            labelLogin.Text = "ВХОД";
            // 
            // linkLabelToRegistration
            // 
            linkLabelToRegistration.Location = new Point(155, 405);
            linkLabelToRegistration.Name = "linkLabelToRegistration";
            linkLabelToRegistration.Size = new Size(156, 25);
            linkLabelToRegistration.TabIndex = 6;
            linkLabelToRegistration.TabStop = true;
            linkLabelToRegistration.Text = "У вас нет аккаунта?";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 628);
            Controls.Add(panelEntrance);
            Name = "LoginForm";
            Text = "Вход";
            panelEntrance.ResumeLayout(false);
            panelEntrance.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelEntrance;
        private Button buttonLogin;
        private TextBox textBoxPasswordLog;
        private TextBox textBoxLoginLog;
        private Label labelLogin;
        private LinkLabel linkLabelToRegistration;
    }
}