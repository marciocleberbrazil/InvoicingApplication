using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicingApplication.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public string Notes { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime DueDate { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual List<InvoiceLine> InvoiceLines { get; set; }
    }
}