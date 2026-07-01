using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksWeb.Models {
    [Table("books")]
    public class Book {
        [Key]
        [Column("book_idx")]
        public int Id { get; set; }

        [Column("author")]
        public string Author { get; set; } = string.Empty;

        [Column("div_code")]
        public string DivCode { get; set; } = string.Empty;

        [Column("book_name")]
        public string BookName { get; set; } = string.Empty;

        [Column("release_dt")]
        public DateTime ReleaseDt { get; set; }

        [Column("isbn")]
        public int Isbn { get; set; }

        [Column("price")]
        public string Price { get; set; } = string.Empty;
    }
}
