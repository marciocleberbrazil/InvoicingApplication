using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicingApplication.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual List<Order> Orders { get; set; }

        public Customer()
        {
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
    }
}