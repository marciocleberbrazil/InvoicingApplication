using InvoicingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApplication.Services
{
    public interface ICustomerRepository
    {
        void Delete(int id);
        void Save(Customer item);
        Customer GetById(int? id);
        List<Customer> GetAll();
    }
}
