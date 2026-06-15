using MahApps.Metro.Controls.Dialogs;
using MQTTnet;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;
using WpfSmartHomeSubscribeApp.Helpers;
using FontFamily = System.Windows.Media.FontFamily;

namespace WpfSmartHomeSubscribeApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MetroWindow : MahApps.Metro.Controls.MetroWindow {
        private bool IsConnected { get; set; }  // 접속여부 확인

        private CancellationTokenSource? _cts;  // 스레드 캔슬객체

        #region MQTT/DB 전송용 속성/변수

        // MQTT
        private IMqttClient? MqttClient { get; set; }

        private string MqttHost { get; set; } = "127.0.0.1";    // TxtMqttBrokerIp 텍스트박스의 IP로 변경되어야 함

        private int MqttPort { get; set; } = 1883;

        private string MqttUser { get; set; } = "root";

        private string MqttPassword { get; set; } = "mqtt123456";

        private string MqttTopic { get; set; } = "home/sensor";

        // DB
        private string DbHost { get; set; } = "127.0.0.1";
        private string DbUser { get; set; } = "root";
        private string DbPassword { get; set; } = "my123456";
        private string DbName { get; set; } = "smarthome";




        #endregion

        # region 생성자 영역
        public MetroWindow()
        {
            InitializeComponent();
            IsConnected = false;
        }
        #endregion

        #region 이벤트 핸들러 영역
        private async void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            // 입력검증
            if (string.IsNullOrWhiteSpace(TxtMqttBrokerIp.Text))
            {
                await this.ShowMessageAsync("오류", "MQTT 브로커 주소를 입력하세요.");
                Common.logger.Warn("MQTT 브로커 주소 미입력!");

                return;
            }

            if (string.IsNullOrWhiteSpace(TxtMqttTopiIp.Text))
            {
                await this.ShowMessageAsync("오류", "MQTT 토픽을 입력하세요.");
                Common.logger.Warn("MQTT 토픽 미입력!");

                return;
            }

            if (string.IsNullOrWhiteSpace(TxtDatabaseIp.Text))
            {
                await this.ShowMessageAsync("오류", "Database IP를 입력하세요.");
                Common.logger.Warn("Database IP 미입력!");

                return;
            }

            MqttHost = TxtMqttBrokerIp.Text.Trim(); // Mqtt호스트 값 127.0.0.1 -> UI에서 입력한 HostIP로 변경
            MqttTopic = TxtMqttTopiIp.Text.Trim();
            DbHost = TxtDatabaseIp.Text.Trim();

            if (!IsConnected)
            {
                // Mqtt브로커 접속 시도
                await ConnectMqttAsync();

                IsConnected = true;
                TxtStatus.Text = "DISCONNECT";
                AddLogs("SYSTEM", "MQTT Subscribe 접속 시작");
                Common.logger.Info("MQTT Subscribe 접속 시작");
                SbiStatus.Text = "MQTT연결시작";
            }
            else
            {
                IsConnected = false;
                TxtStatus.Text = "CONNECT";
                StopSensing();

                if (MqttClient != null && MqttClient.IsConnected)
                {
                    await MqttClient.DisconnectAsync();

                    AddLogs("SYSTEM", "MQTT 브로커 접속 종료");
                    Common.logger.Info("MQTT Subscribe 접속 종료");
                    SbiStatus.Text = "MQTT 연결 종료";
                }
            }
        }

        #endregion

        #region 커스텀 메서드 영역
        private void StopSensing()
        {
            _cts.Cancel();
        }
        private void AddLogs(string topic, string payload)
        {
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

        /// <summary>
        /// MQTT 브로커 접속 메서드
        /// </summary>
        private async Task ConnectMqttAsync()
        {
            // MQTTnet으로 초기화할때 동일한 방식
            var factory = new MqttClientFactory();
            MqttClient = factory.CreateMqttClient();    // DesignPattern 중 Factory 메서드 방식으로 객체 생성   

            // Subscribe 핵심 - 데이터가 Publish(배포)되면 곧바로 Subscribe(구독)
            // Subscribe 실행후 payload가 넘어왔을때 이벤트 처리
            MqttClient.ApplicationMessageReceivedAsync += async e =>
            {
                string topic = e.ApplicationMessage.Topic;

                string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

                AddLogs(topic, payload);
            };

            var options = new MqttClientOptionsBuilder()
                .WithClientId($"WPF-Subscribe-{Guid.NewGuid()}")
                .WithTcpServer(MqttHost, MqttPort)
                .WithCredentials(MqttUser, MqttPassword)
                .WithCleanSession()
                .Build();

            await MqttClient.ConnectAsync(options);

            // subscribe 옵션
            var subscribeOptions = factory
                .CreateSubscribeOptionsBuilder()
                .WithTopicFilter(MqttTopic)
                .Build();

            // subscribe 실행
            await MqttClient.SubscribeAsync(subscribeOptions);

            AddLogs("SYSTEM", "Mqtt 구독 시작!");
        }


        #endregion
    }
}