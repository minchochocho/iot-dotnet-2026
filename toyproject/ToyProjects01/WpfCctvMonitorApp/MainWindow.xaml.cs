using LibVLCSharp.Shared;
using System.Configuration;
using System.Diagnostics;
using System.Text.Json;
using System.Windows;
using System.Windows.Media;
using Wpf.Ui;
using Wpf.Ui.Controls;
using WpfCctvMonitorApp.Common;
using WpfCctvMonitorApp.Model;
using WpfCctvMonitorApp.Services;

namespace WpfCctvMonitorApp {
    public partial class MainWindow : FluentWindow {
        #region 변수영역
        // readonly - 생성자에서만 초기화 가능
        private LibVLC libVLC;
        private LibVLCSharp.Shared.MediaPlayer mediaPlayer;
        private ItsCctvService itsCctvService;

        // 지역을 선택한 위경도의 범위를 저장할 변수
        private GeoBound selectedGeoBound;

        // WPF UI 다이얼로그 동작 서비스 인터페이스
        private IContentDialogService contentDialogService;

        private LoadingWindow? loadingWindow;

        #endregion

        #region 기본 생성자 및 이벤트핸들러 영역
        public MainWindow()
        {
            InitializeComponent();  // 절대 손대지 말 것!
            InitLibVlc();   // VLC라이브러리 초기화
            InitWebApiService();    // 웹서비스 초기화

            // XAML에서 IsChecked=True를 쓰면 InitializeComponent 중 Checked가 발생해
            // 아직 생성되지 않은 GrbCctv에 접근할 수 있으므로, 초기화 완료 후 설정한다.
            TglRoadType.IsChecked = true;
        }

        /// <summary>
        /// 윈도우 로드완료 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Window_LoadedAsync(object sender, RoutedEventArgs e)
        {
            // TODO : 나중에 지울것.. VideoView에 ITS페이지 테스트용 스트리밍 띄우기
            //var media = new Media(libVLC, new Uri("https://cctvsec.ktict.co.kr:8082/mstrm0602hls/1120FCF852F7766470792664D93398BC/live/01CT000000729/002/SELF/playlist.m3u8?nimblesessionid=27489528&wmsAuthSign=c2VydmVyX3RpbWU9Ny8xLzIwMjYgNDo1ODozMiBBTSZoYXNoX3ZhbHVlPWtLb1gwSkdFWkxIWVRDTHRTZjZja0E9PSZ2YWxpZG1pbnV0ZXM9MTIwJmlkPW1sdG0jbnRpY2xpdmUjNzE0Mzg="));
            //mediaPlayer.Play(media);

            await InitWebview2Async();  // 웹뷰2 초기화

            var result = await InitApiKey(); // App.config에서 API키 받아오기
            if (!result) return;

            InitAppName();
            InitStatusBar();   // xaml 화면 텍스트들 초기화
            initComboItems();   // 콤보박스 지역 리스트업
        }

        private void InitAppName()
        {
            // 비하인드코드로 작업하면 유지보수가 쉬움
            // 윈도우타이틀, WPF UI TitleBar 공통작업
            // HACK : 제목을 변경하려면 AppCommon.appName을 변경~
            this.Title = TlbMain.Title = AppCommon.appName;
        }


        /// <summary>
        /// 정상적으로 실행되면 처리하는 이벤트
        /// </summary>
        /// <param name="sender">이벤트 발생객체</param>
        /// <param name="e">컨트롤/객체 소유 속성들</param>
        private void MediaPlayer_Playing(object? sender, EventArgs e)
        {

            Dispatcher.Invoke(() =>
            {
                TxtConnStat.Text = "● 연결 상태 : 정상";
                TxtConnStat.Foreground = new SolidColorBrush(Colors.Green);
            });

        }

        /// <summary>
        /// 실행안되면 처리하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediaPlayer_EncounteredError(object? sender, EventArgs e)
        {
            // UI 스레드와 충돌(응답없음) 방지
            Dispatcher.Invoke(() =>
            {
                TxtConnStat.Text = "● 연결 상태 : 불량";
                TxtConnStat.Foreground = new SolidColorBrush(Colors.Red);

            });
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
        private void TglRoadType_Checked(object sender, RoutedEventArgs e)
        {
            AppCommon.RoadType = "ex";   // 고속도로 its 국도 "Type은 클래스 keyword"
            GrbCctv.Header = $"고속도로 CCTV 목록 (총 0건)";
            TglRoadType.Content = "🛣  고속도로";
        }

        private void TglRoadType_Unchecked(object sender, RoutedEventArgs e)
        {
            AppCommon.RoadType = "its";
            GrbCctv.Header = $"국도 CCTV 목록 (총 0건)";
            TglRoadType.Content = "🛣  국도";
        }
        /// <summary>
        /// 초기화버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInit_Click(object sender, RoutedEventArgs e)
        {
            TglRoadType.IsChecked = true;
            CboRegions.SelectedIndex = 0;
            var roadName = AppCommon.RoadType == "ex" ? "고속도로" : "국도";
            GrbCctv.Header = $"{roadName} CCTV 목록 (총 0건)";
            LsbCctv.ItemsSource = null; // ItemSources로 바인딩 했을 때는

            mediaPlayer.Stop();
            WvwMap.Reload();    // WebBrowser F5(새로고침)

            SetDetailInfo();
        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (selectedGeoBound == null)
            {
                // System.Windows.MessageBox.Show("지역 선택을 먼저 하십시오", "오류");
                await ShowMessageAsync("오류", "지역을 선택하세요");
                return;
            }
            // 아니면 try ~ catch

            try
            {
                // ProgressRing 보이기
                // 로딩 창 열기
                ShowLoading();
                BtnSearch.IsEnabled = false;

                // 프로세스 진행
                AppCommon.MinX = selectedGeoBound.MinLng;
                AppCommon.MaxX = selectedGeoBound.MaxLng;
                AppCommon.MinY = selectedGeoBound.MinLat;
                AppCommon.MaxY = selectedGeoBound.MaxLat;

                var totalApiUrl = AppCommon.BuildCctvAiUrl();

                var result = await itsCctvService.GetCctvListAsync(totalApiUrl);
                Debug.WriteLine(result);
                // MessageBox.Show(result.Response.DataCount.ToString());

                LsbCctv.ItemsSource = result.Response.Data;
                var roadName = AppCommon.RoadType == "ex" ? "고속도로" : "국도";
                GrbCctv.Header = $"{roadName} CCTV 목록 (총 {result.Response.DataCount:N0}건";

            }
            catch (Exception ex)
            {
                // 대부분 OpenAPI 호출 이후 네트워크 문제에서 발생
                await ShowMessageAsync("검색 오류", $"CCTV 검색 중 오류가 발생했습니다. {ex.Message}");
                System.Windows.MessageBox.Show($"CCTV 검색 중 오류가 발생했습니다. {ex.Message}", "검색 오류",
                                System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                // 로딩창 닫기
                HideLoading();
                BtnSearch.IsEnabled = true;
            }

        }

        // 리스트박스 선택 후 이벤트
        private async void LsbCctv_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (LsbCctv.SelectedItem is not CctvInfo selected)
                return;

            //MessageBox.Show(selected.CctvUrl);
            PlayCctv(selected.CctvUrl);

            await ShowMarkerAsync(selected);

            SetDetailInfo(selected);
            DisplayStatusBarInfo(selected);
        }



        // 종료여부 확인
        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // var result = System.Windows.MessageBox.Show("종료하시겠습니까?", "종료확인", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Question);

            //if (result == System.Windows.MessageBoxResult.No)
            //{
            //    e.Cancel = true;
            //    return;
            //}

            var result = await ShowConfirmAsync("종료확인", "종료하시겠습니까?");

            if (!result)
            {
                e.Cancel = true;
                return;
            }
        }
        // 프로그램 종료 시 메모리 해제
        private void Window_Closed(object sender, EventArgs e)
        {
            mediaPlayer.Stop();
            mediaPlayer.Dispose();
            libVLC.Dispose();
        }

        #endregion

        #region 커스텀 메서드 영역

        /// <summary>
        /// Its OpenApiService 초기화
        /// </summary>
        private void InitWebApiService()
        {
            // OpenAPI 서비스 객체 생성
            itsCctvService = new ItsCctvService();
        }

        /// <summary>
        /// LibVLCsharp.WPF 컨트롤 초기화 메서드
        /// </summary>
        private void InitLibVlc()
        {
            // LibVLCsharp 초기화
            libVLC = new LibVLC();
            mediaPlayer = new LibVLCSharp.Shared.MediaPlayer(libVLC);
            mediaPlayer.Playing += MediaPlayer_Playing;
            mediaPlayer.EncounteredError += MediaPlayer_EncounteredError;

            VvwScreen.MediaPlayer = mediaPlayer;
        }

        private async Task InitWebview2Async()
        {
            await WvwMap.EnsureCoreWebView2Async();
            WvwMap.NavigateToString(GetLeafletHtml());
        }

        private static string GetLeafletHtml()
        {
            return """
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset="utf-8" />
                    <style>
                        html, body, #map {
                            width: 100%;
                            height: 100%;
                            margin: 0;
                            padding: 0;
                        }
                    </style>
                    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.9.4/dist/leaflet.css" />
                    <script src="https://unpkg.com/leaflet@1.9.4/dist/leaflet.js"></script>
                </head>
                <body>
                    <div id="map"></div>
                    <script>
                        let map = L.map('map').setView([36.5, 127.8], 7);

                        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                            maxZoom: 19,
                            attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a>'
                        }).addTo(map);

                        let marker = null;

                        function moveMarker(lat, lng, name) {
                            if (marker != null) {
                                map.removeLayer(marker);
                            }

                            marker = L.marker([lat, lng])
                                .addTo(map)
                                .bindPopup(name)
                                .openPopup();

                            map.setView([lat, lng], 14);
                        }
                    </script>
                </body>
                </html>
                """;
        }

        private async Task<bool> InitApiKey()
        {
            AppCommon.ItsApiKey = ConfigurationManager.AppSettings["ItsApiKey"];

            if (string.IsNullOrWhiteSpace(AppCommon.ItsApiKey))
            {
                //System.Windows.MessageBox.Show("ITS API Key가 설정되지 않았습니다.");
                await ShowMessageAsync("오류", "ITS API Key가 설정되지 않았습니다.");
                return false;
            }
            //MessageBox.Show(Common.AppCommon.ItsApiKey);
            return true;
        }

        private void InitStatusBar()
        {

            TxtConnStat.Text = "● 연결 상태 : -";
            TxtConnStat.Foreground = new SolidColorBrush(Colors.Black);

            TxtConnStat.Text = "선택 CCTV : ";

            TxtConnStat.Text = "영상 URL : ";

            var currTime = DateTime.Now.ToString("HH:mm:ss");
            TxtConnStat.Text = $"마지막 업데이트 : {currTime} ⟳";
        }

        private void initComboItems()
        {
            // CboRegions.Items.Add("전국");
            CboRegions.ItemsSource = AppCommon.Regions;
            CboRegions.SelectedIndex = 0;
        }


        // 위경도 범위 리턴 메서드
        private GeoBound GetRegionBounds(string regionName)
        {
            // Validation Check
            if (string.IsNullOrWhiteSpace(regionName))
                return AppCommon.RegionBounds["전국"];

            return AppCommon.RegionBounds.TryGetValue(regionName, out GeoBound bound)
                ? bound
                : AppCommon.RegionBounds["전국"];
        }


        private void DisplayStatusBarInfo(CctvInfo cctv)
        {
            TxtSelCctvName.Text = $"선택 CCTV : {cctv.CctvName,80}";
            TxtSelCctvUrl.Text = $"영상 URL : {AppCommon.Ellipsis(cctv.CctvUrl)}";
        }

        private void SetDetailInfo(CctvInfo? cctv = null)
        {
            if (cctv != null)   // 실제 데이터 할당
            {
                // 정규식으로 CctvName에 들어온 문자열 분해
                var (cctvName, roadName, direction) = AppCommon.ParseName(cctv.CctvName);

                TxtCctvName.Text = cctvName;
                TxtRoadName.Text = roadName;
                TxtDerection.Text = direction;
                TxtCoordY.Text = cctv.CoordY;
                TxtCoordX.Text = cctv.CoordX;
                TxtCctvFormat.Text = cctv.CctvFormat;
            }
            else    // 초기화
            {
                TxtCctvName.Text = string.Empty;
                TxtRoadName.Text = "";
                TxtDerection.Text = "";
                TxtCoordY.Text = string.Empty;
                TxtCoordX.Text = string.Empty;
                TxtCctvFormat.Text = string.Empty;
            }
        }

        private async Task ShowMarkerAsync(CctvInfo cctv)
        {
            double lat = Convert.ToDouble(cctv.CoordY);
            double lng = Convert.ToDouble(cctv.CoordX);

            string name = cctv.CctvName ?? "CCTV";
            string script = $"moveMarker({lat},{lng},{JsonSerializer.Serialize(name)});";
            await WvwMap.ExecuteScriptAsync(script);  // HTML 스크립트문을 외부에서 실행
        }

        // 스트리밍 재생 메서드

        private async Task PlayCctv(string url)
        {
            if (string.IsNullOrEmpty(url))
                return;

            try
            {
                mediaPlayer.Stop(); // 이전 영상 멈추기

                using var media = new Media(libVLC, new Uri(url));
                var isPlayed = mediaPlayer.Play(media);


            }
            catch (Exception ex)
            {
                await ShowMessageAsync("오류", $"스트리밍 재생 오류 발생 : {ex.Message}");
                //System.Windows.MessageBox.Show(ex.Message);
            }

        }

        // 단순 MessageBox.Show 대신 사용할 기능

        private async Task ShowMessageAsync(string title, string message)
        {

            var uiMessagebox = new Wpf.Ui.Controls.MessageBox()
            {
                Title = title,
                Content = message
            };

            _ = await uiMessagebox.ShowDialogAsync();
        }

        // 예/아니오 선택 MesageBox.Show 대신 사용할 메서드
        private async Task<bool> ShowConfirmAsync(string title, string message)
        {
            var uiMessagebox = new Wpf.Ui.Controls.MessageBox()
            {
                Title = title,
                Content = message,

                PrimaryButtonText = "예",
                SecondaryButtonText = "아니오",
                IsCloseButtonEnabled = false
            };

            var result = await uiMessagebox.ShowDialogAsync();

            return result == Wpf.Ui.Controls.MessageBoxResult.Primary;
        }

        // 로딩창 열기
        private void ShowLoading()
        {
            if (loadingWindow != null) return;

            loadingWindow = new LoadingWindow
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };

            loadingWindow.Show();
        }

        // 로딩창 닫기
        private void HideLoading()
        {
            if (loadingWindow == null) return;

            loadingWindow.Close();
            loadingWindow = null;
        }

        #endregion

    }
}