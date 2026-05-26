namespace DotNet03GuessNum {
    partial class FrmMain {
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
            BtnCheck = new Button();
            LblDisplay = new Label();
            BtnStart = new Button();
            TxtGuess = new TextBox();
            SuspendLayout();
            // 
            // BtnCheck
            // 
            BtnCheck.Location = new Point(274, 66);
            BtnCheck.Name = "BtnCheck";
            BtnCheck.Size = new Size(83, 30);
            BtnCheck.TabIndex = 0;
            BtnCheck.Text = "맞히기";
            BtnCheck.UseVisualStyleBackColor = true;
            // 
            // LblDisplay
            // 
            LblDisplay.Dock = DockStyle.Top;
            LblDisplay.Font = new Font("맑은 고딕", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LblDisplay.Location = new Point(0, 0);
            LblDisplay.Name = "LblDisplay";
            LblDisplay.Size = new Size(369, 54);
            LblDisplay.TabIndex = 1;
            LblDisplay.Text = "게임을 시작합니다";
            LblDisplay.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BtnStart
            // 
            BtnStart.Location = new Point(12, 120);
            BtnStart.Name = "BtnStart";
            BtnStart.Size = new Size(345, 44);
            BtnStart.TabIndex = 2;
            BtnStart.Text = "게임 시작";
            BtnStart.UseVisualStyleBackColor = true;
            BtnStart.Click += BtnStart_Click;
            // 
            // TxtGuess
            // 
            TxtGuess.Font = new Font("맑은 고딕", 12F);
            TxtGuess.Location = new Point(12, 66);
            TxtGuess.Name = "TxtGuess";
            TxtGuess.Size = new Size(256, 29);
            TxtGuess.TabIndex = 3;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(369, 176);
            Controls.Add(TxtGuess);
            Controls.Add(BtnStart);
            Controls.Add(LblDisplay);
            Controls.Add(BtnCheck);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnCheck;
        private Label LblDisplay;
        private Button BtnStart;
        private TextBox TxtGuess;
    }
}
