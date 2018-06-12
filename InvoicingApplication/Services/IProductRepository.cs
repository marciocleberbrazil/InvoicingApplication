using InvoicingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApplication.Services
{
    public interface IProductRepository
    {
        void Delete(int id);
        void Save(Product item);
        Product GetById(int? id);
        List<Product> GetAll();
    }
}
