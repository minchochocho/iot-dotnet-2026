using Newtonsoft.Json;
using System;
[Serializable]
public class ProductResult {
    [JsonProperty("deviceId")]   // JSON 키 확인 후 수정 (deviceId / devicedId)
    public string DeviceId;
    [JsonProperty("timestamp")]
    public string Timestamp;
    [JsonProperty("data")]
    public string Data;
}
[Serializable]
public class ProductData {
    [JsonProperty("color")]
    public string Color;        // "red", "blue" 등
    [JsonProperty("result")]
    public string Result;       // "OK", "NG" 등
    [JsonProperty("productId")]
    public int ProductId;
}