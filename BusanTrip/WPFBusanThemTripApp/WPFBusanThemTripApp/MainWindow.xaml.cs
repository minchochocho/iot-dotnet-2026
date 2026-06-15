using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using WPFBusanThemTripApp.Services;

namespace WPFBusanThemTripApp;

public partial class MainWindow : MetroWindow {
    private readonly ObservableCollection<TripCardItem> _visibleItems = [];
    private readonly List<TripCardItem> _allItems = [];
    private readonly OpenApiOptions _apiOptions;
    private readonly BusanTravelApiClient _apiClient;
    private int _pageNo = 1;
    private int _totalCount;
    private const int CardsPerPage = 9;
    private TripCardItem? _selectedItem;

    public MainWindow()
    {
        InitializeComponent();

        _apiOptions = LoadOpenApiOptions();
        _apiClient = new BusanTravelApiClient(_apiOptions);
        TripItemsControl.ItemsSource = _visibleItems;
        Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        BindDistrictFilter([]);
        await LoadAllItemsAsync();
    }

    private OpenApiOptions LoadOpenApiOptions()
    {
        try
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            if (!File.Exists(filePath))
            {
                return new OpenApiOptions();
            }

            string json = File.ReadAllText(filePath);
            using JsonDocument doc = JsonDocument.Parse(json);
            if (!doc.RootElement.TryGetProperty("OpenApi", out JsonElement node))
            {
                return new OpenApiOptions();
            }

            return new OpenApiOptions
            {
                BaseUrl = node.TryGetProperty("BaseUrl", out JsonElement baseUrl) ? baseUrl.GetString() ?? string.Empty : string.Empty,
                ServicePath = node.TryGetProperty("ServicePath", out JsonElement servicePath) ? servicePath.GetString() ?? "getRecommendedKr" : "getRecommendedKr",
                ServiceKey = ResolveServiceKey(node),
                ResultType = node.TryGetProperty("ResultType", out JsonElement resultType) ? resultType.GetString() ?? "json" : "json",
                NumOfRows = node.TryGetProperty("NumOfRows", out JsonElement numOfRows) ? numOfRows.GetInt32() : 10
            };
        }
        catch
        {
            return new OpenApiOptions();
        }
    }

    private static string ResolveServiceKey(JsonElement node)
    {
        string keyFromJson = node.TryGetProperty("ServiceKey", out JsonElement serviceKey)
            ? serviceKey.GetString() ?? string.Empty
            : string.Empty;

        if (!string.IsNullOrWhiteSpace(keyFromJson))
        {
            return keyFromJson;
        }

        return Environment.GetEnvironmentVariable("BUSAN_OPENAPI_SERVICE_KEY") ?? string.Empty;
    }

    private async Task LoadAllItemsAsync()
    {
        SetLoading(true, "OpenAPI 데이터를 불러오는 중...");
        try
        {
            if (string.IsNullOrWhiteSpace(_apiOptions.ServiceKey))
            {
                StatusText.Text = "appsettings.json의 OpenApi.ServiceKey를 설정해 주세요.";
                return;
            }

            StatusText.Text = "전체 데이터를 불러오는 중...";
            _allItems.Clear();
            _pageNo = 1;

            int requestPage = 1;
            int totalCount = 0;

            while (true)
            {
                ApiPageResult result = await _apiClient.GetRecommendedKrAsync(requestPage);
                if (requestPage == 1)
                {
                    totalCount = result.TotalCount;
                }

                List<TripCardItem> mapped = result.Items.Select(MapToCard).ToList();
                if (mapped.Count == 0)
                {
                    break;
                }

                _allItems.AddRange(mapped);
                StatusText.Text = $"전체 데이터를 불러오는 중... {_allItems.Count}/{Math.Max(totalCount, _allItems.Count)}";
                LoadingText.Text = StatusText.Text;

                if (_allItems.Count >= totalCount)
                {
                    break;
                }

                requestPage++;
            }

            _totalCount = _allItems.Count;
            BindDistrictFilter(_allItems);
            ApplyFilters();
            StatusText.Text = "OpenAPI 연동 완료";
        }
        catch (Exception ex)
        {
            StatusText.Text = "데이터 조회 실패";
            await ShowCenteredPopupAsync("OpenAPI 오류", ex.Message);
        }
        finally
        {
            SetLoading(false);
        }
    }

    private static TripCardItem MapToCard(TravelApiItem item)
    {
        string address = $"{item.Address1} {item.Address2}".Trim();
        string bestMainImageUrl = GetBestQualityImageUrl(item.MainImageNormal, item.MainImageThumb);
        string bestThumbnailUrl = GetBestQualityImageUrl(item.MainImageThumb, item.MainImageNormal);

        return new TripCardItem
        {
            Id = item.UcSeq,
            MainTitle = item.MainTitle,
            District = item.District,
            Category = item.Category,
            Title = string.IsNullOrWhiteSpace(item.Title) ? item.MainTitle : item.Title,
            Place = item.Place,
            Address = address,
            Phone = item.ContactTel,
            Homepage = item.HomepageUrl,
            ThumbnailUrl = bestThumbnailUrl,
            MainImageUrl = bestMainImageUrl,
            Description = StripHtml(item.ItemContents),
            Lat = item.Lat,
            Lng = item.Lng
        };
    }

    private static string GetBestQualityImageUrl(string primary, string secondary)
    {
        string selected = !string.IsNullOrWhiteSpace(primary) ? primary : secondary;
        if (string.IsNullOrWhiteSpace(selected))
        {
            return string.Empty;
        }

        // VisitBusan 이미지 규칙: *_thumbL -> *_ttiel(원본 계열)
        if (selected.Contains("_thumb", StringComparison.OrdinalIgnoreCase))
        {
            selected = selected
                .Replace("_thumbL", "_ttiel", StringComparison.OrdinalIgnoreCase)
                .Replace("_thumb", "_ttiel", StringComparison.OrdinalIgnoreCase);
        }

        return selected;
    }

    private static string StripHtml(string html)
    {
        if (string.IsNullOrWhiteSpace(html))
        {
            return string.Empty;
        }

        string noTag = Regex.Replace(html, "<.*?>", " ");
        return Regex.Replace(noTag, @"\s+", " ").Trim();
    }

    private void BindDistrictFilter(List<TripCardItem> source)
    {
        object? selected = DistrictFilterComboBox.SelectedItem;
        DistrictFilterComboBox.Items.Clear();
        DistrictFilterComboBox.Items.Add("전체 구군");
        foreach (string district in source.Select(x => x.District).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().OrderBy(x => x))
        {
            DistrictFilterComboBox.Items.Add(district);
        }

        DistrictFilterComboBox.SelectedItem = selected ?? "전체 구군";
        if (DistrictFilterComboBox.SelectedIndex < 0)
        {
            DistrictFilterComboBox.SelectedIndex = 0;
        }
    }

    private void ApplyFilters()
    {
        string keyword = SearchTextBox.Text.Trim();
        string? district = DistrictFilterComboBox.SelectedIndex <= 0 ? null : DistrictFilterComboBox.SelectedItem?.ToString();
        bool sortByTitle = SortComboBox.SelectedIndex == 1;

        IEnumerable<TripCardItem> query = _allItems;
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(x =>
                x.MainTitle.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                x.District.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                x.Place.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(district))
        {
            query = query.Where(x => x.District.Equals(district, StringComparison.OrdinalIgnoreCase));
        }

        query = sortByTitle ? query.OrderBy(x => x.MainTitle) : query.OrderByDescending(x => x.Id);
        List<TripCardItem> filteredAll = query.ToList();

        int pageSize = CardsPerPage;
        int totalPages = Math.Max(1, (int)Math.Ceiling(filteredAll.Count / (double)pageSize));
        _pageNo = Math.Clamp(_pageNo, 1, totalPages);
        List<TripCardItem> filtered = filteredAll
            .Skip((_pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        _visibleItems.Clear();
        foreach (TripCardItem item in filtered)
        {
            _visibleItems.Add(item);
        }

        int start = (_pageNo - 1) * pageSize + (filtered.Count > 0 ? 1 : 0);
        int end = (_pageNo - 1) * pageSize + filtered.Count;

        ResultCountText.Text = $"결과 {start}-{end} / {filteredAll.Count}";
        PageStatusText.Text = $"Page {_pageNo} / {totalPages}";
    }

    private void ShowDetail(TripCardItem item)
    {
        _selectedItem = item;
        DetailTitleText.Text = item.Title;
        DetailAddressText.Text = $"주소: {item.Address}";

        bool hasPhone = !string.IsNullOrWhiteSpace(item.Phone);
        string phoneText = hasPhone ? item.Phone : "정보 없음";
        DetailPhoneText.Text = $"연락처: {phoneText}";

        bool hasHomepage = !string.IsNullOrWhiteSpace(item.Homepage);
        string homepageText = hasHomepage ? item.Homepage : "정보 없음";
        DetailHomepageText.Text = $"홈페이지: {homepageText}";
        DetailHomepageText.TextDecorations = hasHomepage ? TextDecorations.Underline : null;

        DetailDescriptionText.Text = item.Description;
        DetailFavoriteButton.IsChecked = item.IsFavorite;

        string imageUrl = string.IsNullOrWhiteSpace(item.MainImageUrl) ? item.ThumbnailUrl : item.MainImageUrl;
        if (Uri.TryCreate(imageUrl, UriKind.Absolute, out Uri? imageUri))
        {
            DetailImage.Source = new BitmapImage(imageUri);
        }

        DetailDescriptionScrollViewer.ScrollToTop();
    }

    private void SearchTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) => ApplyFilters();

    private void DistrictFilterComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (IsLoaded)
        {
            ApplyFilters();
        }
    }

    private void SortComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (IsLoaded)
        {
            ApplyFilters();
        }
    }

    private async void RefreshButton_Click(object sender, RoutedEventArgs e) => await LoadAllItemsAsync();

    private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
    {
        if (_pageNo <= 1)
        {
            return;
        }

        _pageNo--;
        ApplyFilters();
    }

    private void NextPageButton_Click(object sender, RoutedEventArgs e)
    {
        int pageSize = CardsPerPage;
        string keyword = SearchTextBox.Text.Trim();
        string? district = DistrictFilterComboBox.SelectedIndex <= 0 ? null : DistrictFilterComboBox.SelectedItem?.ToString();
        IEnumerable<TripCardItem> query = _allItems;

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(x =>
                x.MainTitle.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                x.District.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                x.Place.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(district))
        {
            query = query.Where(x => x.District.Equals(district, StringComparison.OrdinalIgnoreCase));
        }

        int maxPage = Math.Max(1, (int)Math.Ceiling(query.Count() / (double)pageSize));
        if (_pageNo >= maxPage)
        {
            return;
        }

        _pageNo++;
        ApplyFilters();
    }

    private void SetLoading(bool isLoading, string? message = null)
    {
        LoadingOverlay.Visibility = isLoading ? Visibility.Visible : Visibility.Collapsed;
        if (!string.IsNullOrWhiteSpace(message))
        {
            LoadingText.Text = message;
        }
    }

    private void OpenDetailButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is System.Windows.Controls.Button button && button.Tag is TripCardItem item)
        {
            ShowDetail(item);
        }
    }

    private void FavoriteToggleButton_Changed(object sender, RoutedEventArgs e)
    {
        if (sender is not System.Windows.Controls.Primitives.ToggleButton toggle || toggle.DataContext is not TripCardItem item)
        {
            return;
        }

        item.IsFavorite = toggle.IsChecked == true;
        if (_selectedItem?.Id == item.Id)
        {
            DetailFavoriteButton.IsChecked = item.IsFavorite;
        }
    }

    private void DetailFavoriteButton_Changed(object sender, RoutedEventArgs e)
    {
        if (_selectedItem is not null)
        {
            _selectedItem.IsFavorite = DetailFavoriteButton.IsChecked == true;
        }
    }

    private async void ShowMapButton_Click(object sender, RoutedEventArgs e)
    {
        if (_selectedItem is null)
        {
            await ShowCenteredPopupAsync("안내", "먼저 목록에서 여행지를 선택해 주세요.");
            return;
        }

        if (_selectedItem.Lat is null || _selectedItem.Lng is null)
        {
            await ShowCenteredPopupAsync("안내", "선택한 항목에 좌표 정보가 없습니다.");
            return;
        }

        string label = Uri.EscapeDataString(_selectedItem.Title);
        string url = $"https://map.kakao.com/link/map/{label},{_selectedItem.Lat.Value},{_selectedItem.Lng.Value}";

        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            await ShowCenteredPopupAsync("지도 열기 실패", ex.Message);
        }
    }

    private async Task ShowCenteredPopupAsync(string title, string message)
    {
        var settings = new MetroDialogSettings
        {
            AnimateShow = true,
            AnimateHide = true
        };

        await this.ShowMessageAsync(title, message, MessageDialogStyle.Affirmative, settings);
    }
}

public sealed class TripCardItem : INotifyPropertyChanged {
    private bool _isFavorite;

    public int Id { get; init; }
    public string MainTitle { get; init; } = string.Empty;
    public string District { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Place { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string Homepage { get; init; } = string.Empty;
    public string ThumbnailUrl { get; init; } = string.Empty;
    public string MainImageUrl { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public double? Lat { get; init; }
    public double? Lng { get; init; }

    public string DistrictAndCategory => $"{District} / {Category}";

    public bool IsFavorite
    {
        get => _isFavorite;
        set
        {
            if (_isFavorite == value)
            {
                return;
            }

            _isFavorite = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
