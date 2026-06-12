using Newtonsoft.Json;
using NLog;
using System.Collections.ObjectModel;
using System.Net.Http;
using WpfBusanFestivalApp.model;

namespace WpfBusanFestivalApp.Services {
    internal class FestivalApiService {

        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        // 서비스키가 공개됨. github 등
        // private readonly string ServiceKey = "....XlJz%2B%2FK8A%3D%3D";
        private string? ServiceKey { get; set; }

        public FestivalApiService()
        {
            // setx 서비스키 등록
            ServiceKey = Environment.GetEnvironmentVariable("BUSAN_FESTIVAL_API_KEY");

        }

        // OpenAPI를 네트워크 요청시 응답이 늦으면 UI스레드 충돌때문에 응답없음 뜰 수 있음
        public async Task<(ObservableCollection<FestivalItem> Items, int TotalCount)> GetFestivalsAsync(int pageNo = 1, int numOfRows = 10)
        {
            if (ServiceKey == null)
            {
                logger.Warn("공공 데이터 포털 API가 없습니다");
                return ([], 0);
            }

            string serviceUrl = $"https://apis.data.go.kr/6260000/FestivalService/getFestivalKr" +
                                $"?serviceKey={ServiceKey}" +
                                $"&pageNo={pageNo}" +
                                $"&numOfRows={numOfRows}" +
                                $"&resultType=json";

            try
            {
                using HttpClient client = new HttpClient();


                // json으로 다 돌려받음
                string json = await client.GetStringAsync(serviceUrl);

                // 데이터 직렬화(Serialization)로 네트워크 다운로드 
                // 역직렬화로 데이터 변환
                FestivalResponse? response = JsonConvert.DeserializeObject<FestivalResponse>(json);
                FestivalData? data = response?.FestivalData;

                // 1번 예전 C# 방식
                //if (response != null &&
                //    response.FestivalData != null &&
                //    response.FestivalData.Items != null)
                //{
                //    return response.FestivalData.Items;
                //}
                //else
                //{
                //    return new ObservableCollection<FestivalItem>();   // 빈리스트

                //}

                // 2번 좀더 최근 C# 방식

                List<FestivalItem>? items = data?.Items;

                if (items == null) return ([], data?.TotalCnt ?? 0);
                return (new ObservableCollection<FestivalItem>(items), data?.TotalCnt ?? items.Count);
            }
            catch (Exception ex)
            {
                logger.Error($"예외발생 : {ex.Message}");

                throw;
            }
        }
    }
}
