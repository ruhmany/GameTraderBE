using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.Interfaces.IServices
{
    public interface ICategoryService
    {
        Task<bool> CreateNewCategory(string categoryName);
        Task<List<string>> GetAllCategories();
    }
}
