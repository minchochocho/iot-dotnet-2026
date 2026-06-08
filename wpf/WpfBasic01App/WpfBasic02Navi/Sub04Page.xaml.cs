using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace WpfBasic02Navi {
    public partial class Sub04Page : Page {
        // [수정] public 리스트는 함수 내부가 아니라, 이렇게 클래스 바로 아래에 선언해야 합니다.
        private List<Employee> employees { get; set; }

        public Sub04Page()
        {
            InitializeComponent();
        }

        // [수정] WPF의 기본 Loaded 이벤트는 TouchEventArgs가 아니라 RoutedEventArgs를 사용합니다.
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // 초기화
            Car car = new Car
            {
                Speed = 10.0,
                Color = Colors.Black
            };

            TxtSpeed.Text = car.Speed.ToString();
            ScbColor.Color = car.Color;
            TxtColor.Text = car.Color.ToString();
        }

        private void TxtColor_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Color color = (Color)ColorConverter.ConvertFromString(TxtColor.Text);
                ScbColor.Color = color;
            }
            catch (Exception)
            {
                Debug.WriteLine("변환 실패");
            }
        }

    }
}