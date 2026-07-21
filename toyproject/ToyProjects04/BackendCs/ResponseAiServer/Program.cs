namespace ResponseAiServer {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // 1. HttpClient 등록 - PythonAi 외부서버 등록
            builder.Services.AddHttpClient("PythonAiServer", client => {
                client.BaseAddress = new Uri("http://127.0.0.1:8080");
            });

            // 2. CORS 허용(로컬테스트용) -> 실제 운용시는 더 정확하게 설정해야 함
            builder.Services.AddCors(options => {
                options.AddDefaultPolicy(policy => {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            // 3. Controller만 사용하니깐
            builder.Services.AddControllers();
            var app = builder.Build();

            app.UseRouting();

            app.UseCors();

            app.UseDefaultFiles();   // index.html 자동 실행
            app.UseStaticFiles();    // html/css/js 제공

            app.MapStaticAssets();

            app.MapControllers();

            app.Run();
        }
    }
}
