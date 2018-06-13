using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicingApplication.ViewsModel
{
    public class PrepareOrder
    {
        public int id { get; set; }
        public int customerId { get; set; }
        public List<PrepareOrderItem> lines { get; set; }
    }

    public class PrepareOrderItem
    {
        public int id { get; set; }
        public int quantity { get; set; }
        public double discount { get; set; }
        public int productId { get; set; }
    }
}