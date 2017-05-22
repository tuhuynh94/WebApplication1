using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1.Models
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext context;

        public EFCategoryRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        IEnumerable<string> ICategoryRepository.Names =>  context.Categories.Select(c => c.Name).OrderBy(n => n);
        public async Task<IEnumerable<Category>> CategoriesAsNoTrackingAsync() => await context.Categories.AsNoTracking().OrderBy(c => c.Name).ToListAsync();
    }
}
