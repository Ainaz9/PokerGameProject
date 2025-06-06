﻿using System;
using System.Drawing;

namespace PokerGameRSF
{
    partial class MainMenuContentControl
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
            label2 = new Label();
            pictureBox3 = new PictureBox();
            label4 = new Label();
            label3 = new Label();
            startGameButtonPicture = new PictureBox();
            startgameLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)startGameButtonPicture).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Location = new Point(453, 145);
            label2.Name = "label2";
            label2.Size = new Size(266, 20);
            label2.TabIndex = 8;
            label2.Text = "♠ Добро пожаловать в PokerArena! ♠";
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.Image = Properties.Resources.покер_лицо;
            pictureBox3.Location = new Point(474, 33);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(212, 87);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 14;
            pictureBox3.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Location = new Point(380, 254);
            label4.Name = "label4";
            label4.Size = new Size(431, 20);
            label4.TabIndex = 13;
            label4.Text = "свою удачу, стратегию и стать настоящим мастером покера!";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Location = new Point(310, 199);
            label3.Name = "label3";
            label3.Size = new Size(640, 20);
            label3.TabIndex = 12;
            label3.Text = "Погрузись в мир захватывающих покерных дуэлей.  Здесь ты можешь испытать                  ";
            // 
            // startGameButtonPicture
            // 
            startGameButtonPicture.BackColor = Color.Transparent;
            startGameButtonPicture.Image = Properties.Resources.buttonStartGame;
            startGameButtonPicture.Location = new Point(504, 315);
            startGameButtonPicture.Name = "startGameButtonPicture";
            startGameButtonPicture.Size = new Size(161, 46);
            startGameButtonPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            startGameButtonPicture.TabIndex = 15;
            startGameButtonPicture.TabStop = false;
            startGameButtonPicture.Click += startGameButtonPicture_Click;
            // 
            // startgameLabel
            // 
            startgameLabel.AutoSize = true;
            startgameLabel.BackColor = Color.White;
            startgameLabel.Location = new Point(538, 330);
            startgameLabel.Name = "startgameLabel";
            startgameLabel.Size = new Size(93, 20);
            startgameLabel.TabIndex = 16;
            startgameLabel.Text = "Начать игру";
            startgameLabel.Click += startgameLabel_Click;
            // 
            // MainMenuContentControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label2);
            Controls.Add(startgameLabel);
            Controls.Add(startGameButtonPicture);
            Controls.Add(pictureBox3);
            Controls.Add(label4);
            Controls.Add(label3);
            Name = "MainMenuContentControl";
            Size = new Size(1130, 613);
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)startGameButtonPicture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private PictureBox pictureBox3;
        private Label label4;
        private Label label3;
        private PictureBox startGameButtonPicture;
        private Label startgameLabel;
    }
}
