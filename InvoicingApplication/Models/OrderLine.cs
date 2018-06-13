using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicingApplication.Models
{
    public class OrderLine
    {
        public int OrderLineId { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public double Quantity { get; set; }
        public double Discount { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public OrderLine()
        {
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
    }
}