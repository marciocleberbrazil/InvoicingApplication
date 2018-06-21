using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoicingApplication.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public double Price { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Product()
        {
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }
    }
}