using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicingApplication.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}