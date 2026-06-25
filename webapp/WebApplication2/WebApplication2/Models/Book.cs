using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models {
    [Table("books")]
    public class Book {
        [Key]
        [Column("book_idx")]
        public int Id { get; set; }

        [Column("author")]
        public string Author { get; set; }

        [Column("div_code")]
        public string DivCode { get; set; }

        [Column("book_name")]
        public string BookName { get; set; }

        [Column("release_dt")]
        public DateTime ReleaseDt { get; set; }

        [Column("isbn")]
        public string ISBN { get; set; }

        [Column("price")]
        public decimal Price { get; set; }
    }
}
