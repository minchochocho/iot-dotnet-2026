using System.Windows.Controls;

namespace WpfBasic02Navi {
    /// <summary>
    /// Page1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MenuPage : Page {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void BtnMenu01_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Sub01Page.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("/Sub02Page.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BtnMenu03_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Sub03Page.xaml", UriKind.RelativeOrAbsolute));
        }
        private void BtnMenu04_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Sub04Page.xaml", UriKind.RelativeOrAbsolute));
        }

    }
}
