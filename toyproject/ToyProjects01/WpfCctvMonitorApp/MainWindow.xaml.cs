using LibVLCSharp.Shared;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using WpfCctvMonitorApp.Common;
using WpfCctvMonitorApp.Services;

namespace WpfCctvMonitorApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly LibVLC libVLC;
        private readonly MediaPlayer mediaPlayer;

        private readonly ItsCctvService itsCctvService;

        // 지역을 선택한 위경도의 범위를 저장할 변수
        private GeoBound selectedGeoBound;

        public MainWindow()
        {
            InitializeComponent();

            // LibVLCsharp 초기화
            libVLC = new LibVLC();
            mediaPlayer = new MediaPlayer(libVLC);

            VvwScreen.MediaPlayer = mediaPlayer;

            // OpenAPI 서비스 객체 생성
            itsCctvService = new ItsCctvService();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO : 나중에 지울것.. VideoView에 ITS페이지 스트리밍 띄우기
            var media = new Media(libVLC, new Uri("https://cctvsec.ktict.co.kr:8082/mstrm0602hls/1120FCF852F7766470792664D93398BC/live/01CT000000729/002/SELF/playlist.m3u8?nimblesessionid=27489528&wmsAuthSign=c2VydmVyX3RpbWU9Ny8xLzIwMjYgNDo1ODozMiBBTSZoYXNoX3ZhbHVlPWtLb1gwSkdFWkxIWVRDTHRTZjZja0E9PSZ2YWxpZG1pbnV0ZXM9MTIwJmlkPW1sdG0jbnRpY2xpdmUjNzE0Mzg="));
            mediaPlayer.Play(media);

            Common.AppCommon.ItsApiKey = ConfigurationManager.AppSettings["ItsApiKey"];
            // MessageBox.Show(Common.AppCommon.ItsApiKey);

            initComboItems();
        }

        private void initComboItems()
        {
            // CboRegions.Items.Add("전국");
            CboRegions.ItemsSource = Common.AppCommon.Regions;
            CboRegions.SelectedIndex = 0;
        }

        private void BtnExpress_Click(object sender, RoutedEventArgs e)
        {
            Common.AppCommon.RoadType = "ex";   // 고속도로 its 국도 "Type은 클래스 keyword"
        }

        private void BtnNational_Click(object sender, RoutedEventArgs e)
        {
            Common.AppCommon.RoadType = "its";
        }

        private void CboRegions_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //Debug.WriteLine(CboRegions.SelectedItem);
            if (CboRegions.SelectedIndex > 0)
            {
                selectedGeoBound = GetRegionBounds(CboRegions.SelectedValue.ToString());

                Debug.WriteLine(selectedGeoBound.MinLng);
                Debug.WriteLine(selectedGeoBound.MaxLng);
                Debug.WriteLine(selectedGeoBound.MinLat);
                Debug.WriteLine(selectedGeoBound.MaxLat);
            }
        }

        // 위경도 범위 리턴 메서드
        private GeoBound GetRegionBounds(string regionName)
        {
            if (string.IsNullOrWhiteSpace(regionName))
                return AppCommon.RegionBounds["전국"];

            return AppCommon.RegionBounds.TryGetValue(regionName, out GeoBound bound)
                ? bound
                : AppCommon.RegionBounds["전국"];
        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            Common.AppCommon.MinX = selectedGeoBound.MinLng;
            Common.AppCommon.MaxX = selectedGeoBound.MaxLng;
            Common.AppCommon.MinY = selectedGeoBound.MinLat;
            Common.AppCommon.MaxY = selectedGeoBound.MaxLat;

            var totalApiUrl = Common.AppCommon.BuildCctvAiUrl();

            var result = await itsCctvService.GetCctvListAsync(totalApiUrl);
            Debug.WriteLine(result);
        }
    }
}