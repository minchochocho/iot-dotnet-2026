using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Input;
using WpfBusanFestivalApp.Helpers;
using WpfBusanFestivalApp.model;
using WpfBusanFestivalApp.Services;

namespace WpfBusanFestivalApp {
    public partial class MainWindow : MetroWindow {

        // NLog 기본 객체 생성방법
        // private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly FestivalApiService service;

        // 객체 생성은 클래스 생성자에서 일반적으로 구현
        public MainWindow()
        {
            InitializeComponent();

            service = new FestivalApiService();

            // logger에서 쓸수 있는 메서드
            // Info, Trace(잘안씀), Debug, Warn(경고), Error(예외발생)
            Common.logger.Info("부산 페스티벌 정보앱 시작");

        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 앱 시작 시 첫 데이터 로드
            await SearchFestivalAsync();
        }

        // 검색

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            await SearchFestivalAsync();
        }

        // 검색기능 처리
        private async Task SearchFestivalAsync()
        {
            try
            {
                BtnSearch.IsEnabled = false; // 검색이 완료될때까지 비활성화. 다시 못누르게

                // NumPageNo.Value == Null ? 1 : NumPageNo.Value
                int pageNo = Convert.ToInt32(NumPageNo.Value ?? 1);
                int numOfRows = Convert.ToInt32(NumOfRows.Value ?? 10);

                var (festivals, totalCount) = await service.GetFestivalsAsync(pageNo, numOfRows);
                DgrFestival.ItemsSource = festivals;
                RunTotalCount.Text = totalCount.ToString("N0");

                if (totalCount > 0)
                {
                    Common.logger.Info($"Page : {pageNo}, Records : {numOfRows} 로드 완료!");

                    SbiStatus.Text = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {pageNo * numOfRows}건 로드 완료";

                }
                else
                    Common.logger.Warn("조회된 데이터가 없습니다. API 키 또는 요청 값을 확인하세요.");
            }
            catch (Exception ex)
            {
                Common.logger.Error($"데이터 로드 실패 SearchFestivalAsync() : {ex.Message}");
            }
            finally
            {
                BtnSearch.IsEnabled = true;
            }
        }

        // +,- 버튼부분
        #region
        private void BtnPageMinus_Click(object sender, RoutedEventArgs e)
        {
            if ((NumPageNo.Value ?? 1) > 1)
            {
                NumPageNo.Value = (NumPageNo.Value ?? 1) - 1;
                TxtPageNo.Text = NumPageNo.Value.ToString();
            }
        }

        private void BtnPagePlus_Click(object sender, RoutedEventArgs e)
        {
            NumPageNo.Value = (NumPageNo.Value ?? 1) + 1;
            TxtPageNo.Text = NumPageNo.Value.ToString();
        }

        private void BtnRowsMinus_Click(object sender, RoutedEventArgs e)
        {
            if ((NumOfRows.Value ?? 10) > 10)
            {
                NumOfRows.Value = (NumOfRows.Value ?? 10) - 10;
                TxtRows.Text = NumOfRows.Value.ToString();
            }
        }

        private void BtnRowsPlus_Click(object sender, RoutedEventArgs e)
        {
            if ((NumOfRows.Value ?? 10) < 100)
            {
                NumOfRows.Value = (NumOfRows.Value ?? 10) + 10;
                TxtRows.Text = NumOfRows.Value.ToString();
            }
        }
        #endregion

        private void DgrFestival_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DgrFestival.SelectedItem is not FestivalItem item)
                return;

            FestivalDetailWindow win = new FestivalDetailWindow(item);
            win.Owner = this;

            win.ShowDialog();
        }
    }
}
