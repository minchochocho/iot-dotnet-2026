using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace WpfBasic02Navi {
    public class Car {
        public double Speed { get; set; }
        public Color Color { get; set; }
    }
    public partial class Sub03Page : Page {
        // [수정] public 리스트는 함수 내부가 아니라, 이렇게 클래스 바로 아래에 선언해야 합니다.
        private List<Employee> employees { get; set; }

        public Sub03Page()
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

            // 데이터컨텍스트
            // this.DataContext = car; // 전체 page에 데이터컨텍스트에 car를 지정
            GrbInfo.DataContext = car; // 그룹박스 하위에서 car이라는 

            // WinForms에서 데이터바인딩하던 방식
            // TxtSpeed.Text = car.Speed.ToString();
        }
    }
}