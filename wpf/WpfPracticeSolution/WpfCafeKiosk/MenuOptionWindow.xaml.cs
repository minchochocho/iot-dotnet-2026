using System.Windows;
using System.Windows.Media.Imaging;
using WpfCafeKiosk.models;
// 네임스페이스가 다르면 using문으로 import 해야

namespace WpfCafeKiosk {
    /// <summary>
    /// MenuOptionWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MenuOptionWindow : Window {
        private string menuName;
        private int price;
        private string imagePath;
        private int menuId; // 메뉴아이디 추가
        private int qty = 1;    // Quentity 수량
        private string[] tags;

        public OrderItem SelectedOrder { get; set; }

        // 변수가 추가될때마다 파라미터를 계속 늘리는 문제
        //public MenuOptionWindow(string menuName, int price, string imagePath, int menuId)
        //{
        //    InitializeComponent();

        //    this.menuName = menuName;
        //    this.price = price;
        //    this.imagePath = imagePath;

        //    TxtMenuName.Text = menuName;
        //    TxtPrice.Text = $"{price:N0}원"; // N0는 천단위마다 ,찍기

        //    ImgMenu.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
        //    this.menuId = menuId;
        //}

        public MenuOptionWindow(string? strTag)
        {
            InitializeComponent();  // WPF 창에서 생성자 필수!!

            tags = strTag.Split('|');

            this.menuName = tags[0];
            this.price = Convert.ToInt32(tags[1]);
            this.imagePath = tags[2];
            this.menuId = Convert.ToInt32(tags[3]);


            TxtMenuName.Text = menuName;
            TxtPrice.Text = $"{price:N0}원"; // N0는 천단위마다 ,찍기

            ImgMenu.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
            this.menuId = menuId;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        private void BtnMinus_Click(object sender, RoutedEventArgs e)
        {
            if (qty <= 1) return;   // 수량은 1이하로 내려갈 필요없음

            qty--;
            TxtQty.Text = qty.ToString();
        }

        private void BtnPlus_Click(object sender, RoutedEventArgs e)
        {
            qty++;  // 
            TxtQty.Text = qty.ToString();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            SelectedOrder = new OrderItem
            {
                MenuId = menuId,
                MenuName = menuName,
                Price = price,
                Count = qty,
                TotalPrice = price * qty

            };
            DialogResult = true;
            Close();
        }
    }
}
