using MahApps.Metro.Controls;
using System.Data;
using System.Windows;

namespace WpfBasic04DbApp {
    public partial class MainWindow : MetroWindow {
        // 1. 클래스 내부 멤버 변수로 databaseHelper를 정확히 선언합니다.
        private DatabaseHelper databaseHelper;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 2. 위에서 선언한 변수 이름(databaseHelper)과 똑같이 맞춰서 초기화합니다.
            databaseHelper = new DatabaseHelper();

            // 3. 화면이 로드될 때 데이터를 불러오도록 호출해 줍니다.
            LoadData();
        }

        private void LoadData()
        {
            string query = "SELECT b.book_idx, b.author, b.div_code, d.div_name, b.book_name, b.release_dt, b.isbn, b.price " +
                           "  FROM books AS b " +
                           "  JOIN division AS d ON b.div_code = d.div_code " +
                           " ORDER BY b.book_idx ";

            // 4. 이제 복잡한 쿼리 결과를 정상적으로 받아옵니다.
            DataTable dt = databaseHelper.SelectBook(query);
            GrdBooks.ItemsSource = dt.DefaultView;
        }
    }
}