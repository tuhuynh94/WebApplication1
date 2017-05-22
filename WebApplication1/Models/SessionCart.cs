using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WebApplication1.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class SessionCart : Cart
    {
        private ICollection<LineItem> _lineItems = new List<LineItem>();

        [JsonIgnore]
        public ISession Session { get; set; }
        public ICollection<LineItem> LineItems => _lineItems;

        public void AddLineItem(SongModel song, int quantity)
        {
            LineItem line = _lineItems
                .Where(p => p.Song.id == song.id)
                .FirstOrDefault();
            if (line == null)
            {
                _lineItems.Add(new LineItem
                {
                    Song = song,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
            Session.SetJson("Cart", this);
        }

        public void RemoveLineItem(SongModel song)
        { 
            LineItem line = _lineItems.SingleOrDefault(l => l.Song.id == song.id);
            _lineItems.Remove(line);
            Session.SetJson("Cart", this);
        }

        public decimal ComputeTotalValue() =>
            _lineItems.Sum(e => e.Song.Price * e.Quantity);

        public void Clear()
        {
            _lineItems.Clear();
            Session.Remove("Cart");
        }
        
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
            .HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart")
            ?? new SessionCart();
            cart.Session = session;
            return cart;
        }
    }
}
