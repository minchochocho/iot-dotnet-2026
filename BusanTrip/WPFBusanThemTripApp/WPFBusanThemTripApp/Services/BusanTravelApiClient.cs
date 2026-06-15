using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace WPFBusanThemTripApp.Services;

public sealed class BusanTravelApiClient {
    private readonly HttpClient _httpClient;
    private readonly OpenApiOptions _options;

    public BusanTravelApiClient(OpenApiOptions options)
    {
        _options = options;
        _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(15)
        };
    }

    public async Task<ApiPageResult> GetRecommendedKrAsync(int pageNo, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_options.ServiceKey))
        {
            throw new InvalidOperationException("OpenAPI ServiceKey가 비어 있습니다. appsettings.json에 설정해 주세요.");
        }

        string endpoint = $"{_options.BaseUrl.TrimEnd('/')}/{_options.ServicePath.TrimStart('/')}";
        string query = BuildQueryString(new Dictionary<string, string>
        {
            ["serviceKey"] = _options.ServiceKey,
            ["numOfRows"] = _options.NumOfRows.ToString(),
            ["pageNo"] = pageNo.ToString(),
            ["resultType"] = _options.ResultType
        });

        using HttpResponseMessage response = await _httpClient.GetAsync($"{endpoint}?{query}", cancellationToken);
        response.EnsureSuccessStatusCode();

        await using Stream stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using JsonDocument doc = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);

        JsonElement root = doc.RootElement;
        if (!root.TryGetProperty("getRecommendedKr", out JsonElement data))
        {
            throw new InvalidOperationException("응답에 getRecommendedKr 노드가 없습니다.");
        }

        JsonElement header = data.GetProperty("header");
        string code = GetString(header, "code");
        if (string.IsNullOrWhiteSpace(code))
        {
            code = GetString(header, "resultCode");
        }

        string message = GetString(header, "message");
        if (string.IsNullOrWhiteSpace(message))
        {
            message = GetString(header, "resultMsg");
        }

        if (!string.Equals(code, "00", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException($"API 오류: {code} - {message}");
        }

        int totalCount = GetInt(data, "totalCount");
        int numOfRows = GetInt(data, "numOfRows");
        int currentPage = GetInt(data, "pageNo");
        if (numOfRows <= 0)
        {
            numOfRows = _options.NumOfRows;
        }

        if (currentPage <= 0)
        {
            currentPage = pageNo;
        }

        List<TravelApiItem> items = [];
        if (TryGetItemsNode(data, out JsonElement itemNode))
        {
            if (itemNode.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement element in itemNode.EnumerateArray())
                {
                    items.Add(MapItem(element));
                }
            }
            else if (itemNode.ValueKind == JsonValueKind.Object)
            {
                items.Add(MapItem(itemNode));
            }
        }

        return new ApiPageResult(currentPage, numOfRows, totalCount, items);
    }

    private static TravelApiItem MapItem(JsonElement element)
    {
        return new TravelApiItem
        {
            UcSeq = GetInt(element, "UC_SEQ"),
            MainTitle = GetString(element, "MAIN_TITLE"),
            District = GetString(element, "GUGUN_NM"),
            Category = GetString(element, "CATE2_NM"),
            Place = GetString(element, "PLACE"),
            Title = GetString(element, "TITLE"),
            Address1 = GetString(element, "ADDR1"),
            Address2 = GetString(element, "ADDR2"),
            ContactTel = GetString(element, "CNTCT_TEL"),
            HomepageUrl = GetString(element, "HOMEPAGE_URL"),
            MainImageNormal = GetString(element, "MAIN_IMG_NORMAL"),
            MainImageThumb = GetString(element, "MAIN_IMG_THUMB"),
            ItemContents = GetString(element, "ITEMCNTNTS"),
            Lat = GetDouble(element, "LAT"),
            Lng = GetDouble(element, "LNG")
        };
    }

    private static int GetInt(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out JsonElement value))
        {
            return 0;
        }

        return value.ValueKind switch
        {
            JsonValueKind.Number => value.GetInt32(),
            JsonValueKind.String when int.TryParse(value.GetString(), out int parsed) => parsed,
            _ => 0
        };
    }

    private static double? GetDouble(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out JsonElement value))
        {
            return null;
        }

        return value.ValueKind switch
        {
            JsonValueKind.Number => value.GetDouble(),
            JsonValueKind.String when double.TryParse(value.GetString(), out double parsed) => parsed,
            _ => null
        };
    }

    private static string GetString(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out JsonElement value))
        {
            return string.Empty;
        }

        return value.GetString() ?? string.Empty;
    }

    private static string BuildQueryString(Dictionary<string, string> query)
    {
        return string.Join("&", query
            .Where(x => !string.IsNullOrWhiteSpace(x.Value))
            .Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));
    }

    private static bool TryGetItemsNode(JsonElement data, out JsonElement itemNode)
    {
        if (data.TryGetProperty("item", out itemNode))
        {
            return true;
        }

        if (data.TryGetProperty("items", out itemNode))
        {
            return true;
        }

        itemNode = default;
        return false;
    }
}

public sealed class OpenApiOptions {
    public string BaseUrl { get; set; } = "http://apis.data.go.kr/6260000/RecommendedService";
    public string ServicePath { get; set; } = "getRecommendedKr";
    public string ServiceKey { get; set; } = string.Empty;
    public string ResultType { get; set; } = "json";
    public int NumOfRows { get; set; } = 10;
}

public sealed record ApiPageResult(
    int PageNo,
    int NumOfRows,
    int TotalCount,
    List<TravelApiItem> Items);

public sealed class TravelApiItem {
    public int UcSeq { get; set; }
    public string MainTitle { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Place { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Address1 { get; set; } = string.Empty;
    public string Address2 { get; set; } = string.Empty;
    public string ContactTel { get; set; } = string.Empty;
    public string HomepageUrl { get; set; } = string.Empty;
    public string MainImageNormal { get; set; } = string.Empty;
    public string MainImageThumb { get; set; } = string.Empty;
    public string ItemContents { get; set; } = string.Empty;
    public double? Lat { get; set; }
    public double? Lng { get; set; }
}
