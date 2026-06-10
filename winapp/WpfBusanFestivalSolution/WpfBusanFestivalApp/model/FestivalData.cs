using Newtonsoft.Json;


namespace WpfBusanFestivalApp.model {
    internal class FestivalData {
        [JsonProperty("item")]
        public List<FestivalItem> Items { get; set; } = [];

        [JsonProperty("totalCnt")]
        public int TotalCnt { get; set; }
    }
}
