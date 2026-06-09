using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfCafeKiosk.Common;
using WpfCafeKiosk.models;

namespace WpfCafeKiosk {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        // 주문리스트 객체
        // WPF에서 사용하는 리스트. 값이 변동되면 바로 적용
        private ObservableCollection<OrderItem> orders;

        // 남은시간 처리용 필드
        private int remainSeconds = 60;
        private DispatcherTimer timer;  // WPF는 타이머를 직접 만들어 써라!

        private DatabaseHelper db;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new DatabaseHelper();

            // 메뉴 읽어오기
            LoadMenus();

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1),  // 1초마다 이벤트 발생
                IsEnabled = true
            };

            // 속성 이외의 호출이나 이벤트 추가
            timer.Tick += Timer_Tick;
            timer.Start();

            TxtRemainTime.Text = remainSeconds.ToString();

            orders = new ObservableCollection<OrderItem>();
            LstOrder.ItemsSource = orders;  // 리스트가 빈값이므로 아무변화 없음
        }

        private void LoadMenus()
        {
            MenuPanel.Children.Clear();

            // 쿼리는 여러줄문자열일때 앞뒤 공백을 추가 반드시, Sysntax Error
            string query = "SELECT menu_id, menu_name, price, image_path, category, is_sale " +
                           " FROM menu " +
                           " WHERE is_sale = 'Y' " +
                           " ORDER BY menu_id ";
            try
            {
                DataTable dt = db.Select(query);
                // MessageBox.Show(dt.Rows.Count.ToString());
                foreach (DataRow row in dt.Rows)
                {
                    MenuItemModel MenuItem = new MenuItemModel
                    {
                        MenuId = Convert.ToInt32(row["menu_id"]),
                        MenuName = row["menu_name"].ToString(),
                        Price = Convert.ToInt32(row["price"]),
                        ImagePath = row["image_path"].ToString(),
                        Category = row["category"].ToString()
                    };
                    Button btn = CreateMenuButton(MenuItem);
                    MenuPanel.Children.Add(btn);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 메뉴버튼 생성 메서드
        private Button CreateMenuButton(MenuItemModel menuItem)
        {
            Button btn = new Button
            {
                Margin = new Thickness(5),
                Padding = new Thickness(0),
                Height = 200,
                Background = Brushes.White,
                BorderThickness = new Thickness(0),
                Tag = $"{menuItem.MenuName}|{menuItem.Price}|{menuItem.ImagePath}|{menuItem.MenuId}|{menuItem.Category}"
            };

            // xaml의 materialDesign:ButtonAssist.CornerRadius="5"
            ButtonAssist.SetCornerRadius(btn, new CornerRadius(8));

            btn.Click += Menu_Click;    // 이전에 만든 Menu_Click 이벤트핸들러를 코딩으로 새로만드는 버튼 이벤트로 연결

            // 버튼 디자인 코딩 구현 시작
            Card card = new Card
            {
                UniformCornerRadius = 5,
                Padding = new Thickness(0)
            };

            Grid grid = new Grid();

            Image img = new Image { Stretch = Stretch.Fill };

            try
            {
                img.Source = new BitmapImage(new Uri(menuItem.ImagePath, UriKind.RelativeOrAbsolute));
            }
            catch
            {
                img.Source = null;
            }

            // 음료명, 가격 주변에 들어갈 Border
            Border bottomBg = new Border
            {
                Background = new SolidColorBrush(Color.FromArgb(204, 255, 255, 255)),
                Height = 42,
                VerticalAlignment = VerticalAlignment.Bottom,
                CornerRadius = new CornerRadius(0, 0, 8, 8)
            };

            StackPanel PnlText = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 0, 5)
            };

            TextBlock txtMenuName = new TextBlock
            {
                Text = menuItem.MenuName,
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.DimGray,
                TextAlignment = TextAlignment.Center
            };

            TextBlock txtPrice = new TextBlock
            {
                Text = $"{menuItem.Price:N0}원",
                FontSize = 12,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.Heavy,
                Foreground = new SolidColorBrush(Color.FromRgb(81, 99, 52))
            };

            // 메뉴명을 스택패널에 할당
            PnlText.Children.Add(txtMenuName);
            PnlText.Children.Add(txtPrice);

            // 버튼 디자인 코딩 구현 끝

            grid.Children.Add(img);         // 그리드에 이미지 할당
            grid.Children.Add(bottomBg);    // 그리드에 반투명배경 할당

            grid.Children.Add(PnlText);     // 그리드에 텍스트 할당

            btn.Content = card;     // 버튼에 그리드 할당
            card.Content = grid;    // 카드에 그리드 할당

            return btn;
        }

        // 1초마다 타이머 이벤트 발생
        private void Timer_Tick(object sender, EventArgs e) // 이벤트에다간 ?잘 안씀
        {
            if (orders.Count == 0)
            {
                TxtRemainTime.Text = "60";
                remainSeconds = 60;
                return;
            }
            remainSeconds--;

            TxtRemainTime.Text = remainSeconds.ToString();
            if (remainSeconds <= 0)
            {
                orders.Clear();
                RefreshOrderSummary();

                remainSeconds = 60;


            }
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;  // 이벤트를 발생시킨 주체를 할당

            string[] tag = btn.Tag.ToString().Split("|");

            //string menuName = tag[0];
            //int price = int.Parse(tag[1]);
            //string imagePath = tag[2];    // 태그를 메인윈도우에서 잘라 변수들을 파라미터로 보내면 변수개수에 따라 생성자 변경이 필요함
            string strTag = btn.Tag.ToString();

            // MessageBox.Show($"{price}, {name}");

            MenuOptionWindow win = new MenuOptionWindow(strTag);

            win.Owner = this;   // MainWindow가 MenuOptionWindow의 부모

            bool? result = win.ShowDialog();    // 옵션창에서 취소누르면 false, 주문담기 누르면 true

            // TODO : result가 true일때 주문감기 처리
            if (result == true)
            {
                // OrderItem item = win.SelectedOrder;

                // 주문 리스트뷰에 추가
                // MessageBox.Show($"{item.MenuName} {item.Count}개 담기! {item.TotalPrice:N0}원");
                orders.Add(win.SelectedOrder);
            }
            RefreshOrderSummary();
            remainSeconds = 60;
        }

        // xaml에서 F12클릭시 자동 생성
        private void BtnRemoveOrder_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;  // WPF, WinForms에서 중요한 개념, 이벤트를 발생시킨 주체

            OrderItem item = btn.Tag as OrderItem;  // MenuId가 아직 연동안됨

            if (item != null)
            {
                orders.Remove(item);
                RefreshOrderSummary();
            }

        }

        private void RefreshOrderSummary()
        {
            int count = orders.Sum(x => x.Count);   // Lambda 함수
            int total = orders.Sum(x => x.TotalPrice);

            TxtOrderCount.Text = $"{count}잔";
            TxtTotalPrice.Text = $"{total:N0}원";
        }

        private void BtnClearAll_Click(object sender, RoutedEventArgs e)
        {
            if (orders.Count > 0)
            {
                // MessageBox.Show("주문내역이 없습니다");
                RootDialog.IsOpen = true;
            }
            return;

        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

            orders.Clear();
            RefreshOrderSummary();

        }

        // 홈버튼 클릭이벤트 핸들러
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            TabAll.Focus();
            RefreshOrderSummary();
        }

        // 결제버튼
        private void BtnPay_Click(object sender, RoutedEventArgs e)
        {
            if (orders.Count == 0) return;

            OrderConfirmWindow win = new OrderConfirmWindow(orders);
            win.Owner = this;   // 결제창의 소유자(부모)는 MainWindow다

            bool? result = win.ShowDialog();

            if (result == true)
            {
                //
            }
            else
            {
                timer.Start();
            }

        }
    }
}