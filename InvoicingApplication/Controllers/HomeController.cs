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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region Json Request
        /*[HttpGet]
        public JsonResult GetCustomers(string text)
        {

        }*/
        #endregion
    }
}