using BooksWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace BooksWeb {
    public class Program {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();

            // 데이터베이스 매핑
            var conn = builder.Configuration.GetConnectionString("BookRentalShopConnecting");

            // Models.AppDbContext.cs 내용 매핑
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(conn, ServerVersion.AutoDetect(conn));
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
