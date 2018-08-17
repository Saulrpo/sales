using System;
using System.Collections.Generic;
using System.Text;

namespace sales.ViewModels
{
    public class MainViewModels
    {
        public ProductsViewModels Products { get; set; }

        public MainViewModels()
        {
            this.Products = new ProductsViewModels();
        }
    }
}
