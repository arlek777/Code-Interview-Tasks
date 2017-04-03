using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks.DesignPatterns
{
    internal class ProductsService : IProductsService
    {
        private readonly List<ProductContainer> _products;

        public ProductsService(List<ProductContainer> products)
        {
            _products = products;
        }

        public void ReturnProduct(uint position, double money)
        {
            var product = _products.FirstOrDefault(p => p.Position == position);

            if(product == null || product.Quantity == 0) throw new ApplicationException("Don't have this product.");
            if(product.Price > money) throw new ApplicationException("Not enough money.");

            product.Quantity--;
        }
    }
}