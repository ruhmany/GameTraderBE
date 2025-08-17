using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Core.Interfaces.IRepositories
{
    public interface ICategoryRepository
    {
        Task<bool> CreateNewCategory(string categoryName);
        Task<List<string>> GetAllCategories();
    }
}
