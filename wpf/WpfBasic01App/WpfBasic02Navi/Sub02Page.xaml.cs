using System.Windows;
using System.Windows.Controls;

namespace WpfBasic02Navi {
    public partial class Sub02Page : Page {
        public List<Employee> Employees { get; set; }  // employee 컬렉션 선언
        public Employee SelectedEmployee { get; set; }

        public List<string> Departments { get; set; }
        public string SelectedDepartment { get; set; }
        public Sub02Page()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Departments = new List<string>
            {
                "개발팀",
                "영업팀",
                "인사팀",
                "디자인팀",
                "경영팀"
            };
            // 초기화. DB에서 불러오는 것과 유사
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

            // 데이터그리드 할당
            this.DataContext = this;    // 코드비하인드의 모든 바인딩 객체를 화면상에서 사용하겠다라는 선언
            DgrEmployees.ItemsSource = employees;

        }


    }
}