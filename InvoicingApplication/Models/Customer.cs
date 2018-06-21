using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoicingApplication.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "This field is required")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "This field is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string City { get; set; }

        [DisplayName("Post Code")]
        [Required(ErrorMessage = "This field is required")]
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