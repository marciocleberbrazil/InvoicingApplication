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
                if (order.OrderId > 0)
                {
                    if (order.OrderLines != null)
                    {
                        foreach (var item in order.OrderLines)
                        {
                            if (item.OrderLineId > 0)
                                _unitOfWork.OrderLineRepository.Update(item);
                            else
                                _unitOfWork.OrderLineRepository.Insert(item);
                        }
                    }
                    
                    _unitOfWork.OrderRepository.Update(order);
                }
                else
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
        [Route("api/customers")]
        public JsonResult GetCustomers()
        {
            try
            {
                var items = _unitOfWork.CustomerRepository.Get();

                return Json(new
                {
                    status = true,
                    items = items.Select(x => new {
                        id = x.CustomerId,
                        firstName = x.FirstName,
                        lastName = x.LastName,
                        address = x.Address,
                        city = x.City,
                        state = x.State,
                        postCode = x.PostCode
                    })
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Route("api/products")]
        public JsonResult GetProducts()
        {
            try
            {
                var items = _unitOfWork.ProductRepository.Get();

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
                    created = x.Created.ToString("dd/MM/yyyy"),
                    dueDate = x.DueDate.ToString("dd/MM/yyyy"),
                    customer = new
                    {
                        id = x.Customer.CustomerId,
                        firstName = x.Customer.FirstName,
                        lastName = x.Customer.LastName,
                        address = x.Customer.Address,
                        city = x.Customer.City,
                        state = x.Customer.State
                    },
                    lines = x.OrderLines.Select(ln => new {
                        id = ln.OrderLineId,
                        orderId = x.OrderId,
                        discount = ln.Discount,
                        price = ln.Price,
                        quantity= ln.Quantity,
                        created = ln.Created.ToString("dd/MM/yyyy"),
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