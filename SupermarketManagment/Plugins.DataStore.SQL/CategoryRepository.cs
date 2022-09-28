using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL
{
    public class CategoryRepository : ICategoryRepository
    {
        private MarketContext db;

        public CategoryRepository(MarketContext db)
        {
            this.db = db;
        }

        public void AddCategory(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var categoryToRemove = db.Categories.Find(categoryId);
            if (categoryToRemove == null) return;
            db.Categories.Remove(categoryToRemove);
            db.SaveChanges();
        }

        public IEnumerable<Category> GetCategories()
        {
            return db.Categories.ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            return db.Categories.Find(categoryId);
        }

        public void UpdateCategory(Category category)
        {
            var categoryToUpdate = db.Categories.Find(category.CategoryId);
            categoryToUpdate.Name = category.Name;
            categoryToUpdate.Description = category.Description;

            db.SaveChanges();
        }
    }
}
