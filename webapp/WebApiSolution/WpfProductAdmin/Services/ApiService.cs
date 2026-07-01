using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WpfProductAdmin.Models;

namespace WpfProductAdmin.Services {
    public class ApiService {
        // private const string BaseUrl = "http://localhost:5151/api/products";    // 최초 개발용 API
        private const string BaseUrl = "    ";    // 도커배포 API

        public async Task<ObservableCollection<Product>> GetProductsAsync()
        {
            try
            {
                using HttpClient client = new HttpClient();
                string json = await client.GetStringAsync(BaseUrl);

                var response = JsonSerializer.Deserialize<ObservableCollection<Product>>(
                    json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return response ?? new ObservableCollection<Product>();
            }
            catch
            {
                return new ObservableCollection<Product>();
            }
        }

        public async Task<bool> PostProductAsync(Product product)
        {
            try
            {
                using HttpClient client = new HttpClient();
                var response = await client.PostAsJsonAsync(BaseUrl, product);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
