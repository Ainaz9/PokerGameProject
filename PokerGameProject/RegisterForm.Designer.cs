namespace PokerGameProject
{
    partial class RegisterForm
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
            panelRegister = new Panel();
            buttonRegister = new Button();
            textBoxRepeatPasswordReg = new TextBox();
            textBoxPasswordReg = new TextBox();
            textBoxEmailReg = new TextBox();
            textBoxLoginReg = new TextBox();
            labelRegister = new Label();
            linkLabelToLogin = new LinkLabel();
            panelRegister.SuspendLayout();
            SuspendLayout();
            // 
            // panelRegister
            // 
            panelRegister.BackColor = Color.White;
            panelRegister.Controls.Add(linkLabelToLogin);
            panelRegister.Controls.Add(buttonRegister);
            panelRegister.Controls.Add(textBoxRepeatPasswordReg);
            panelRegister.Controls.Add(textBoxPasswordReg);
            panelRegister.Controls.Add(textBoxEmailReg);
            panelRegister.Controls.Add(textBoxLoginReg);
            panelRegister.Controls.Add(labelRegister);
            panelRegister.Location = new Point(372, 67);
            panelRegister.Name = "panelRegister";
            panelRegister.Size = new Size(449, 480);
            panelRegister.TabIndex = 0;
            // 
            // buttonRegister
            // 
            buttonRegister.Font = new Font("Segoe UI", 13F);
            buttonRegister.Location = new Point(104, 363);
            buttonRegister.Name = "buttonRegister";
            buttonRegister.Size = new Size(241, 55);
            buttonRegister.TabIndex = 5;
            buttonRegister.Text = "Зарегистрироваться";
            buttonRegister.UseVisualStyleBackColor = true;
            // 
            // textBoxRepeatPasswordReg
            // 
            textBoxRepeatPasswordReg.Font = new Font("Segoe UI", 13F);
            textBoxRepeatPasswordReg.Location = new Point(48, 296);
            textBoxRepeatPasswordReg.Name = "textBoxRepeatPasswordReg";
            textBoxRepeatPasswordReg.Size = new Size(358, 36);
            textBoxRepeatPasswordReg.TabIndex = 4;
            // 
            // textBoxPasswordReg
            // 
            textBoxPasswordReg.Font = new Font("Segoe UI", 13F);
            textBoxPasswordReg.Location = new Point(48, 236);
            textBoxPasswordReg.Name = "textBoxPasswordReg";
            textBoxPasswordReg.Size = new Size(358, 36);
            textBoxPasswordReg.TabIndex = 3;
            // 
            // textBoxEmailReg
            // 
            textBoxEmailReg.Font = new Font("Segoe UI", 13F);
            textBoxEmailReg.Location = new Point(48, 177);
            textBoxEmailReg.Name = "textBoxEmailReg";
            textBoxEmailReg.Size = new Size(358, 36);
            textBoxEmailReg.TabIndex = 2;
            // 
            // textBoxLoginReg
            // 
            textBoxLoginReg.Font = new Font("Segoe UI", 13F);
            textBoxLoginReg.Location = new Point(48, 120);
            textBoxLoginReg.Name = "textBoxLoginReg";
            textBoxLoginReg.Size = new Size(358, 36);
            textBoxLoginReg.TabIndex = 1;
            // 
            // labelRegister
            // 
            labelRegister.Font = new Font("Segoe UI", 15F);
            labelRegister.Location = new Point(131, 59);
            labelRegister.Name = "labelRegister";
            labelRegister.Size = new Size(186, 47);
            labelRegister.TabIndex = 0;
            labelRegister.Text = "РЕГИСТРАЦИЯ";
            // 
            // linkLabelToLogin
            // 
            linkLabelToLogin.Location = new Point(162, 421);
            linkLabelToLogin.Name = "linkLabelToLogin";
            linkLabelToLogin.Size = new Size(135, 25);
            linkLabelToLogin.TabIndex = 7;
            linkLabelToLogin.TabStop = true;
            linkLabelToLogin.Text = "Уже есть аккаунт?";
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 628);
            Controls.Add(panelRegister);
            Name = "RegisterForm";
            Text = "Регистрация";
            panelRegister.ResumeLayout(false);
            panelRegister.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelRegister;
        private TextBox textBoxRepeatPasswordReg;
        private TextBox textBoxPasswordReg;
        private TextBox textBoxEmailReg;
        private TextBox textBoxLoginReg;
        private Label labelRegister;
        private Button buttonRegister;
        private LinkLabel linkLabelToLogin;
    }
}
