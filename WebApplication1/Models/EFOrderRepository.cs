using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext _context;
        public EFOrderRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public IEnumerable<Order> NotYetShippedOrders =>
            _context.Orders.Where(o => !o.Shipped);

        public async Task<IEnumerable<Order>> UnshippedOrdersAsync() =>
            await _context.Orders
            .Include(o => o.LineItems)
                .ThenInclude(l => l.Song)
            .Where(o => !o.Shipped).ToListAsync();
       
        public IEnumerable<Order> Orders => _context.Orders
            .Include(o => o.LineItems)
                .ThenInclude(l => l.Song);
        
        public async Task<IEnumerable<Order>> OrdersAsync() => 
            await _context.Orders
               .Include(o => o.LineItems)
                   .ThenInclude(l => l.Song).ToListAsync();

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.LineItems.Select(l => l.Song));
            if (order.OrderID == 0)
            {
                _context.Orders.Add(order);
            }
            _context.SaveChanges();
        }

        public async Task SaveOrderAsync(Order order)
        {
            _context.AttachRange(order.LineItems.Select(l => l.Song));
            if (order.OrderID == 0)
            {
                 _context.Orders.Add(order);
            }
           await _context.SaveChangesAsync();
        }

        public Order Search(int orderID) =>
            _context.Orders.FirstOrDefault(o => o.OrderID == orderID);

        public async Task<Order> SearchAsync(int orderID) =>
         await _context.Orders
            .Include(o => o.LineItems)
                .ThenInclude(l => l.Song)
            .FirstOrDefaultAsync(o => o.OrderID == orderID);

        
    }
}
