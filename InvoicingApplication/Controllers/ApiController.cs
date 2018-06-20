using InvoicingApplication.Models;
using InvoicingApplication.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
                    orderId = order.OrderId,
                    created = order.Created.ToString("yyyy-MM-dd")
                }, JsonRequestBehavior.AllowGet);
            }
            catch (DbEntityValidationException ex)
            {
                string message = string.Empty;
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        message = string.Format("{0}:{1}", validationErrors.Entry.Entity.ToString(), validationError.ErrorMessage);
                        break;
                    }
                }
                return Json(new { status = false, message }, JsonRequestBehavior.AllowGet);
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
                        x.CustomerId,
                        x.FirstName,
                        x.LastName,
                        x.Address,
                        x.City,
                        x.State,
                        x.PostCode
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
                        x.ProductId,
                        x.Description,
                        x.Price
                    })
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("api/order-lines/delete")]
        public JsonResult DeleteOrderLine(int OrderLineId)
        {
            try
            {
                _unitOfWork.OrderLineRepository.Delete(OrderLineId);
                _unitOfWork.Save();

                return Json(new
                {
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Route("api/orders/delete")]
        public JsonResult DeleteOrder(int OrderId)
        {
            try
            {
                _unitOfWork.OrderRepository.Delete(OrderId);
                _unitOfWork.Save();

                return Json(new
                {
                    status = true
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
                    x.OrderId,
                    x.Notes,
                    Created = x.Created.ToString("yyyy-MM-dd"),
                    DueDate = x.DueDate.ToString("yyyy-MM-dd"),
                    Customer = new
                    {
                        x.Customer.CustomerId,
                        x.Customer.FirstName,
                        x.Customer.LastName,
                        x.Customer.Address,
                        x.Customer.City,
                        x.Customer.State
                    },
                    OrderLines = x.OrderLines.Select(ln => new {
                        ln.OrderLineId,
                        x.OrderId,
                        ln.Discount,
                        ln.Price,
                        ln.Quantity,
                        Created = ln.Created.ToString("yyyy-MM-dd"),
                        Product = new
                        {
                            ln.Product.ProductId,
                            ln.Product.Description
                        }
                    })
                })
            }, JsonRequestBehavior.AllowGet);
        }
    }
}