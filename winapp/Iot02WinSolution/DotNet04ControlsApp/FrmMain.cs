using System.ComponentModel;

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

        // 스레드 없이 진행
        private void NoThreadBtn_Click(object sender, EventArgs e)
        {
            var maximum = 100;
            var minimum = 0;
            var currValue = 0;
            TxtLog.Clear();
            PrgProcess.Minimum = minimum;
            PrgProcess.Maximum = maximum;
            PrgProcess.Value = 0;

            ThreadBtn.Enabled = false;
            NoThreadBtn.Enabled = false;
            StopBtn.Enabled = true;

            // 프로세스 진행 더미로 실행
            for (int i = 0; i <= maximum; i++)
            {
                // 내부적으로 복잡하고 시간이 많이 소요되는 작업
                currValue = i;
                PrgProcess.Value = currValue;
                // 윈도우의 경우 \n으로만으론 안됨. \r\n 둘다 사용 필요
                TxtLog.AppendText($"진행사항 : {currValue}\r\n");
                Thread.Sleep(500);
            }

            NoThreadBtn.Enabled = ThreadBtn.Enabled = true;
            StopBtn.Enabled = false;
        }
        // 스레드로 진행
        private void ThreadBtn_Click(object sender, EventArgs e)
        {
            // 진행바 초기화
            var maximum = 100;
            var minimum = 0;
            var currValue = 0;
            TxtLog.Clear();
            PrgProcess.Minimum = minimum;
            PrgProcess.Maximum = maximum;
            PrgProcess.Value = 0;

            ThreadBtn.Enabled = false;
            NoThreadBtn.Enabled = false;
            StopBtn.Enabled = true;

            // 내부처리 작업을 백그라운드워커에 이벤트로 분리
            WrkProcess.WorkerReportsProgress = true;    // 진행사항 리포트 활성화
            WrkProcess.WorkerSupportsCancellation = true;   // 백그라운드워커 중간 취소 활성화
            WrkProcess.RunWorkerAsync(null);    // 백그라운드 워커 실행! (DoWork)
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            WrkProcess.CancelAsync();   // Async 비동기로 취소
            // 백그라운드워커의 Canceld 속성값을 True로 변경
        }

        #region `백그라운드워커 이벤트핸들러`

        // 1. 백그라운드워커 첫 시작점
        private void WrkProcess_DoWork(object sender, DoWorkEventArgs e)
        {
            var maximum = 100;
            var currValue = 0.0;

            for (int i = 0; i < maximum; i++)
            {
                if (WrkProcess.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    currValue = i;
                    Thread.Sleep(100);
                    // 진행사항은 ProgressChanged 이벤트핸들러에 작성
                    WrkProcess.ReportProgress((int)(currValue * 100 / maximum));
                }
            }

        }

        // 2. 프로세스 변경사항 UI로 전달
        private void WrkProcess_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // UI 스레드에 넘길값들만 실행!
            PrgProcess.Value = e.ProgressPercentage;
            TxtLog.AppendText($"진행률 : {PrgProcess.Value}\r\n");
        }

        // 3. 프로세스가 끝난 뒤 처리
        private void WrkProcess_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                TxtLog.AppendText("작업 취소!\r\n");

            }
            else
            {
                TxtLog.AppendText("작업 완료!\r\n");
            }
            NoThreadBtn.Enabled = ThreadBtn.Enabled = true;
            StopBtn.Enabled = false;

        }



        #endregion

        #region `텍스트파일 읽고쓰기`
        private void BtnFileLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;    // 여러파일 선택여부
            dlg.Filter = "Text files(*.txt;*.cs;*.py;*.sql)|*.txt;*.cs;*.py;*.sql;|RitchText file(*.rtf)|*.rtf";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                // 확장자가 rtf면

                // UTF-8 한글깨짐. EUC-KR, UTF-8(BOM) 문제 없음
                RtbEditer.LoadFile(dlg.FileName, RichTextBoxStreamType.RichText);
                // RichTestbox 포멧으로 변경
            }
        }

        private void BtnFileSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "RitchText file(*.rtf)|*.rtf";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                RtbEditer.SaveFile(dlg.FileName, RichTextBoxStreamType.RichNoOleObjs);
            }
        }
        #endregion

        /// <summary>
        /// 폼종료할 때 종료여부 체크
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            var res = MessageBox.Show("정말 종료하시겠습니까?",
                            "종료여부",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

            if (res == DialogResult.No)
            {
                e.Cancel = true;    // 종료가 취소됨
            }
        }
    }
}