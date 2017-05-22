using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public interface ISongRepository
    {
        Task<int> SongCountAsync();
        Task<int> SongCountAsync(string category);
        Task<IEnumerable<SongModel>> SongsAsync();
        Task<IEnumerable<SongModel>> SongsAsync(string category, int pageSize, int page);

        Task<SongModel> SearchAsync(int productID);
        IEnumerable<SongModel> SearchText(string searchText);
        Task<IEnumerable<Order>> OrdersAsync();
    }
}
