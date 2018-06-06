using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicingApplication.Models
{
    public class InvoiceLine
    {
        public int InvoiceLineId { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }

        public double Quantity { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}