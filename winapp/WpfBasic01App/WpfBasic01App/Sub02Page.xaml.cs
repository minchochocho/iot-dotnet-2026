using System.Windows;
using System.Windows.Controls;

namespace WpfBasic01App {
    /// <summary>
    /// Sub02Page.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Sub02Page : Page {
        // [수정] public 리스트는 함수 내부가 아니라, 이렇게 클래스 바로 아래에 선언해야 합니다.
        private List<Employee> employees { get; set; }

        public Sub02Page()
        {
            InitializeComponent();
        }

        // [수정] WPF의 기본 Loaded 이벤트는 TouchEventArgs가 아니라 RoutedEventArgs를 사용합니다.
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // [수정] 여기서는 public을 빼고, 위에서 만든 리스트에 데이터만 채워줍니다.
            employees = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "김철수",
                    Department = "개발팀",
                    Salary = 4200,
                    HireDate = new DateTime(2021, 3, 15),
                    IsActive = true
                },
                new Employee
                {
                    Id = 2,
                    Name = "이영희",
                    Department = "인사팀",
                    Salary = 5100,
                    HireDate = new DateTime(2019, 7, 1),
                    IsActive = true
                },
                new Employee
                {
                    Id = 3,
                    Name = "박민수",
                    Department = "영업팀",
                    Salary = 3300,
                    HireDate = new DateTime(2023, 1, 10),
                    IsActive = false
                },
                new Employee
                {
                    Id = 4,
                    Name = "최지은",
                    Department = "디자인팀",
                    Salary = 6200,
                    HireDate = new DateTime(2018, 11, 20),
                    IsActive = true
                },
                new Employee
                {
                    Id = 5,
                    Name = "홍길동",
                    Department = "경영팀",
                    Salary = 9000,
                    HireDate = new DateTime(2010, 1, 1),
                    IsActive = true
                }
            };

        }
    }
}