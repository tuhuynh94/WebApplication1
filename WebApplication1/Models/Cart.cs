using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public interface Cart
    {
        void AddLineItem(SongModel product, int quantity);


        void RemoveLineItem(SongModel product);

        decimal ComputeTotalValue();

        void Clear();

        ICollection<LineItem> LineItems { get; }

    }

}

