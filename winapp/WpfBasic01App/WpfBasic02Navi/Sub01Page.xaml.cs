using System.Windows.Controls;

namespace WpfBasic02Navi {
    /// <summary>
    /// Sub01.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Sub01Page : Page {
        public Sub01Page()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            MediaPlayer.Source = new Uri(@".\sample01.mp4", UriKind.RelativeOrAbsolute);
            MediaPlayer.Play();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
