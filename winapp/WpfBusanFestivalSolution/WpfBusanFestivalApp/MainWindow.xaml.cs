using MahApps.Metro.Controls;
using System.Windows;
using WpfBusanFestivalApp.Services;

namespace WpfBusanFestivalApp {
    public partial class MainWindow : MetroWindow {

        private int _pageNo = 1;
        private int _rows = 10;

        public int PageNo => _pageNo;
        public int Rows => _rows;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            FestivalApiService service = new FestivalApiService();
            var festivals = await service.GetFestivalsAsync(_pageNo, _rows);
            if (festivals != null)
                DgFestival.ItemsSource = festivals;
        }

        private void BtnPageMinus_Click(object sender, RoutedEventArgs e)
        {
            if (_pageNo > 1)
            {
                _pageNo--;
                TxtPageNo.Text = _pageNo.ToString();
            }
        }

        private void BtnPagePlus_Click(object sender, RoutedEventArgs e)
        {
            _pageNo++;
            TxtPageNo.Text = _pageNo.ToString();
        }

        private void BtnRowsMinus_Click(object sender, RoutedEventArgs e)
        {
            if (_rows > 10)
            {
                _rows -= 10;
                TxtRows.Text = _rows.ToString();
            }
        }

        private void BtnRowsPlus_Click(object sender, RoutedEventArgs e)
        {
            if (_rows < 100)
            {
                _rows += 10;
                TxtRows.Text = _rows.ToString();
            }
        }

    }
}
