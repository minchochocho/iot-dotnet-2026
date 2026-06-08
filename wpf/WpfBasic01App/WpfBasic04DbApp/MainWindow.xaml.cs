using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MySqlConnector;
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

            LoadComboBoxData();

            // 3. 화면이 로드될 때 데이터를 불러오도록 호출해 줍니다.
            LoadData();
        }

        private void LoadComboBoxData()
        {
            string query = "SELECT div_code, div_name FROM division";

            DataTable dt = databaseHelper.SelectBook(query);
            CboDivCode.ItemsSource = dt.DefaultView;
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

        /// <summary>
        /// 윈폼 형태 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdBooks_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            try
            {
                if (GrdBooks.SelectedItems.Count == 1)
                {
                    // 데이터그리드에 한 아이템을 선택했을때
                    var item = GrdBooks.SelectedItems[0] as DataRowView;

                    // MessageBox.Show(item.Row["book_idx"].ToString());
                    NudBookIdx.Value = Convert.ToDouble(item.Row["book_idx"]);
                    TxtAuthor.Text = Convert.ToString(item.Row["author"]);

                    TxtBookName.Text = Convert.ToString(item.Row["book_name"]);
                    DtpReleaseDt.Text = Convert.ToString(item.Row["release_dt"]);
                    TxtIsbn.Text = Convert.ToString(item.Row["isbn"]);
                    TxtPrice.Text = Convert.ToString(item.Row["price"]);


                    // 콤보박스 선택
                    CboDivCode.SelectedValue = Convert.ToString(item.Row["div_code"]);

                    SbiResMsg.Content = $"{Convert.ToInt32(item.Row["book_idx"])}번 데이터로드 완료";

                }
            }
            catch (Exception ex)
            {
                SbiResMsg.Content = $"데이터로드 오류 : {ex.Message}";
            }


            // wpf 메인윈도우 위에 올라가는 메시지박스
            // 비동기로 처리하면 Fadeout이 제대로 진행
            // await this.ShowMessageAsync($"책상세", "데이터 상세 완료");
        }


        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // 신규 저장과 수정을 구분
            // 신규 : book_idx 데이터 없음
            try
            {
                string author = TxtAuthor.Text.Trim();
                string bookName = TxtBookName.Text.Trim();
                string isbn = TxtIsbn.Text.Trim();
                string divCode = Convert.ToString(CboDivCode.SelectedValue);

                // Validation Check
                if (string.IsNullOrEmpty(author) || string.IsNullOrEmpty(bookName) || string.IsNullOrEmpty(divCode))
                {
                    await this.ShowMessageAsync("입력오류", "필수값을 입력하세요");
                    return;
                }

                DateTime releaseDt = DateTime.Parse(DtpReleaseDt.Text); // 예외발생
                // TryParse(가져올값 변수, out 담을변수) 메서드
                if (!DateTime.TryParse(DtpReleaseDt.Text, out DateTime releseDt))    // 잘처리하면 True, 실패하면 False 우린 실패할때를 봐야 하니깐 !붙이기
                {
                    await this.ShowMessageAsync("입력오류", "날짜형식이 올바르지 않습니다.");
                }

                // 가격도 TryParse
                if (!int.TryParse(TxtPrice.Text, out int price))
                {
                    await this.ShowMessageAsync("입력오류", "가격은 숫자로 입력하세요.");
                }

                int bookIdx = Convert.ToInt32(NudBookIdx.Value); // double to int

                string query;

                if (bookIdx == 0)    // INSERT
                {
                    // 신규 저장 기초방법
                    //query = "INSERT INTO books" +
                    //    " (author, div_code, book_name, release_dt, isbn, price)" +
                    //    " VALUES " +
                    //    $"('{author}', '{divCode}', '{bookName}', '{releaseDt:yyyy-MM-dd}', '{isbn}', {price})";

                    query =
                       "INSERT INTO books" +
                       " (author, div_code, book_name, release_dt, isbn, price)" +
                       " VALUES " +
                       " (@author, @div_code, @book_name, @release_dt, @isbn, @price)";

                    databaseHelper.Execute(query,
                        new MySqlParameter("@author", author),
                        new MySqlParameter("@div_code", divCode),
                        new MySqlParameter("@book_name", bookName),
                        new MySqlParameter("@release_dt", releaseDt.ToString("yyyy-MM-dd")),
                        new MySqlParameter("@isbn", isbn),
                        new MySqlParameter("@price", price)
                        );

                    SbiResMsg.Content = "신규도서가 저장되었습니다.";
                }

                else                // UPDATE
                {
                    query = $"UPDATE books" +
                            $"   SET author = '{author}'" +
                            $" , div_code = '{divCode}'" +
                            $" , book_name = '{bookName}'" +
                            $" , release_dt = '{releaseDt:yyyy-MM-dd}'" +
                            $" , isbn = '{isbn}'" +
                            $" , price = {price}" +
                            $" WHERE book_idx = {bookIdx}";

                    databaseHelper.Execute(query);

                    SbiResMsg.Content = $"{bookIdx}번 도서정보가 수정되었습니다.";

                }
                ClearInputs();  // 책 상세 입력컨트롤에 들어가있는 데이터를 전부 초기화

                LoadData(); // 데이터 재조회
            }

            catch (Exception ex)
            {
                SbiResMsg.Content = $"데이터 저장 오류 : {ex.Message}";
            }

        }

        private void ClearInputs()
        {
            NudBookIdx.Value = 0;
            TxtAuthor.Text = string.Empty;
            TxtBookName.Text = string.Empty;
            DtpReleaseDt.Text = string.Empty;
            TxtIsbn.Text = string.Empty;
            TxtPrice.Text = ""; // == string.Empty
            CboDivCode.SelectedIndex = -1;  // 콤보박스 선택값 없애기
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
            SbiResMsg.Content = $"신규 책정보를 입력하세요";
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int bookIdx = Convert.ToInt32(NudBookIdx.Value);

                if (bookIdx <= 0)
                {
                    SbiResMsg.Content = "삭제할 도서를 선택하세요";
                    return;
                }

                // 다이얼로그로 삭제여부를 확인
                MessageDialogResult res = await this.ShowMessageAsync("삭제 확인", $"{bookIdx}번 도서를 삭제하시겠습니까?", MessageDialogStyle.AffirmativeAndNegative);   // yes/no

                if (res != MessageDialogResult.Affirmative)
                {
                    return; // 확인이 아니면 
                }

                // 가장 초보적인 방법
                // string query = "DELETE FROM books WHERE book_idx = "
                string query = "DELETE FROM books WHERE book_idx = @book_idx";

                int resultRow = databaseHelper.Execute(query,
                                    new MySqlParameter("@book_idx", bookIdx)
                    );

                if (resultRow > 0)
                {
                    SbiResMsg.Content = $"{bookIdx}번 도서가 삭제되었습니다";

                    ClearInputs();
                    LoadData();
                }
                else
                {
                    SbiResMsg.Content = "삭제된 도서가 없습니다";
                }

            }
            catch (Exception ex)
            {
                SbiResMsg.Content = $"데이터 삭제 오류 : {ex.Message}";
            }
        }
    }
}