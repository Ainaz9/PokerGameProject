using PokerGameRSF.Services;

namespace PokerGameRSF
{
    partial class FormPlayer2
    {
        private Label lblCard1;
        private Label lblCard2;
        private Label lblPot;
        private Label lblMyStack;
        private Label lblOppStack;
        private Label lblTurn;
        private Label lblTimer;
        private Button btnFold;
        private Button btnCheck;
        private Button btnCall;
        private Button btnRaise;
        private NumericUpDown nudRaise;

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
            this.Text = "Poker - Player 2";
            this.Width = 400;
            this.Height = 300;

            lblCard1 = new Label() { Left = 20, Top = 20, Width = 100 };
            lblCard2 = new Label() { Left = 120, Top = 20, Width = 100 };
            lblPot = new Label() { Left = 20, Top = 60, Width = 200 };
            lblMyStack = new Label() { Left = 20, Top = 90, Width = 200 };
            lblOppStack = new Label() { Left = 20, Top = 120, Width = 200 };
            lblTurn = new Label() { Left = 20, Top = 150, Width = 200 };
            lblTimer = new Label() { Left = 20, Top = 180, Width = 200 };

            btnFold = new Button() { Text = "Fold", Left = 20, Top = 210, Width = 60 };
            
            btnCheck = new Button() { Text = "Check", Left = 90, Top = 210, Width = 60 };
            
            btnCall = new Button() { Text = "Call", Left = 160, Top = 210, Width = 60 };
           
            btnRaise = new Button() { Text = "Raise", Left = 230, Top = 210, Width = 60 };
            
            nudRaise = new NumericUpDown() { Left = 300, Top = 210, Width = 60, Minimum = 1, Maximum = 1000, Value = 10 };

            this.Controls.Add(lblCard1);
            this.Controls.Add(lblCard2);
            this.Controls.Add(lblPot);
            this.Controls.Add(lblMyStack);
            this.Controls.Add(lblOppStack);
            this.Controls.Add(lblTurn);
            this.Controls.Add(lblTimer);
            this.Controls.Add(btnFold);
            this.Controls.Add(btnCheck);
            this.Controls.Add(btnCall);
            this.Controls.Add(btnRaise);
            this.Controls.Add(nudRaise);
        }

        #endregion
    }
}