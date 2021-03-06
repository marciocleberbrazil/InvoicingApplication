﻿using InvoicingApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InvoicingApplication.Services
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppContext db) : base(db)
        {

        }

        public override void Update(Customer ojb)
        {
            _db.Customers.Attach(ojb);
            _db.Entry(ojb).State = EntityState.Modified;
            _db.Entry(ojb).Property(x => x.Created).IsModified = false;
        }
    }
}