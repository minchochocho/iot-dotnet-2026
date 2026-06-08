using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfCafeKiosk {
    /// <summary>
    /// MenuOptionWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MenuOptionWindow : Window {
        private string menuName;
        private int price;
        private string imagePath;
        private int qty = 1;    // Quentity 수량

        public MenuOptionWindow(string menuName, int price, string imagePath)
        {
            InitializeComponent();

            this.menuName = menuName;
            this.price = price;
            this.imagePath = imagePath;

            TxtMenuName.Text = menuName;
            TxtPrice.Text = $"{price:N0}원"; // N0는 천단위마다 ,찍기

            ImgMenu.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }
    }
}
