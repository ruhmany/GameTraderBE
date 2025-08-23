using GameTrader.Core.Interfaces.IRepositories;
using GameTrader.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Business.Services
{
    public class CategoryService : ICategoryService
    {        
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> CreateNewCategory(string categoryName)
         => await _categoryRepository.CreateNewCategory(categoryName);

        public async Task<List<string>> GetAllCategories()
        => await _categoryRepository.GetAllCategories();
    }
}
