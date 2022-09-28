using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL
{
    public class ProductRepository : IProductRepository
    {
        private MarketContext db;

        public ProductRepository(MarketContext db)
        {
            this.db = db;
        }

        public void AddProduct(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

        public void DeleteProduct(int ProductId)
        {
            var productToDelete = db.Products.Find(ProductId);
            if (productToDelete == null) return;
            
            db.Products.Remove(productToDelete);
            db.SaveChanges();
        }

        public Product GetProductById(int ProductId)
        {
            return db.Products.Find(ProductId);
        }

        public IEnumerable<Product> GetProducts()
        {
            return db.Products.ToList();
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            return db.Products.Where(e => e.CategoryId == categoryId).ToList();
        }

        public void UpdateProduct(Product product)
        {
            var productToUpdate = db.Products.Find(product.ProductId);
            productToUpdate.Name = product.Name;
            productToUpdate.Price = product.Price;
            productToUpdate.Quantity = product.Quantity;
            productToUpdate.CategoryId = product.CategoryId;

            db.SaveChanges();
        }
    }
}
