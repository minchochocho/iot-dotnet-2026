namespace DotNet05AsyncApp {
    partial class Form1 {
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
            components = new System.ComponentModel.Container();
            groupBox1 = new GroupBox();
            panel3 = new Panel();
            panel2 = new Panel();
            BtnAsyncCopy = new Button();
            BtnSyncCopy = new Button();
            panel1 = new Panel();
            BtnTarget = new Button();
            BtnSource = new Button();
            TxtTarget = new TextBox();
            TxtSource = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            PrgProcess = new ProgressBar();
            groupBox1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(panel3);
            groupBox1.Controls.Add(panel2);
            groupBox1.Controls.Add(panel1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(390, 176);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // panel3
            // 
            panel3.Controls.Add(PrgProcess);
            panel3.Location = new Point(6, 134);
            panel3.Name = "panel3";
            panel3.Size = new Size(375, 36);
            panel3.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(BtnAsyncCopy);
            panel2.Controls.Add(BtnSyncCopy);
            panel2.Location = new Point(6, 96);
            panel2.Name = "panel2";
            panel2.Size = new Size(375, 32);
            panel2.TabIndex = 0;
            // 
            // BtnAsyncCopy
            // 
            BtnAsyncCopy.Location = new Point(192, 4);
            BtnAsyncCopy.Name = "BtnAsyncCopy";
            BtnAsyncCopy.Size = new Size(97, 23);
            BtnAsyncCopy.TabIndex = 0;
            BtnAsyncCopy.Text = "비동기화 복사";
            BtnAsyncCopy.UseVisualStyleBackColor = true;
            BtnAsyncCopy.Click += BtnAsyncCopy_Click;
            // 
            // BtnSyncCopy
            // 
            BtnSyncCopy.Location = new Point(88, 4);
            BtnSyncCopy.Name = "BtnSyncCopy";
            BtnSyncCopy.Size = new Size(97, 23);
            BtnSyncCopy.TabIndex = 0;
            BtnSyncCopy.Text = "동기화 복사";
            BtnSyncCopy.UseVisualStyleBackColor = true;
            BtnSyncCopy.Click += BtnSyncCopy_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(BtnTarget);
            panel1.Controls.Add(BtnSource);
            panel1.Controls.Add(TxtTarget);
            panel1.Controls.Add(TxtSource);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(6, 22);
            panel1.Name = "panel1";
            panel1.Size = new Size(375, 68);
            panel1.TabIndex = 0;
            // 
            // BtnTarget
            // 
            BtnTarget.Location = new Point(313, 35);
            BtnTarget.Name = "BtnTarget";
            BtnTarget.Size = new Size(37, 23);
            BtnTarget.TabIndex = 2;
            BtnTarget.Text = "...";
            BtnTarget.UseVisualStyleBackColor = true;
            BtnTarget.Click += BtnTarget_Click;
            // 
            // BtnSource
            // 
            BtnSource.Location = new Point(313, 11);
            BtnSource.Name = "BtnSource";
            BtnSource.Size = new Size(37, 23);
            BtnSource.TabIndex = 2;
            BtnSource.Text = "...";
            BtnSource.UseVisualStyleBackColor = true;
            BtnSource.Click += BtnSource_Click;
            // 
            // TxtTarget
            // 
            TxtTarget.Location = new Point(62, 36);
            TxtTarget.Name = "TxtTarget";
            TxtTarget.ReadOnly = true;
            TxtTarget.Size = new Size(248, 23);
            TxtTarget.TabIndex = 1;
            // 
            // TxtSource
            // 
            TxtSource.Location = new Point(62, 10);
            TxtSource.Name = "TxtSource";
            TxtSource.ReadOnly = true;
            TxtSource.Size = new Size(248, 23);
            TxtSource.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 39);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 0;
            label3.Text = "타겟";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 13);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 0;
            label2.Text = "소스";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // PrgProcess
            // 
            PrgProcess.Location = new Point(3, 7);
            PrgProcess.Name = "PrgProcess";
            PrgProcess.Size = new Size(369, 23);
            PrgProcess.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(414, 200);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private Panel panel1;
        private Panel panel3;
        private Panel panel2;
        private Button BtnAsyncCopy;
        private Button BtnSyncCopy;
        private Button BtnTarget;
        private Button BtnSource;
        private TextBox TxtTarget;
        private TextBox TxtSource;
        private Label label3;
        private Label label2;
        private Label label1;
        private ContextMenuStrip contextMenuStrip1;
        private ProgressBar PrgProcess;
    }
}
