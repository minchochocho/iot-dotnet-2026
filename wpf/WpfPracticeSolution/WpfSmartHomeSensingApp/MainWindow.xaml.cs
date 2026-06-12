using Bogus;
using MahApps.Metro.Controls.Dialogs;
using MQTTnet;
using System.Net;
using System.Text.Json;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;
using WpfSmartHomeSensingApp.Helpers;
using WpfSmartHomeSensingApp.Models;

namespace WpfSmartHomeSensingApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MetroWindow : MahApps.Metro.Controls.MetroWindow {
        private bool IsConnected { get; set; }  // 접속여부 확인

        private CancellationTokenSource? _cts;  // 스레드 캔슬객체

        #region DummyData용 속성/변수들
        private string[] Rooms { get; set; } = Array.Empty<string>();
        private string HomeId { get; set; } = string.Empty;
        private Faker SmartHomeFaker { get; set; } = new("ko");
        #endregion

        #region MQTT 전송용 속성/변수

        private IMqttClient? MqttClient { get; set; }

        private string MqttHost { get; set; } = "127.0.0.1";

        private int MqttPort { get; set; } = 1883;

        private string MqttUser { get; set; } = "root";

        private string MqttPassword { get; set; } = "mqtt123456";

        private string MqttTopic { get; set; } = "home/sensor";

        #endregion

        # region 생성자 영역
        public MetroWindow()
        {
            InitializeComponent();
            IsConnected = false;
            InitFakeData();
        }
        #endregion

        #region 이벤트 핸들러 영역
        private async void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtMqttBrokerIp.Text))
            {
                await this.ShowMessageAsync("오류", "MQTT 브로커 주소를 입력하세요.");
                Common.logger.Info("MQTT 브로커 주소 미입력!");

                return;
            }

            if (!IPAddress.TryParse(TxtMqttBrokerIp.Text, out _))
            {
                await this.ShowMessageAsync("오류", "IP 주소 형식이 올바르지 않습니다.");
                Common.logger.Info("올바르지않은 IP 주소형식!");

                return;
            }

            if (!IsConnected)
            {
                IsConnected = true;
                TxtStatus.Text = "DISCONNECT";
                SbiStatus.Text = "MQTT연결시작";
                StartSensing();
            }
            else
            {
                IsConnected = false;
                TxtStatus.Text = "CONNECT";
                SbiStatus.Text = "MQTT연결종료";
                StopSensing();
            }
        }

        #endregion

        #region 커스텀 메서드 영역
        private void InitFakeData()
        {
            Rooms = new[] { "BED", "BATH", "LIVING", "DINING" };
            HomeId = "D10H703";
            SmartHomeFaker = new Faker("ko");

            Common.logger.Info("Bogus Faker 초기화 완료");
        }

        private void StopSensing()
        {
            _cts.Cancel();
        }
        private async void StartSensing()
        {
            _cts = new CancellationTokenSource();
            try
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    List<SensorData> lists = Rooms.Select(room => new SensorData
                    {
                        HomeId = HomeId,
                        RoomName = room,
                        SensingDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        Temp = Math.Round(SmartHomeFaker.Random.Double(20, 30), 1),
                        Humid = Math.Round(SmartHomeFaker.Random.Double(40, 70), 1),
                    }).ToList();

                    string json = JsonSerializer.Serialize(lists, new JsonSerializerOptions { WriteIndented = true });

                    // Console.WriteLine(json);
                    AddLogs("home/sensor", json);

                    await Task.Delay(TimeSpan.FromSeconds(1));
                }

            }
            catch (Exception)
            {
            }

        }

        private void AddLogs(string topic, string payload)
        {
            // 언젠가 응답없음이 뜸
            // RtbLog.AppendText($"{topic} : {payload}\r\n");  // 이방식으로 텍스트 입력 가능

            // RichTextBox 활용
            Dispatcher.Invoke(() =>
            {
                // UI스레드와 충돌없이 텍스트 출력방법
                Paragraph p = new Paragraph();

                p.Margin = new Thickness(0, 0, 0, 10);  // bottom에 10여백

                p.Inlines.Add(
                    new Run($"[{DateTime.Now:HH:mm:ss}]")
                    {
                        FontWeight = FontWeights.Bold,
                        Foreground = new SolidColorBrush(Colors.Blue)
                    });

                p.Inlines.Add(  // 토픽
                    new Run($"TOPIC : {topic}\n")
                    {
                        FontWeight = FontWeights.Bold
                    });

                p.Inlines.Add( // json 페이로드
                    new Run(payload)
                    {
                        FontFamily = new FontFamily("Consolas")
                    });

                RtbLog.Document.Blocks.Add(p);

                if (RtbLog.Document.Blocks.Count > 50)
                {
                    RtbLog.Document.Blocks.Remove(
                        RtbLog.Document.Blocks.FirstBlock);
                }

                RtbLog.ScrollToEnd();   // 리치텍스트박스 가장 아래쪽으로 포커스
            });


        }
        #endregion
    }
}