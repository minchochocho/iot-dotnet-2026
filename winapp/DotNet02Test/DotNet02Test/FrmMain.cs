namespace DotNet02Test {
    public partial class FrmMain : Form {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtnPress_Click(object sender, EventArgs e)
        {
            LblResult.Text = "결과 : 컴퓨터 터짐!";
            MessageBox.Show("버튼클릭", "테스트", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
        }
    }
}
