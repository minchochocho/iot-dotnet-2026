using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls.Dialogs;
using MySqlConnector;
using System.Collections.ObjectModel;
using System.Data;
using WpfMvvm02.Helpers;
using WpfMvvm02.Models;

namespace WpfMvvm02.ViewModels {
    public partial class BookViewModel : ObservableObject {

        private readonly IDialogCoordinator _coordinator;

        private readonly DatabaseHelper _helper;

        public ObservableCollection<Division> Divisions { get; set; }
        public ObservableCollection<Book> Books { get; set; }

        [ObservableProperty]
        private Book selectedBook;

        public BookViewModel(IDialogCoordinator coordinator) {
            _coordinator = coordinator;
            _helper = new DatabaseHelper();

            Divisions = new ObservableCollection<Division>();
            Books = new ObservableCollection<Book>();
            LoadComboFromDb();
            LoadDataFromDb();
        }

        private void LoadComboFromDb() {
            try {
                string query = "SELECT div_code, div_name FROM division";
                var result = _helper.Select(query).AsEnumerable().ToList();

                foreach (DataRow row in result) {
                    Division division = new Division {
                        DivCode = row["div_code"].ToString(),
                        DivName = row["div_name"].ToString()
                    };

                    Divisions.Add(division);
                }

            } catch (Exception ex) {
                _coordinator.ShowMessageAsync(this, "조회오류", $"DB조회 오류 발생 : {ex.Message}");
            }
        }

        private void LoadDataFromDb() {
            try {
                Books = new ObservableCollection<Book>();
                string query = @"SELECT b.book_idx, b.author, 
                                        b.div_code, d.div_name,
                                        b.book_name, b.release_dt, 
                                        b.isbn, b.price
                                   FROM books b
                                  INNER JOIN division d 
                                     ON b.div_code = d.div_code
                                  ORDER BY b.book_idx ASC";

                var result = _helper.Select(query).AsEnumerable().ToList();

                foreach (DataRow row in result) {
                    Book book = new Book {
                        BookIdx = Convert.ToInt32(row["book_idx"]),
                        Author = row["author"].ToString(),
                        DivCode = row["div_code"].ToString(),
                        DivName = row["div_name"].ToString(),
                        BookName = row["book_name"].ToString(),
                        ReleaseDt = Convert.ToDateTime(row["release_dt"]),
                        Isbn = row["isbn"].ToString(),
                        Price = Convert.ToDecimal(row["price"])
                    };
                    Books.Add(book);
                }


            } catch (Exception) {

                throw;
            }
        }

        #region `Command 명령 영역`

        [RelayCommand]
        public void Save() {
            try {
                if (SelectedBook.BookIdx == 0) { //신규


                } else {    // 수정 
                    UpdateBook();
                }

                _coordinator.ShowMessageAsync(this, "저장완료", "도서 정보가 저장되었습니다.");
                LoadDataFromDb();


            } catch (Exception ex) {
                _coordinator.ShowMessageAsync(this, "저장오류", $"저장 중 오류 발생 : {ex.Message}");
            }
        }

        private void UpdateBook() {
            string query = @"
                            UPDATE books
                               SET author=@author
                                 , div_code=@div_code
                                 , book_name=@book_name
                                 , release_dt=@release_dt
                                 , isbn=@isbn
                                 , price=@price
                             WHERE book_idx=@book_idx ";

            _helper.Execute(query,
                new MySqlParameter("@author", SelectedBook.Author),
                new MySqlParameter("@div_code", SelectedBook.DivCode),
                new MySqlParameter("@book_name", SelectedBook.BookName),
                new MySqlParameter("@release_dt", SelectedBook.ReleaseDt.ToString("yyyy-MM-dd")),
                new MySqlParameter("@isbn", SelectedBook.Isbn),
                new MySqlParameter("@price", SelectedBook.Price),
                new MySqlParameter("@book_idx", SelectedBook.BookIdx)
                );

        }

        #endregion
    }
}
