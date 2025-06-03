using PokerGameRSF.Services;
namespace PokerGameRSF
{
    partial class FormPlayer1 : Form
    {
        private System.ComponentModel.IContainer components = null;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblCard1 = new Label();
            lblCard2 = new Label();
            lblPot = new Label();
            lblMyStack = new Label();
            lblOppStack = new Label();
            lblTurn = new Label();
            lblTimer = new Label();
            btnFold = new Button();
            btnCheck = new Button();
            btnCall = new Button();
            btnRaise = new Button();
            nudRaise = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)nudRaise).BeginInit();
            SuspendLayout();
            // 
            // lblCard1
            // 
            lblCard1.Location = new Point(73, 65);
            lblCard1.Name = "lblCard1";
            lblCard1.Size = new Size(100, 23);
            lblCard1.TabIndex = 0;
            // 
            // lblCard2
            // 
            lblCard2.Location = new Point(0, 0);
            lblCard2.Name = "lblCard2";
            lblCard2.Size = new Size(100, 23);
            lblCard2.TabIndex = 1;
            // 
            // lblPot
            // 
            lblPot.Location = new Point(0, 0);
            lblPot.Name = "lblPot";
            lblPot.Size = new Size(100, 23);
            lblPot.TabIndex = 2;
            // 
            // lblMyStack
            // 
            lblMyStack.Location = new Point(0, 0);
            lblMyStack.Name = "lblMyStack";
            lblMyStack.Size = new Size(100, 23);
            lblMyStack.TabIndex = 3;
            // 
            // lblOppStack
            // 
            lblOppStack.Location = new Point(0, 0);
            lblOppStack.Name = "lblOppStack";
            lblOppStack.Size = new Size(100, 23);
            lblOppStack.TabIndex = 4;
            // 
            // lblTurn
            // 
            lblTurn.Location = new Point(0, 0);
            lblTurn.Name = "lblTurn";
            lblTurn.Size = new Size(100, 23);
            lblTurn.TabIndex = 5;
            // 
            // lblTimer
            // 
            lblTimer.Location = new Point(0, 0);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(100, 23);
            lblTimer.TabIndex = 6;
            // 
            // btnFold
            // 
            btnFold.Location = new Point(0, 0);
            btnFold.Name = "btnFold";
            btnFold.Size = new Size(75, 23);
            btnFold.TabIndex = 7;
            // 
            // btnCheck
            // 
            btnCheck.Location = new Point(0, 0);
            btnCheck.Name = "btnCheck";
            btnCheck.Size = new Size(75, 23);
            btnCheck.TabIndex = 8;
            // 
            // btnCall
            // 
            btnCall.Location = new Point(0, 0);
            btnCall.Name = "btnCall";
            btnCall.Size = new Size(75, 23);
            btnCall.TabIndex = 9;
            // 
            // btnRaise
            // 
            btnRaise.Location = new Point(0, 0);
            btnRaise.Name = "btnRaise";
            btnRaise.Size = new Size(75, 23);
            btnRaise.TabIndex = 10;
            // 
            // nudRaise
            // 
            nudRaise.Location = new Point(0, 0);
            nudRaise.Name = "nudRaise";
            nudRaise.Size = new Size(120, 27);
            nudRaise.TabIndex = 11;
            // 
            // FormPlayer1
            // 
            ClientSize = new Size(775, 411);
            Controls.Add(lblCard1);
            Controls.Add(lblCard2);
            Controls.Add(lblPot);
            Controls.Add(lblMyStack);
            Controls.Add(lblOppStack);
            Controls.Add(lblTurn);
            Controls.Add(lblTimer);
            Controls.Add(btnFold);
            Controls.Add(btnCheck);
            Controls.Add(btnCall);
            Controls.Add(btnRaise);
            Controls.Add(nudRaise);
            Name = "FormPlayer1";
            Text = "Poker - Player 1";
            ((System.ComponentModel.ISupportInitialize)nudRaise).EndInit();
            ResumeLayout(false);
        }
    }
}
