using Newtonsoft.Json;


namespace WpfBusanFestivalApp.model {
    internal class FestivalData {
        [JsonProperty("item")]
        [JsonConverter(typeof(SingleOrArrayConverter<FestivalItem>))]
        public List<FestivalItem> Items { get; set; } = [];

        [JsonProperty("totalCnt")]
        public int TotalCnt { get; set; }
    }
}
