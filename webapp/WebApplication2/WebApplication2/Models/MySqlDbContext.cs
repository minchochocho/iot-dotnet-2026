using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models {
    public class MySqlDbContext : DbContext {
        public MySqlDbContext(DbContextOptions options) : base(options)
        {
            // 자동생성. 내용없음
        }

        public DbSet<Book> Books => Set<Book>();
    }
}
