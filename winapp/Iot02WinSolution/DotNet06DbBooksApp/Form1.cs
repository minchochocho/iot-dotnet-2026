using MaterialSkin.Controls;
using System.Data;

namespace DotNet06DbBooksApp {
    public partial class FrmBook : MaterialForm {
        DatabaseHelper dbHelper;

        public FrmBook()
        {
            InitializeComponent();
        }

        private void FrmBook_Load(object sender, EventArgs e)
        {
            dbHelper = new DatabaseHelper();

            // 자동으로 컬럼생성하면 개발자 의도대로 컬럼을 변경할 수 없음
            DgvBooks.AutoGenerateColumns = false;

            InitGrid(); // 데이터그리드뷰 컬럼 초기설정
            InitData(); // division 테이블 데이터 연동
        }

        private void InitData()
        {
            // 책장르 데이터 조회
            string divSql = "SELECT div_code, div_name FROM bookrentalshop.division";
            DataTable divTable = dbHelper.SelectBook(divSql);

            // 기존 div_code
            var colIndex = DgvBooks.Columns["div_code"].Index;

            // 기존 DataGridViewTextBoxColumn으로 생성된 컬럼 제거
            DgvBooks.Columns.RemoveAt(colIndex);

            // 콤보박스 컬럼 새로 생성
            DataGridViewComboBoxColumn colCboDivCode = new DataGridViewComboBoxColumn
            {
                Name = "div_code",
                HeaderText = "책장르",
                DataPropertyName = "div_code",
                // 연동되는 데이터 설정
                DataSource = divTable,
                ValueMember = "div_code",
                DisplayMember = "div_name"
            };

            // 기존 div_code 컬럼 위치에 추가
            DgvBooks.Columns.Insert(colIndex, colCboDivCode);
            DgvBooks.Columns["div_code"].ReadOnly = false;
        }

        private void InitGrid()
        {
            // 7개 컬럼을 설정
            DgvBooks.Columns.Clear(); // 기존 컬럼 초기화

            // 1. 순번 (book_idx) - PK
            DataGridViewTextBoxColumn colBookIdx = new DataGridViewTextBoxColumn
            {
                Name = "book_idx",
                HeaderText = "순번",
                DataPropertyName = "book_idx",
                ReadOnly = true, // PK는 수정 불가
                Width = 60       // 너비 자동조절 필요 시 설정
            };
            DgvBooks.Columns.Add(colBookIdx);

            // 2. 저자 (author)
            DataGridViewTextBoxColumn colAuthor = new DataGridViewTextBoxColumn
            {
                Name = "author",
                HeaderText = "저자",
                DataPropertyName = "author",
                Width = 100
            };
            DgvBooks.Columns.Add(colAuthor);

            // 3. 분류 코드 (div_code)
            DataGridViewTextBoxColumn colDivCode = new DataGridViewTextBoxColumn
            {
                Name = "div_code",
                HeaderText = "분류 코드",
                DataPropertyName = "div_code",
                Width = 90
            };
            DgvBooks.Columns.Add(colDivCode);

            // 4. 도서명 (book_name)
            DataGridViewTextBoxColumn colBookName = new DataGridViewTextBoxColumn
            {
                Name = "book_name",
                HeaderText = "도서명",
                DataPropertyName = "book_name",
                Width = 200
            };
            DgvBooks.Columns.Add(colBookName);

            // 5. 출판일자 (release_dt)
            DataGridViewTextBoxColumn colReleaseDt = new DataGridViewTextBoxColumn
            {
                Name = "release_dt",
                HeaderText = "출판일자",
                DataPropertyName = "release_dt",
                Width = 110,
                DefaultCellStyle =
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };
            DgvBooks.Columns.Add(colReleaseDt);

            // 6. ISBN (isbn)
            DataGridViewTextBoxColumn colIsbn = new DataGridViewTextBoxColumn
            {
                Name = "isbn",
                HeaderText = "ISBN",
                DataPropertyName = "isbn",
                Width = 130,
                DefaultCellStyle =
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };
            DgvBooks.Columns.Add(colIsbn);

            // 7. 가격 (price)
            DataGridViewTextBoxColumn colPrice = new DataGridViewTextBoxColumn
            {
                Name = "price",
                HeaderText = "가격",
                DataPropertyName = "price",
                Width = 120
            };
            colPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colPrice.DefaultCellStyle.Format = "#,##0원";

            DgvBooks.Columns.Add(colPrice);
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            string query = "SELECT book_idx, author, div_code, book_name, release_dt, isbn, price" +
                           " FROM books";

            // DataGridView 컨트롤 내 DataSource : DataTable 객체를 할당
            DgvBooks.DataSource = dbHelper.SelectBook(query);

        }

        // BtnNew
        private void BtnNew_Click(object sender, EventArgs e)
        {
            try
            {
                var insRes = 0;
                foreach (DataGridViewRow row in DgvBooks.Rows)
                {
                    if (row.IsNewRow) continue;

                    string bookIdx = row.Cells["book_idx"].Value?.ToString();

                    // Console.WriteLine(bookIdx);

                    // book_idx가 비어있으면 새로운 레코드 추가
                    // author, div_code, book_name, release_dt, isbn, price
                    if (string.IsNullOrWhiteSpace(bookIdx))
                    {
                        // 신규추가 할 셀의 값을 읽어옴
                        string author = row.Cells["author"].Value?.ToString();
                        string div_code = row.Cells["div_code"].Value?.ToString();
                        string book_name = row.Cells["book_name"].Value?.ToString();
                        string release_dt = row.Cells["release_dt"].Value?.ToString();

                        // 그리드뷰 셀에서 가져온 날짜 텍스트를 DateTime 객체로 안전하게 파싱합니다.
                        if (DateTime.TryParse(release_dt, out DateTime parsedDate))
                        {
                            // MySQL이 완벽하게 인식할 수 있는 'YYYY-MM-DD' 형식 문자열로 바꿉니다.
                            release_dt = parsedDate.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            // 사용자가 날짜를 이상하게 입력했거나 비워둔 경우 처리 (예: 오늘 날짜로 대체 혹은 스킵)
                            release_dt = DateTime.Now.ToString("yyyy-MM-dd");
                        }
                        string isbn = row.Cells["isbn"].Value?.ToString();
                        string price = row.Cells["price"].Value?.ToString();

                        string insSql = $"INSERT INTO bookrentalshop.books" +
                                        $" (author, div_code, book_name, release_dt, isbn, price)" +
                                        $" VALUES('{author}', '{div_code}', '{book_name}', '{release_dt}', '{isbn}', '{price}');";

                        insRes = dbHelper.Excute(insSql);
                    }
                }
                if (insRes > 0)
                {
                    MessageBox.Show("데이터 추가 완료!");
                    LoadData();

                }
                else
                {
                    MessageBox.Show("데이터 추가 실패!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"INSERT 오류 : {ex.Message}");
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var updRes = 0;

                foreach (DataGridViewRow row in DgvBooks.SelectedRows)
                {
                    string bookIdx = row.Cells["book_idx"].Value?.ToString();
                    if (!string.IsNullOrWhiteSpace(bookIdx))
                    {
                        // MessageBox.Show(bookIdx.ToString());
                        string author = row.Cells["author"].Value?.ToString();
                        string div_code = row.Cells["div_code"].Value?.ToString();
                        string book_name = row.Cells["book_name"].Value?.ToString();
                        string release_dt = row.Cells["release_dt"].Value?.ToString();

                        // 그리드뷰 셀에서 가져온 날짜 텍스트를 DateTime 객체로 안전하게 파싱합니다.
                        if (DateTime.TryParse(release_dt, out DateTime parsedDate))
                        {
                            // MySQL이 완벽하게 인식할 수 있는 'YYYY-MM-DD' 형식 문자열로 바꿉니다.
                            release_dt = parsedDate.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            // 사용자가 날짜를 이상하게 입력했거나 비워둔 경우 처리 (예: 오늘 날짜로 대체 혹은 스킵)
                            release_dt = DateTime.Now.ToString("yyyy-MM-dd");
                        }
                        string isbn = row.Cells["isbn"].Value?.ToString();
                        string price = row.Cells["price"].Value?.ToString();

                        // 가격이 빈값일 때 SQL 에러 방지용 처리
                        if (string.IsNullOrWhiteSpace(price)) price = "0";

                        string upSql = "UPDATE bookrentalshop.books" +
                                       $" SET author='{author}'," +
                                       $" div_code='{div_code}'," +
                                       $" book_name='{book_name}'," +
                                       $" release_dt='{release_dt}'," +
                                       $" isbn='{isbn}'," +
                                       $" price={price}" +
                                       $" WHERE book_idx={bookIdx};";

                        updRes += dbHelper.Excute(upSql);

                    }
                }
                if (updRes > 0)
                {
                    MessageBox.Show($"{updRes}건의 데이터가 수정되었습니다.");
                }
                else
                {
                    MessageBox.Show("데이터 추가 실패!");
                }
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"UPDATE 오류 : {ex.Message}");
            }
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (DgvBooks.SelectedRows.Count == 0)
                {
                    MessageBox.Show("삭제할 행을 선택하세요.");
                    return;
                }

                DialogResult result = MessageBox.Show($"{DgvBooks.SelectedRows.Count}건 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                var delRes = 0;

                if (result == DialogResult.Yes)
                {
                    // 삭제 진행
                    foreach (DataGridViewRow row in DgvBooks.SelectedRows)
                    {
                        string bookIdx = row.Cells["book_idx"].Value?.ToString();

                        if (string.IsNullOrWhiteSpace(bookIdx))
                        {
                            // 책번호 pk가 없으면 패스
                            continue;
                        }

                        string delSql = $"DELETE FROM books WHERE book_idx = {bookIdx}";

                        delRes += dbHelper.Excute(delSql);
                    }
                    MessageBox.Show($"{delRes}건 삭제 완료!");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DELETE 오류{ex}");
            }
        }

    }
}