namespace WpfSmartHomeSensingApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            var faker = new Faker("ko");    // 한국어 더미 데이터
            Console.WriteLine(faker);
        }
    }
}