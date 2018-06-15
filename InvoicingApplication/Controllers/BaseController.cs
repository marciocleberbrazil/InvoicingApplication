using InvoicingApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoicingApplication.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}