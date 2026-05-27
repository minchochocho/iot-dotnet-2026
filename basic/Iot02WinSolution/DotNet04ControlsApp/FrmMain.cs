namespace DotNet04ControlsApp {
    public partial class FrmMain : Form {
        public FrmMain()
        {
            InitializeComponent();
        }

        // 폼로드 이벤트 핸들러
        private void FrmMain_Load(object sender, EventArgs e)
        {
            var Fonts = FontFamily.Families;    // OS에 설치된 폰트 리스트
            foreach (var font in Fonts)
            {
                // Otems -> Items로 오타 수정
                CboFonts.Items.Add(font.Name);
            }
            TxtResult.Text = "현재 글씨체 / Fonts";
        }

        private void ChkBold_CheckedChanged(object sender, EventArgs e)
        {
            ChangeFontStyle();
        }

        private void ChkItalic_CheckedChanged(object sender, EventArgs e)
        {
            ChangeFontStyle();

        }

        private void CboFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeFontStyle();
        }

        // 폰트 글자체, 굵게, 이텔릭 변경 메서드
        private void ChangeFontStyle()
        {
            if (CboFonts.SelectedIndex < 0)
            {
                return; // 콤보박스 아무것도 선택안됨
            }
            FontStyle style = FontStyle.Regular;    // 처음에는 기본 글자(볼드, 이탤릭x)

            if (ChkBold.Checked)
            {
                style |= FontStyle.Bold;    // or, 볼드체로 변경
            }
            if (ChkItalic.Checked)
            {
                style |= FontStyle.Italic;
            }
            TxtResult.Font = new Font(CboFonts.SelectedItem as string, 10, style);
        }

        // 모달버튼 클릭이벤트 핸들러
        // 모달창이 닫히기전에 부모창을 제어할 수 없음
        private void BtnModal_Click(object sender, EventArgs e)
        {
            FrmSub frmSub = new FrmSub();
            frmSub.Text = "모달창";
            frmSub.BackColor = Color.Red;
            frmSub.ShowDialog();    // 모달창으로 오픈
        }

        // 모달리스버튼 클릭이벤트 핸들러
        // 모달창이 닫히기전에 부모창을 제어할 수 있음
        private void BtnModaless_Click(object sender, EventArgs e)
        {
            FrmSub frmSub = new FrmSub();
            frmSub.Text = "모달리스창";
            frmSub.BackColor = Color.GreenYellow;
            frmSub.StartPosition = FormStartPosition.Manual;
            // 메인 창(this)의 중앙 좌표 계산
            int centerX = this.Left + (this.Width - frmSub.Width) / 2;
            int centerY = this.Top + (this.Height - frmSub.Height) / 2;

            // 계산된 좌표를 Point로 지정
            frmSub.Location = new Point(centerX, centerY);
            frmSub.Show(this);  // this -> FrmMain
        }

        private void BtnMsgBox_Click(object sender, EventArgs e)
        {
            // 기본적인 메시지박스
            // 파라미터 : 메시지, 타이틀, 버튼종류, 아이콘 종류
            MessageBox.Show(TxtResult.Text, "김민주 바보", MessageBoxButtons.OK);
        }

        // 다이얼로그창 띄우기
        private void BtnDialog_Click(object sender, EventArgs e)
        {
            // DlgOpenFile.ShowDialog(this);
            if (DlgOpenFile.ShowDialog(this) == DialogResult.OK)
            {
                MessageBox.Show($"선택한 파일은 {DlgOpenFile.FileName} 입니다.");
            }
        }

        private void TrkStatus_Scroll(object sender, EventArgs e)
        {
            PrgStatus.Value = TrkStatus.Value;
        }

        // 트리뷰 내요을 리스트뷰에 표현 메서드
        private void TreeToList()
        {
            LvmDummy.Items.Clear();
            foreach (TreeNode node in TvwDummy.Nodes)
            {
                TreeToList(node);
            }
        }

        private void TreeToList(TreeNode node)
        {
            LvmDummy.Items.Add(
                new ListViewItem(
                    new String[] { node.Text, node.FullPath.Count(f => f == '\\').ToString() }
                    )
                );
            foreach (TreeNode subNode in node.Nodes)
            {
                TreeToList(subNode);    // 재귀호출
            }
        }

        private void BtnAddRoot_Click(object sender, EventArgs e)
        {
            var random = new Random();
            TvwDummy.Nodes.Add(random.Next().ToString());
            TreeToList();
        }

        private void BtnAddNode_Click(object sender, EventArgs e)
        {
            if (TvwDummy.SelectedNode == null)
            {
                MessageBox.Show("노드를 선택하세요", "경고", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // 메서드 탈출
            }
            var random = new Random();
            TreeNode childeNode = new TreeNode(random.Next().ToString());
            childeNode.ImageIndex = 1;
            childeNode.SelectedImageIndex = 1; // 선택되었을 때도 1번 아이콘 유지

            // 원래 코드 대신 세팅된 childeNode 객체를 추가하도록 수정
            TvwDummy.SelectedNode.Nodes.Add(childeNode);

            TvwDummy.ExpandAll(); // 하위노드 전부 확장
            TreeToList();

        }

        private void BtnLoadImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "이미지열기";
            dlg.Filter = "Image Files(*.bmp;*.png;*.jpg)|*.bmp;*.png;*.jpg";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                PicImg.Image = Bitmap.FromFile(dlg.FileName);
                PicImg.SizeMode = PictureBoxSizeMode.StretchImage;  // 전체이미지 표시
            }
        }

        // 픽쳐박스 컨트롤 클릭이벤트 핸들러
        private void PicImg_Click(object sender, EventArgs e)
        {
            if (PicImg.SizeMode == PictureBoxSizeMode.CenterImage)
            {
                PicImg.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                PicImg.SizeMode = PictureBoxSizeMode.CenterImage;

            }
        }
    }
}