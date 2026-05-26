namespace DotNet02Test {
    // partial 키워드를 통해 합침

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            BtnPress = new Button();
            LblResult = new Label();
            SuspendLayout();
            // 
            // BtnPress
            // 
            BtnPress.Location = new Point(616, 426);
            BtnPress.Name = "BtnPress";
            BtnPress.Size = new Size(156, 82);
            BtnPress.TabIndex = 0;
            BtnPress.Text = "출력";
            BtnPress.UseVisualStyleBackColor = true;
            BtnPress.Click += BtnPress_Click;
            // 
            // LblResult
            // 
            LblResult.AutoSize = true;
            LblResult.Location = new Point(349, 259);
            LblResult.Name = "LblResult";
            LblResult.Size = new Size(42, 15);
            LblResult.TabIndex = 1;
            LblResult.Text = "결과 : ";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 520);
            Controls.Add(LblResult);
            Controls.Add(BtnPress);
            Font = new Font("맑은 고딕", 9F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmMain";
            Text = "첫번째 윈앱";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnPress;
        private Label LblResult;
    }
}
