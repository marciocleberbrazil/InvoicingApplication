using InvoicingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoicingApplication.Services
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(AppContext db) : base(db)
        {

        }
    }
}