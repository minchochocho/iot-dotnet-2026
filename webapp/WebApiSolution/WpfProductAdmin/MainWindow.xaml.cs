using MahApps.Metro.Controls;
using System.Threading.Tasks;
using System.Windows;
using WpfProductAdmin.Services;

namespace WpfProductAdmin {
    public partial class MainWindow : MetroWindow {
        private readonly ApiService service = new ApiService();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SbiStatus.Text = "준비";
            await SearchProductsAsync();
        }

        private async Task SearchProductsAsync()
        {
            SbiStatus.Text = "조회 중...";

            var result = await service.GetProductsAsync();

            DgrProduct.ItemsSource = result;
            SbiStatus.Text = $"조회 완료 ({result.Count}건)";
        }

        private async void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            var createWindow = new ProductCreateWindow(service) {
                Owner = this
            };

            if (createWindow.ShowDialog() == true)
            {
                await SearchProductsAsync();
            }
        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            await SearchProductsAsync();
        }
    }
}
