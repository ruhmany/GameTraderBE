using GameTrader.Core.Interfaces.IRepositories;
using GameTrader.Data.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTrader.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateNewCategory(string categoryName)
        {
            await _context.Categories.AddAsync(new CategoryLockup { CategoryName = categoryName });
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<string>> GetAllCategories()
        => await _context.Categories
            .Select(c => c.CategoryName)
            .ToListAsync();
    }
}
