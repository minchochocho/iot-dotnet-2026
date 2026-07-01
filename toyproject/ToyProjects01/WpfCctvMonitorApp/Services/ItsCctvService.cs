using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using WpfCctvMonitorApp.Model;

namespace WpfCctvMonitorApp.Services {
    public class ItsCctvService {
        private readonly HttpClient httpClient = new HttpClient();
        public async Task<CctvResponse> GetCctvListAsync(string apiUrl)
        {
            string json = await httpClient.GetStringAsync(apiUrl);

            var result = JsonConvert.DeserializeObject<CctvResponse>(json);

            if (result == null)
                return new CctvResponse();
            else
                return result;

        }
        public async Task<ObservableCollection<CctvInfo>> GetAllCctvListAsync()
        {
            return null;
        }
    }
}
