using PokerGameRSF;

namespace PokerGame
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
            linkLabelToLogin = new LinkLabel();
            buttonRegister = new Button();
            textBoxRepeatPasswordReg = new CueTextBox();
            textBoxPasswordReg = new CueTextBox();
            textBoxEmailReg = new CueTextBox();
            textBoxLoginReg = new CueTextBox();
            labelRegister = new Label();
            pictureBox1 = new PictureBox();
            panelRegister.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panelRegister
            // 
            panelRegister.BackColor = Color.White;
            panelRegister.Controls.Add(pictureBox1);
            panelRegister.Controls.Add(linkLabelToLogin);
            panelRegister.Controls.Add(buttonRegister);
            panelRegister.Controls.Add(textBoxRepeatPasswordReg);
            panelRegister.Controls.Add(textBoxPasswordReg);
            panelRegister.Controls.Add(textBoxEmailReg);
            panelRegister.Controls.Add(textBoxLoginReg);
            panelRegister.Controls.Add(labelRegister);
            panelRegister.Location = new Point(420, 77);
            panelRegister.Name = "panelRegister";
            panelRegister.Size = new Size(505, 552);
            panelRegister.TabIndex = 0;
            // 
            // linkLabelToLogin
            // 
            linkLabelToLogin.Location = new Point(182, 484);
            linkLabelToLogin.Name = "linkLabelToLogin";
            linkLabelToLogin.Size = new Size(152, 29);
            linkLabelToLogin.TabIndex = 7;
            linkLabelToLogin.TabStop = true;
            linkLabelToLogin.Text = "Уже есть аккаунт?";
            // 
            // buttonRegister
            // 
            buttonRegister.BackColor = Color.Gray;
            buttonRegister.FlatStyle = FlatStyle.Flat;
            buttonRegister.Font = new Font("Segoe UI", 10F);
            buttonRegister.Location = new Point(117, 417);
            buttonRegister.Name = "buttonRegister";
            buttonRegister.Size = new Size(271, 63);
            buttonRegister.TabIndex = 5;
            buttonRegister.Text = "Зарегистрироваться";
            buttonRegister.UseVisualStyleBackColor = false;
            buttonRegister.Click += buttonRegister_Click;
            // 
            // textBoxRepeatPasswordReg
            // 
            textBoxRepeatPasswordReg.Cue = null;
            textBoxRepeatPasswordReg.Font = new Font("Segoe UI", 13F);
            textBoxRepeatPasswordReg.Location = new Point(54, 340);
            textBoxRepeatPasswordReg.Name = "textBoxRepeatPasswordReg";
            textBoxRepeatPasswordReg.Size = new Size(360, 36);
            textBoxRepeatPasswordReg.TabIndex = 4;
            // 
            // textBoxPasswordReg
            // 
            textBoxPasswordReg.Cue = null;
            textBoxPasswordReg.Font = new Font("Segoe UI", 13F);
            textBoxPasswordReg.Location = new Point(54, 287);
            textBoxPasswordReg.Name = "textBoxPasswordReg";
            textBoxPasswordReg.Size = new Size(360, 36);
            textBoxPasswordReg.TabIndex = 3;
            // 
            // textBoxEmailReg
            // 
            textBoxEmailReg.Cue = null;
            textBoxEmailReg.Font = new Font("Segoe UI", 13F);
            textBoxEmailReg.Location = new Point(54, 204);
            textBoxEmailReg.Name = "textBoxEmailReg";
            textBoxEmailReg.Size = new Size(402, 36);
            textBoxEmailReg.TabIndex = 2;
            // 
            // textBoxLoginReg
            // 
            textBoxLoginReg.Cue = null;
            textBoxLoginReg.Font = new Font("Segoe UI", 13F);
            textBoxLoginReg.Location = new Point(54, 138);
            textBoxLoginReg.Name = "textBoxLoginReg";
            textBoxLoginReg.Size = new Size(402, 36);
            textBoxLoginReg.TabIndex = 1;
            textBoxLoginReg.TextChanged += textBoxLoginReg_TextChanged;
            // 
            // labelRegister
            // 
            labelRegister.BackColor = Color.Transparent;
            labelRegister.Font = new Font("Segoe UI", 15F);
            labelRegister.ForeColor = SystemColors.ActiveCaptionText;
            labelRegister.Location = new Point(147, 68);
            labelRegister.Name = "labelRegister";
            labelRegister.Size = new Size(209, 54);
            labelRegister.TabIndex = 0;
            labelRegister.Text = "РЕГИСТРАЦИЯ";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(428, 289);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(41, 34);
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1330, 722);
            Controls.Add(panelRegister);
            Font = new Font("Segoe UI", 10F);
            ForeColor = SystemColors.ButtonHighlight;
            Name = "RegisterForm";
            Text = "Регистрация";
            panelRegister.ResumeLayout(false);
            panelRegister.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelRegister;
        private CueTextBox textBoxRepeatPasswordReg;
        private CueTextBox textBoxPasswordReg;
        private CueTextBox textBoxEmailReg;
        private CueTextBox textBoxLoginReg;
        private Label labelRegister;
        private Button buttonRegister;
        private LinkLabel linkLabelToLogin;
        private PictureBox pictureBox1;
    }
}
