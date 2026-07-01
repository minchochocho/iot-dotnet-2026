using BooksWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksWeb.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }  // Books 테이블을 사용가능하게 해줌
    }
}
