using InvoicingApplication.Services;
using InvoicingApplication.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoicingApplication.Controllers
{
    public class ApiController : BaseController
    {
        public ApiController(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        [HttpPost]
        [Route("api/orders/save")]
        public JsonResult SaveOrder(PrepareOrder prepareOrder)
        {
            try
            {
                return Json(new
                {
                    status = true,
                    prepareOrder
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("api/customers/search/{text?}")]
        public JsonResult GetCustomers(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                    throw new Exception("Not Found");

                var items = _unitOfWork.CustomerRepository.Get(x => x.FirstName.Contains(text) || x.LastName.Contains(text));

                return Json(new
                {
                    status = true,
                    items = items.Select(x => new {
                        id = x.CustomerId,
                        firstName = x.FirstName,
                        lastName = x.LastName
                    })
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("api/products/search/{text?}")]
        public JsonResult GetProducts(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                    throw new Exception("Not Found");

                var items = _unitOfWork.ProductRepository.Get(x => x.Description.Contains(text));

                return Json(new
                {
                    status = true,
                    items = items.Select(x => new {
                        id = x.ProductId,
                        description = x.Description,
                        price = x.Price
                    })
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}