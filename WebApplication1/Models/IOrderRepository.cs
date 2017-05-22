using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public interface IOrderRepository
    {
        //IEnumerable<Order> Orders{ get; }
        Task<IEnumerable<Order>> OrdersAsync();
        
        //IEnumerable<Order> NotYetShippedOrders { get; }

        Task<IEnumerable<Order>> UnshippedOrdersAsync();

        //void SaveOrder(Order order);
        Task SaveOrderAsync(Order order);

        //Order Search(int orderID);

        Task<Order> SearchAsync(int orderID);
    }
}
