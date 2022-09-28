using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases
{
    public class ViewCategoriesUseCase : IViewCategoriesUseCase
    {
        private readonly ICategoryRepository categoryReprository;

        public ViewCategoriesUseCase(ICategoryRepository categoryReprository)
        {
            this.categoryReprository = categoryReprository;
        }

        public IEnumerable<Category> Execute()
        {
            return categoryReprository.GetCategories();
        }
    }
}
