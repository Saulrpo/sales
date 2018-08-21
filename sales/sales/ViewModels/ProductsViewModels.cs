namespace sales.ViewModels
{
    using Common.Models;
    using System.Collections.ObjectModel;
    using Services;
    using Xamarin.Forms;
    using System.Collections.Generic;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

    public class ProductsViewModels : BaseViewModel
    {
        private ApiService ApiService;

        private bool isRefreshing;

        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public ProductsViewModels()
        {
            this.ApiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            this.isRefreshing = true;

            var connection = await this.ApiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.isRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");
                return;
            }
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.ApiService.GetList<Product>(url, "/api", "/Products");
            if (!response.IsSuccess)
            {
                this.isRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            var list = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(list);
            this.isRefreshing = false;
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadProducts);
            }
        }

    }
}
