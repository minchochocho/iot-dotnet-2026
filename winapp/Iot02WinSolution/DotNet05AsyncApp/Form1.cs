namespace DotNet05AsyncApp {
    public partial class Form1 : Form {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Files(*.*)|*.*";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                TxtSource.Text = dlg.FileName;
            }
        }

        private void BtnTarget_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "All Files(*.*)|*.*";

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                // [수정] 원본 경로를 덮어쓰지 않도록 TxtTarget으로 변경했습니다.
                TxtTarget.Text = dlg.FileName;
            }
        }

        // 동기화 복사
        private void BtnSyncCopy_Click(object sender, EventArgs e)
        {
            // 비동기 복사 기능과의 일관성 및 스레드 안전성을 위해 버튼 제어는 핸들러로 이동했습니다.
            SetButtonsEnabled(false);
            try
            {
                long result = CopySync(TxtSource.Text, TxtTarget.Text);
            }
            finally
            {
                SetButtonsEnabled(true);
            }
        }

        private async void BtnAsyncCopy_Click(object sender, EventArgs e)
        {
            // 비동기화 복사
            // 비동기 작업 도중 버튼이 다시 눌리는 것을 방지하기 위해 여기서 버튼을 끕니다.
            SetButtonsEnabled(false);
            try
            {
                long result = await CopyAsync(TxtSource.Text, TxtTarget.Text);
            }
            finally
            {
                // 복사가 끝나거나 에러가 나면 버튼을 다시 켭니다.
                SetButtonsEnabled(true);
            }
        }

        // 버튼들의 활성화 상태를 한 번에 관리해 주는 편리한 메서드입니다.
        private void SetButtonsEnabled(bool isEnabled)
        {
            BtnSource.Enabled = BtnTarget.Enabled = BtnSyncCopy.Enabled = BtnAsyncCopy.Enabled = isEnabled;
        }

        private long CopySync(string srcFile, string destFile)
        {
            long totalCopied = 0;

            // 읽어오는 쪽
            using (FileStream fromStream = new FileStream(srcFile, FileMode.Open))
            {
                // 새로 쓰는(만드는) 쪽
                // using을 사용하면 close를 안해도댐
                using (FileStream toStream = new FileStream(destFile, FileMode.Create))
                {
                    // 파일 복사 시 항상 버퍼. byte[]배열로 버퍼를 만듦
                    byte[] buffer = new byte[1024];  // 1KB
                    int nRead = 0;  // 1K씩 읽어오는 횟수

                    while ((nRead = fromStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        // 데이터가 있는동안 계속 읽기
                        toStream.Write(buffer, 0, nRead);   // 1MB씩 읽기
                        totalCopied += nRead;       // 전체 복사 횟수

                        // 진행사항 상태바 표시
                        // [수정] 정수 나눗셈 문제를 해결하기 위해 (double) 형변환을 추가했습니다.
                        PrgProcess.Value = (int)(((double)totalCopied / fromStream.Length) * 100);
                    }
                }
            }
            return totalCopied;
        }

        // Task : 비동기 작업, 백그라운드작업 작업 객체
        // <long> : 
        private async Task<long> CopyAsync(string srcFile, string destFile)
        {
            long totalCopied = 0;

            // 읽어오는 쪽
            using (FileStream fromStream = new FileStream(srcFile, FileMode.Open))
            {
                // 새로 쓰는(만드는) 쪽
                // using을 사용하면 close를 안해도댐
                using (FileStream toStream = new FileStream(destFile, FileMode.Create))
                {
                    // 파일 복사 시 항상 버퍼. byte[]배열로 버퍼를 만듦
                    byte[] buffer = new byte[1024];  // 1KB
                    int nRead = 0;  // 1K씩 읽어오는 횟수

                    while ((nRead = await fromStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        // 데이터가 있는동안 계속 읽기
                        await toStream.WriteAsync(buffer, 0, nRead);   // 1MB씩 읽기
                        totalCopied += nRead;       // 전체 복사 횟수

                        // 진행사항 상태바 표시
                        // [수정] 정수 나눗셈 문제를 해결하기 위해 (double) 형변환을 추가했습니다.
                        PrgProcess.Value = (int)(((double)totalCopied / fromStream.Length) * 100);
                    }
                }
            }
            return totalCopied;
        }
    }
}