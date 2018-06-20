using InvoicingApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoicingApplication.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}