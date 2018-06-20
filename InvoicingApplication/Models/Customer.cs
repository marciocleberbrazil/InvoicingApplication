using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace InvoicingApplication.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        [DisplayName("Post Code")]
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