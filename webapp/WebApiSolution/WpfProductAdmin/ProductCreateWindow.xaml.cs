using MahApps.Metro.Controls;
using System;
using System.Windows;
using WpfProductAdmin.Models;
using WpfProductAdmin.Services;

namespace WpfProductAdmin {
    public partial class ProductCreateWindow : MetroWindow {
        private readonly ApiService service;

        public ProductCreateWindow(ApiService service)
        {
            this.service = service;
            InitializeComponent();
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtProductName.Text))
            {
                MessageBox.Show(this, "상품명을 입력하세요.", "입력 확인", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Product product = new Product {
                ProductName = TxtProductName.Text.Trim(),
                Category = string.IsNullOrWhiteSpace(TxtCategory.Text) ? null : TxtCategory.Text.Trim(),
                Price = Convert.ToDecimal(NudPrice.Value),
                Stock = Convert.ToInt32(NudStock.Value),
            };

            bool success = await service.PostProductAsync(product);

            if (!success)
            {
                MessageBox.Show(this, "상품 등록에 실패했습니다. API 서버 상태를 확인하세요.", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
