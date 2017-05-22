using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class LineItem
    {
        public int LineItemID { get; set; }
        public SongModel Song { get; set; }
        public int Quantity { get; set; }
    }
}
