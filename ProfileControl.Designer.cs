namespace PokerGameRSF
{
    partial class ProfileControl
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
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            button1 = new Button();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox6 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(353, 148);
            label1.Name = "label1";
            label1.Size = new Size(92, 20);
            label1.TabIndex = 0;
            label1.Text = "\"Карандаш\"";
            label1.Click += label1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(197, 145);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(150, 27);
            textBox1.TabIndex = 1;
            textBox1.Text = "Логин пользователя";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(197, 190);
            label2.Name = "label2";
            label2.Size = new Size(68, 20);
            label2.TabIndex = 2;
            label2.Text = "БАЛАНС";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(197, 228);
            label3.Name = "label3";
            label3.Size = new Size(101, 20);
            label3.TabIndex = 3;
            label3.Text = "Сыграно игр:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(309, 228);
            label4.Name = "label4";
            label4.Size = new Size(116, 20);
            label4.TabIndex = 4;
            label4.Text = "количество игр";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(197, 261);
            label5.Name = "label5";
            label5.Size = new Size(105, 20);
            label5.TabIndex = 5;
            label5.Text = "Начал играть:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(316, 261);
            label6.Name = "label6";
            label6.Size = new Size(39, 20);
            label6.TabIndex = 6;
            label6.Text = "дата";
            label6.Click += label6_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(411, 319);
            label7.Name = "label7";
            label7.Size = new Size(154, 20);
            label7.TabIndex = 7;
            label7.Text = "Лучшая комбинация";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(670, 139);
            label8.Name = "label8";
            label8.Size = new Size(134, 20);
            label8.TabIndex = 8;
            label8.Text = "Лучший выйгрыш";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(670, 228);
            label9.Name = "label9";
            label9.Size = new Size(140, 20);
            label9.TabIndex = 9;
            label9.Text = "Количество фишек";
            // 
            // button1
            // 
            button1.Location = new Point(3, 284);
            button1.Name = "button1";
            button1.Size = new Size(197, 29);
            button1.TabIndex = 10;
            button1.Text = "Изменить фото профиля";
            button1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.ава_профиля;
            pictureBox1.Location = new Point(26, 200);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(125, 62);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.game_card;
            pictureBox2.Location = new Point(26, 374);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(125, 62);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 12;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.game_card;
            pictureBox3.Location = new Point(197, 374);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(125, 62);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 13;
            pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.game_card;
            pictureBox4.Location = new Point(371, 374);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(125, 62);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 14;
            pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = Properties.Resources.game_card;
            pictureBox5.Location = new Point(551, 374);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(125, 62);
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.TabIndex = 15;
            pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = Properties.Resources.game_card;
            pictureBox6.Location = new Point(713, 374);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(125, 62);
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.TabIndex = 16;
            pictureBox6.TabStop = false;
            // 
            // ProfileControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pictureBox6);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(button1);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Name = "ProfileControl";
            Size = new Size(850, 459);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Button button1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        private PictureBox pictureBox6;
    }
}
