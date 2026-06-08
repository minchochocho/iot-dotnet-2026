using HandyControl.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfTestApp {
    // ─────────────────────────────────────────
    // Models
    // ─────────────────────────────────────────
    public class SummaryCardItem {
        public string Value { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string Color { get; set; } = "#2563EB";
        public string BgColor { get; set; } = "#EFF6FF";
        public string Icon { get; set; } = string.Empty;
    }

    public class ProjectListItem {
        public string Name { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    public class ActivityItem {
        public string TaskId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string TimeAgo { get; set; } = string.Empty;
        public string Description => $"'{TaskId}' 작업이 {Status} 상태로 변경됨";
        public string StatusColor => Status switch { "완료" => "#22C55E", "진행중" => "#3B82F6", _ => "#9CA3AF" };
        public string StatusBgColor => Status switch { "완료" => "#DCFCE7", "진행중" => "#DBEAFE", _ => "#F3F4F6" };
        public string StatusTextColor => Status switch { "완료" => "#16A34A", "진행중" => "#1D4ED8", _ => "#6B7280" };
    }

    // ─────────────────────────────────────────
    // Converter
    // ─────────────────────────────────────────
    public class HexToBrushConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string hex || string.IsNullOrWhiteSpace(hex))
                return Brushes.Black;
            try { return new SolidColorBrush((Color)ColorConverter.ConvertFromString(hex)!); }
            catch { return Brushes.Black; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }

    // ─────────────────────────────────────────
    // ViewModel
    // ─────────────────────────────────────────
    public class MainViewModel {
        public ObservableCollection<ProjectListItem> Projects { get; } =
        [
            new() { Name = "웹 애플리케이션 개발", Count = 0 },
            new() { Name = "모바일 앱 개발",       Count = 0 },
            new() { Name = "데이터베이스 설계",     Count = 0 },
            new() { Name = "API 개발",              Count = 0 },
            new() { Name = "UI/UX 디자인",          Count = 0 }
        ];

        public string Today { get; } =
            DateTime.Now.ToString("yyyy년 M월 d일 (ddd)", new System.Globalization.CultureInfo("ko-KR"));

        public ObservableCollection<SummaryCardItem> SummaryCards { get; } =
        [
            new() { Value = "5", Label = "총 프로젝트",      Color = "#2563EB", BgColor = "#EFF6FF", Icon = "\uE8F1" },
            new() { Value = "6", Label = "총 작업",           Color = "#EF4444", BgColor = "#FEF2F2", Icon = "\uE7C3" },
            new() { Value = "1", Label = "완료된 작업",       Color = "#22C55E", BgColor = "#F0FDF4", Icon = "\uE73E" },
            new() { Value = "4", Label = "진행중인 프로젝트", Color = "#A855F7", BgColor = "#FAF5FF", Icon = "\uE716" }
        ];

        public ObservableCollection<ActivityItem> RecentActivities { get; } =
        [
            new() { TaskId = "작업5", Status = "대기중", TimeAgo = "방금 전"    },
            new() { TaskId = "작업4", Status = "진행중", TimeAgo = "20분 전"    },
            new() { TaskId = "작업1", Status = "완료",   TimeAgo = "43분 전"    },
            new() { TaskId = "작업3", Status = "진행중", TimeAgo = "1일 전"     },
            new() { TaskId = "작업2", Status = "진행중", TimeAgo = "일주일 전"  }
        ];

        public SeriesCollection TaskProgressSeries { get; }

        public MainViewModel()
        {
            TaskProgressSeries = new SeriesCollection
            {
                new PieSeries
                {
                    Title  = "완료",
                    Values = new ChartValues<double> { 1 },
                    Fill   = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#22C55E")!),
                    DataLabels = false
                },
                new PieSeries
                {
                    Title  = "진행중",
                    Values = new ChartValues<double> { 2 },
                    Fill   = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3B82F6")!),
                    DataLabels = false
                },
                new PieSeries
                {
                    Title  = "대기중",
                    Values = new ChartValues<double> { 3 },
                    Fill   = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D1D5DB")!),
                    DataLabels = false
                }
            };
        }
    }

    // ─────────────────────────────────────────
    // MainWindow
    // ─────────────────────────────────────────
    public partial class MainWindow : GlowWindow {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void NewProject_Click(object sender, RoutedEventArgs e)
        {
            Growl.Ask(new HandyControl.Data.GrowlInfo
            {
                Message = "새 프로젝트를 만드시겠습니까?",
                ConfirmStr = "Yes",
                CancelStr = "No",
                ActionBeforeClose = confirmed =>
                {
                    if (confirmed) Growl.Success("프로젝트 생성 화면으로 이동합니다. (준비 중)");
                    return true;
                }
            });
        }

        private void DetailReport_Click(object sender, RoutedEventArgs e)
            => Growl.Info("상세 보고서 화면은 준비 중입니다.");

        private void ViewChartDetail_Click(object sender, RoutedEventArgs e)
            => Growl.Info("작업 진행률 상세 화면은 준비 중입니다.");

        private void ViewAllActivities_Click(object sender, RoutedEventArgs e)
            => Growl.Info("전체 활동 목록은 준비 중입니다.");
    }
}
