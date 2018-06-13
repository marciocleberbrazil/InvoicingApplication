using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InvoicingApplication.Models;
using InvoicingApplication.Services;

namespace InvoicingApplication.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        // GET: Products
        public ActionResult Index()
        {
            return View(_unitOfWork.ProductRepository.Get().ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Product product = _unitOfWork.ProductRepository.GetByID(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Description,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Created = DateTime.Now;
                product.Updated = DateTime.Now;
                _unitOfWork.ProductRepository.Insert(product);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _unitOfWork.ProductRepository.GetByID(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Description,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Updated = DateTime.Now;
                _unitOfWork.ProductRepository.Update(product);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _unitOfWork.ProductRepository.GetByID(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _unitOfWork.ProductRepository.Delete(id);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
