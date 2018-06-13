using InvoicingApplication.Models;
using InvoicingApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace InvoicingApplication.Controllers
{
    public class ApiController : BaseController
    {
        public ApiController(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        [HttpGet]
        [Route("api/orders/{id}")]
        public JsonResult GetOrders(int id)
        {
            try
            {
                var items = _unitOfWork.OrderRepository.Get(x => x.OrderId == id);

                return OrderData(items);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("api/orders")]
        public JsonResult GetOrders()
        {
            try
            {
                var items = _unitOfWork.OrderRepository.Get().OrderByDescending(x => x.Created);

                return OrderData(items);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        [Route("api/orders/save")]
        public JsonResult SaveOrder(Order order)
        {
            try
            {
                _unitOfWork.OrderRepository.Insert(order);
                _unitOfWork.Save();

                return Json(new
                {
                    status = true,
                    orderId = order.OrderId
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

        private JsonResult OrderData(IEnumerable<Order> orders)
        {
            return Json(new
            {
                status = true,
                items = orders.Select(x => new {
                    id = x.OrderId,
                    notes = x.Notes,
                    customer = new
                    {
                        id = x.Customer.CustomerId,
                        firstName = x.Customer.FirstName
                    },
                    lines = x.OrderLines.Select(ln => new {
                        id = ln.OrderLineId,
                        product = new
                        {
                            id = ln.Product.ProductId,
                            description = ln.Product.Description
                        }
                    })
                })
            }, JsonRequestBehavior.AllowGet);
        }
    }
}