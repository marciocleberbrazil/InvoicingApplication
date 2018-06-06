using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicingApplication.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual List<Invoice> Invoices { get; set; }
    }
}