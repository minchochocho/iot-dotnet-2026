using Newtonsoft.Json;

namespace WpfBusanFestivalApp.model {
    internal class FestivalResponse {
        [JsonProperty("getFestivalKr")]
        public FestivalData? FestivalData { get; set; }
    }
}
