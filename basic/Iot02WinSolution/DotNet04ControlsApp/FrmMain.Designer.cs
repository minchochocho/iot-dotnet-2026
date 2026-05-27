namespace DotNet04ControlsApp {
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            groupBox4 = new GroupBox();
            BtnLoadImg = new Button();
            PicImg = new PictureBox();
            groupBox5 = new GroupBox();
            groupBox6 = new GroupBox();
            groupBox2 = new GroupBox();
            panel5 = new Panel();
            PrgStatus = new ProgressBar();
            panel4 = new Panel();
            TrkStatus = new TrackBar();
            groupBox1 = new GroupBox();
            panel3 = new Panel();
            BtnDialog = new Button();
            BtnMsgBox = new Button();
            BtnModaless = new Button();
            BtnModal = new Button();
            panel2 = new Panel();
            TxtResult = new TextBox();
            panel1 = new Panel();
            label1 = new Label();
            ChkItalic = new CheckBox();
            CboFonts = new ComboBox();
            ChkBold = new CheckBox();
            groupBox3 = new GroupBox();
            panel8 = new Panel();
            BtnAddNode = new Button();
            BtnAddRoot = new Button();
            panel7 = new Panel();
            LvmDummy = new ListView();
            ImyDummy = new ImageList(components);
            panel6 = new Panel();
            TvwDummy = new TreeView();
            DlgOpenFile = new OpenFileDialog();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PicImg).BeginInit();
            groupBox2.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TrkStatus).BeginInit();
            groupBox1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            groupBox3.SuspendLayout();
            panel8.SuspendLayout();
            panel7.SuspendLayout();
            panel6.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(BtnLoadImg);
            groupBox4.Controls.Add(PicImg);
            groupBox4.Location = new Point(402, 12);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(380, 310);
            groupBox4.TabIndex = 0;
            groupBox4.TabStop = false;
            groupBox4.Text = "픽쳐 박스";
            // 
            // BtnLoadImg
            // 
            BtnLoadImg.Location = new Point(299, 278);
            BtnLoadImg.Name = "BtnLoadImg";
            BtnLoadImg.Size = new Size(75, 23);
            BtnLoadImg.TabIndex = 1;
            BtnLoadImg.Text = "이미지";
            BtnLoadImg.UseVisualStyleBackColor = true;
            BtnLoadImg.Click += BtnLoadImg_Click;
            // 
            // PicImg
            // 
            PicImg.Location = new Point(6, 22);
            PicImg.Name = "PicImg";
            PicImg.Size = new Size(368, 250);
            PicImg.TabIndex = 0;
            PicImg.TabStop = false;
            PicImg.Click += PicImg_Click;
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox5.Location = new Point(792, 12);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(380, 537);
            groupBox5.TabIndex = 0;
            groupBox5.TabStop = false;
            groupBox5.Text = "텍스트 에디터";
            // 
            // groupBox6
            // 
            groupBox6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox6.Location = new Point(402, 328);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(380, 221);
            groupBox6.TabIndex = 0;
            groupBox6.TabStop = false;
            groupBox6.Text = "스레드";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(panel5);
            groupBox2.Controls.Add(panel4);
            groupBox2.Location = new Point(12, 178);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(380, 144);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "트랙바, 진행바";
            // 
            // panel5
            // 
            panel5.Controls.Add(PrgStatus);
            panel5.Location = new Point(6, 85);
            panel5.Name = "panel5";
            panel5.Size = new Size(368, 50);
            panel5.TabIndex = 3;
            // 
            // PrgStatus
            // 
            PrgStatus.Location = new Point(3, 16);
            PrgStatus.Name = "PrgStatus";
            PrgStatus.Size = new Size(362, 23);
            PrgStatus.TabIndex = 10;
            PrgStatus.Value = 10;
            // 
            // panel4
            // 
            panel4.Controls.Add(TrkStatus);
            panel4.Location = new Point(6, 22);
            panel4.Name = "panel4";
            panel4.Size = new Size(368, 57);
            panel4.TabIndex = 2;
            // 
            // TrkStatus
            // 
            TrkStatus.Location = new Point(3, 7);
            TrkStatus.Maximum = 100;
            TrkStatus.Name = "TrkStatus";
            TrkStatus.Size = new Size(362, 45);
            TrkStatus.TabIndex = 9;
            TrkStatus.Scroll += TrkStatus_Scroll;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(panel3);
            groupBox1.Controls.Add(panel2);
            groupBox1.Controls.Add(panel1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(380, 160);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "기본 컨트롤";
            // 
            // panel3
            // 
            panel3.Controls.Add(BtnDialog);
            panel3.Controls.Add(BtnMsgBox);
            panel3.Controls.Add(BtnModaless);
            panel3.Controls.Add(BtnModal);
            panel3.Location = new Point(6, 116);
            panel3.Name = "panel3";
            panel3.Size = new Size(368, 35);
            panel3.TabIndex = 7;
            // 
            // BtnDialog
            // 
            BtnDialog.Location = new Point(280, 6);
            BtnDialog.Name = "BtnDialog";
            BtnDialog.Size = new Size(75, 23);
            BtnDialog.TabIndex = 8;
            BtnDialog.Text = "...";
            BtnDialog.UseVisualStyleBackColor = true;
            BtnDialog.Click += BtnDialog_Click;
            // 
            // BtnMsgBox
            // 
            BtnMsgBox.Location = new Point(192, 6);
            BtnMsgBox.Name = "BtnMsgBox";
            BtnMsgBox.Size = new Size(75, 23);
            BtnMsgBox.TabIndex = 7;
            BtnMsgBox.Text = "메시지 창";
            BtnMsgBox.UseVisualStyleBackColor = true;
            BtnMsgBox.Click += BtnMsgBox_Click;
            // 
            // BtnModaless
            // 
            BtnModaless.Location = new Point(104, 6);
            BtnModaless.Name = "BtnModaless";
            BtnModaless.Size = new Size(75, 23);
            BtnModaless.TabIndex = 6;
            BtnModaless.Text = "모달리스";
            BtnModaless.UseVisualStyleBackColor = true;
            BtnModaless.Click += BtnModaless_Click;
            // 
            // BtnModal
            // 
            BtnModal.Location = new Point(16, 6);
            BtnModal.Name = "BtnModal";
            BtnModal.Size = new Size(75, 23);
            BtnModal.TabIndex = 5;
            BtnModal.Text = "모달";
            BtnModal.UseVisualStyleBackColor = true;
            BtnModal.Click += BtnModal_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(TxtResult);
            panel2.Location = new Point(6, 69);
            panel2.Name = "panel2";
            panel2.Size = new Size(368, 35);
            panel2.TabIndex = 6;
            // 
            // TxtResult
            // 
            TxtResult.Location = new Point(16, 6);
            TxtResult.Name = "TxtResult";
            TxtResult.Size = new Size(339, 23);
            TxtResult.TabIndex = 4;
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Controls.Add(ChkItalic);
            panel1.Controls.Add(CboFonts);
            panel1.Controls.Add(ChkBold);
            panel1.Location = new Point(6, 22);
            panel1.Name = "panel1";
            panel1.Size = new Size(368, 35);
            panel1.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 9);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 0;
            label1.Text = "폰트";
            // 
            // ChkItalic
            // 
            ChkItalic.AutoSize = true;
            ChkItalic.Location = new Point(299, 8);
            ChkItalic.Name = "ChkItalic";
            ChkItalic.Size = new Size(62, 19);
            ChkItalic.TabIndex = 3;
            ChkItalic.Text = "이텔릭";
            ChkItalic.UseVisualStyleBackColor = true;
            ChkItalic.CheckedChanged += ChkItalic_CheckedChanged;
            // 
            // CboFonts
            // 
            CboFonts.FormattingEnabled = true;
            CboFonts.Location = new Point(53, 6);
            CboFonts.Name = "CboFonts";
            CboFonts.Size = new Size(188, 23);
            CboFonts.TabIndex = 1;
            CboFonts.SelectedIndexChanged += CboFonts_SelectedIndexChanged;
            // 
            // ChkBold
            // 
            ChkBold.AutoSize = true;
            ChkBold.Location = new Point(247, 8);
            ChkBold.Name = "ChkBold";
            ChkBold.Size = new Size(50, 19);
            ChkBold.TabIndex = 2;
            ChkBold.Text = "굵게";
            ChkBold.UseVisualStyleBackColor = true;
            ChkBold.CheckedChanged += ChkBold_CheckedChanged;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox3.Controls.Add(panel8);
            groupBox3.Controls.Add(panel7);
            groupBox3.Controls.Add(panel6);
            groupBox3.Location = new Point(12, 328);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(380, 221);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "트리, 리스트 뷰";
            // 
            // panel8
            // 
            panel8.Controls.Add(BtnAddNode);
            panel8.Controls.Add(BtnAddRoot);
            panel8.Location = new Point(6, 173);
            panel8.Name = "panel8";
            panel8.Size = new Size(368, 42);
            panel8.TabIndex = 1;
            // 
            // BtnAddNode
            // 
            BtnAddNode.Location = new Point(290, 10);
            BtnAddNode.Name = "BtnAddNode";
            BtnAddNode.Size = new Size(75, 23);
            BtnAddNode.TabIndex = 14;
            BtnAddNode.Text = "노드추가";
            BtnAddNode.UseVisualStyleBackColor = true;
            BtnAddNode.Click += BtnAddNode_Click;
            // 
            // BtnAddRoot
            // 
            BtnAddRoot.Location = new Point(209, 10);
            BtnAddRoot.Name = "BtnAddRoot";
            BtnAddRoot.Size = new Size(75, 23);
            BtnAddRoot.TabIndex = 13;
            BtnAddRoot.Text = "루트추가";
            BtnAddRoot.UseVisualStyleBackColor = true;
            BtnAddRoot.Click += BtnAddRoot_Click;
            // 
            // panel7
            // 
            panel7.Controls.Add(LvmDummy);
            panel7.Location = new Point(173, 22);
            panel7.Name = "panel7";
            panel7.Size = new Size(201, 145);
            panel7.TabIndex = 0;
            // 
            // LvmDummy
            // 
            LvmDummy.GroupImageList = ImyDummy;
            LvmDummy.LargeImageList = ImyDummy;
            LvmDummy.Location = new Point(3, 3);
            LvmDummy.Name = "LvmDummy";
            LvmDummy.ShowGroups = false;
            LvmDummy.Size = new Size(195, 139);
            LvmDummy.SmallImageList = ImyDummy;
            LvmDummy.TabIndex = 12;
            LvmDummy.UseCompatibleStateImageBehavior = false;
            LvmDummy.View = View.SmallIcon;
            // 
            // ImyDummy
            // 
            ImyDummy.ColorDepth = ColorDepth.Depth32Bit;
            ImyDummy.ImageStream = (ImageListStreamer)resources.GetObject("ImyDummy.ImageStream");
            ImyDummy.TransparentColor = Color.Transparent;
            ImyDummy.Images.SetKeyName(0, "folder.png");
            ImyDummy.Images.SetKeyName(1, "file.png");
            // 
            // panel6
            // 
            panel6.Controls.Add(TvwDummy);
            panel6.Location = new Point(6, 22);
            panel6.Name = "panel6";
            panel6.Size = new Size(161, 145);
            panel6.TabIndex = 0;
            // 
            // TvwDummy
            // 
            TvwDummy.ImageIndex = 0;
            TvwDummy.ImageList = ImyDummy;
            TvwDummy.Location = new Point(3, 3);
            TvwDummy.Name = "TvwDummy";
            TvwDummy.SelectedImageIndex = 0;
            TvwDummy.Size = new Size(155, 139);
            TvwDummy.TabIndex = 11;
            // 
            // DlgOpenFile
            // 
            DlgOpenFile.FileName = "파일을 선택해주세요";
            DlgOpenFile.Title = "텍스트 파일 열기";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 561);
            Controls.Add(groupBox6);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "컨트롤 예제";
            Load += FrmMain_Load;
            groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PicImg).EndInit();
            groupBox2.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TrkStatus).EndInit();
            groupBox1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox3.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel6.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private Panel panel1;
        private Label label1;
        private ComboBox CboFonts;
        private CheckBox ChkItalic;
        private CheckBox ChkBold;
        private TextBox TxtResult;
        private Panel panel3;
        private Panel panel2;
        private Button BtnDialog;
        private Button BtnMsgBox;
        private Button BtnModaless;
        private Button BtnModal;
        private GroupBox groupBox3;
        private OpenFileDialog DlgOpenFile;
        private Panel panel5;
        private ProgressBar PrgStatus;
        private Panel panel4;
        private TrackBar TrkStatus;
        private Panel panel7;
        private ListView LvmDummy;
        private Panel panel6;
        private TreeView TvwDummy;
        private Panel panel8;
        private Button BtnAddNode;
        private Button BtnAddRoot;
        private ImageList ImyDummy;
        private Button BtnLoadImg;
        private PictureBox PicImg;
    }
}
