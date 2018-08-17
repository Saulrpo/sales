namespace sales.ViewModels
{
    using Common.Models;
    using System.Collections.ObjectModel;
    using Services;
    using System;
    using Xamarin.Forms;
    using System.Collections.Generic;

    public class ProductsViewModels : BaseViewModel
    {
        private ApiService ApiService;

        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }
        }

        public ProductsViewModels()
        {
            this.ApiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            var response = await this.ApiService.GetList<Product>("https://salesapiservices.azurewebsites.net", "/api", "/Products");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                return;
            }

            var list = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(list);
        }
    }
}
