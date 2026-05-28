namespace DotNet06DbBooksApp {
    partial class FrmBook {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBook));
            DgvBooks = new DataGridView();
            BtnLoad = new MaterialSkin.Controls.MaterialButton();
            ((System.ComponentModel.ISupportInitialize)DgvBooks).BeginInit();
            SuspendLayout();
            // 
            // DgvBooks
            // 
            DgvBooks.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgvBooks.Location = new Point(18, 83);
            DgvBooks.Name = "DgvBooks";
            DgvBooks.Size = new Size(762, 308);
            DgvBooks.TabIndex = 0;
            // 
            // BtnLoad
            // 
            BtnLoad.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BtnLoad.AutoSize = false;
            BtnLoad.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnLoad.CharacterCasing = MaterialSkin.Controls.MaterialButton.CharacterCasingEnum.Title;
            BtnLoad.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            BtnLoad.Depth = 0;
            BtnLoad.ForeColor = SystemColors.ActiveCaption;
            BtnLoad.HighEmphasis = true;
            BtnLoad.Icon = null;
            BtnLoad.Location = new Point(651, 400);
            BtnLoad.Margin = new Padding(4, 6, 4, 6);
            BtnLoad.MouseState = MaterialSkin.MouseState.HOVER;
            BtnLoad.Name = "BtnLoad";
            BtnLoad.NoAccentTextColor = Color.Empty;
            BtnLoad.Size = new Size(129, 36);
            BtnLoad.TabIndex = 1;
            BtnLoad.Text = "Load";
            BtnLoad.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            BtnLoad.UseAccentColor = false;
            BtnLoad.UseVisualStyleBackColor = true;
            BtnLoad.Click += BtnLoad_Click;
            // 
            // FrmBook
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(BtnLoad);
            Controls.Add(DgvBooks);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(490, 340);
            Name = "FrmBook";
            Text = "MySQL Books";
            Load += FrmBook_Load;
            ((System.ComponentModel.ISupportInitialize)DgvBooks).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView DgvBooks;
        private MaterialSkin.Controls.MaterialButton BtnLoad;
    }
}
