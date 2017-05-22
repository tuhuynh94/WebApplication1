using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;

namespace WebApplication1.Models
{
    public class EFSongRepository : ISongRepository
    {
        private ApplicationDbContext context;

        public EFSongRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public async Task<SongModel> SearchAsync(int id) =>
            await context.Songs.FirstOrDefaultAsync(p => p.id == id);
        public IEnumerable<SongModel> SearchText(string searchText)
        {
            return context.Songs.Where(s => s.Name.Contains(searchText)
                                       || s.Category.Name.Contains(searchText)
                                       || s.Artist.Contains(searchText));
        }

        public async Task<int> SongCountAsync() => await context.Songs.CountAsync();

        public async Task<int> SongCountAsync(string category) => await context.Songs.CountAsync(s => s.Category.Name == category);

        public async Task<IEnumerable<SongModel>> SongsAsync() => await context.Songs.ToListAsync();

        public async Task<IEnumerable<Order>> OrdersAsync() => await context.Orders.ToListAsync();
        public async Task<IEnumerable<SongModel>> SongsAsync(string category, int pageSize, int page)
        {
            return await context.Songs
                    .Where(p => category == null || p.Category.Name == category)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }
    }
}
