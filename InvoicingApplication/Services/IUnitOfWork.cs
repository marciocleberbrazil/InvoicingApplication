using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApplication.Services
{
    public interface IUnitOfWork
    {
        ProductRepository ProductRepository { get; }
        CustomerRepository CustomerRepository { get; }
        OrderRepository OrderRepository { get; }
        OrderLineRepository OrderLineRepository { get; }

        void Save();
    }
}
