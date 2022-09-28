using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory
{
    public class ProductInMemoryRepository : IProductRepository
    {
        private List<Product> products;

        public ProductInMemoryRepository()
        {
            // Init with default values
            products = new List<Product>()
            {
                new Product(){ProductId = 1, CategoryId = 1, Name = "Iced Tea", Price = 1.99, Quantity = 100},
                new Product(){ProductId = 2, CategoryId = 1, Name = "Ginger Ale", Price = 1.99, Quantity = 200},
                new Product(){ProductId = 3, CategoryId = 2, Name = "Whole Wheat Bread", Price = 1.50, Quantity = 300},
                new Product(){ProductId = 4, CategoryId = 2, Name = "White Bread", Price = 1.50, Quantity = 300}
            };
        }

        public void AddProduct(Product product)
        {
            if (products.Any(x => x.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))) return;

            if ((products.Count > 0) && (products != null))
            {
                var maxId = products.Max(x => x.ProductId);
                product.ProductId = maxId + 1;
            }
            else
            {
                product.ProductId = 1;
            }

            products.Add(product);
        }

        public IEnumerable<Product> GetProducts()
        {
            return products;
        }

        public void UpdateProduct(Product product)
        {
            var productToUpdate = GetProductById(product.ProductId);
            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Price = product.Price;
                productToUpdate.Quantity = product.Quantity;
                productToUpdate.CategoryId = product.CategoryId;
            }
        }

        public Product GetProductById(int ProductId)
        {
            return products?.FirstOrDefault(x => x.ProductId == ProductId);
        }

        public void DeleteProduct(int ProductId)
        {
            var productToDelete = GetProductById(ProductId);
            if(productToDelete != null) products.Remove(productToDelete);
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            return products.Where(x => x.CategoryId == categoryId);
        }
    }
}
