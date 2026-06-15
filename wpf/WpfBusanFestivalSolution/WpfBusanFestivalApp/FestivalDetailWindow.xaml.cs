using CefSharp;
using MahApps.Metro.Controls;
using System.Diagnostics;
using System.Windows.Documents;
using WpfBusanFestivalApp.Helpers;
using WpfBusanFestivalApp.model;

namespace WpfBusanFestivalApp {
    /// <summary>
    /// FestivalDetailWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FestivalDetailWindow : MetroWindow {
        public FestivalItem DetailItem { get; }

        public FestivalDetailWindow(FestivalItem detailItem)
        {
            InitializeComponent();

            DetailItem = detailItem;

            // CS 비하인드코드에 존재하는 데이터를 xaml에서 할당하는 속성
            DataContext = DetailItem;

            // 구글맵은 왼쪽 패널이 지도를 가림
            // string url = $"https://map.google.com/?q={detailItem.Lat},{detailItem.Lng}";
            // string url = $"https://openstreetmap.org/?mlat={detailItem.Lat}&mlon={detailItem.Lng}&zoom=14";

            // leaflet.js를 직접써서 HTML 생성. Python folium이 leaflet을 파이썬용으로 변환
            string html = $@"
            <!DOCTYPE html>
            <html>
            <head>
            <meta charset='utf-8' />
            <link rel='stylesheet'
             href='https://unpkg.com/leaflet@1.9.4/dist/leaflet.css'/>
            <style>
            html,body,#map {{
                margin:0;
                width:100%;
                height:100%;
            }}
            </style>
            </head>

            <body>
            <div id='map'></div>

            <script src='https://unpkg.com/leaflet@1.9.4/dist/leaflet.js'></script>

            <script>
            var map = L.map('map',
            {{
                zoomControl:false
            }})
            .setView([{detailItem.Lat},{detailItem.Lng}], 18);

            L.tileLayer(
            'https://tile.openstreetmap.org/{{z}}/{{x}}/{{y}}.png',
            {{
                attribution:''
            }}
            ).addTo(map);

            L.marker([{detailItem.Lat},{detailItem.Lng}]).addTo(map);
            </script>

            </body>
            </html>";


            //MapBrowser.Address = url;
            MapBrowser.LoadHtml(html);

            RtbItemContents.Document.Blocks.Clear();
            // 새 내용으로 할당
            RtbItemContents.Document.Blocks.Add(
                new Paragraph(
                    new Run(Common.ConvertHtmlToText(detailItem.ItemCntnts))    // 불필요한 html 태그들을 제거
                    )
                );
        }

        private void HomePage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // DataContext에 할당한 오브젝트를 다시 FestivalItem으로 형변환
            if (DataContext is FestivalItem item && !string.IsNullOrWhiteSpace(item.HomepageUrl))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = item.HomepageUrl,
                    UseShellExecute = true
                });
            }
        }
    }
}
