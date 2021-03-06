﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicingApplication.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Notes { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime DueDate { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual List<OrderLine> OrderLines { get; set; }

        public Order()
        {
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
    }
}