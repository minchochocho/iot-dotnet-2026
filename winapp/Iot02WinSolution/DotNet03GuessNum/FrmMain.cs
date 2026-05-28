namespace DotNet03GuessNum {
    public partial class FrmMain : Form {
        private int findNumber = 0;
        private int chance = 0;
        // 생성자는 되도록 손대지 않는다
        public FrmMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// BtnStart 버튼클릭 이벤트핸들러 메서드
        /// </summary>
        /// <param name="sender">이벤트를 보내준 객체</param>
        /// <param name="e">버튼자체에 필요한 매개변수 속성</param>

        private void BtnStart_Click(object sender, EventArgs e)
        {
            int inputNum = int.Parse(TxtNum.Text);

            if (inputNum == findNumber)
            {
                LblDisplay.Text = "맞히셨습니다!";
                MessageBox.Show("축하드립니다!", "게임종료", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                chance--;
                string strVal;
                if (inputNum > findNumber)
                {
                    strVal = "큼";
                }
                else
                {
                    strVal = "작음";
                }

                LblDisplay.Text = $"기회 {chance}번 찾는수보다 {strVal}";
            }
            var rand = new Random();
            findNumber = rand.Next(1, 30 + 1);
            chance = 10;
            LblDisplay.Text = "맞힐 숫자를 입력하세요.";

            MessageBox.Show("게임을 시작하지", "게임시작", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
