using System;
using System.Collections.Generic;

using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VMMVC.Models;


namespace VMMVC.Controllers
{
    public class HomeController : Controller
    {
        public class Money : CMoneyBag
        {
            public static int Debt;
        }

        public static string Message = "Внесите деньги, выберите товар, нажмите КУПИТЬ";
        private ProductContext db = new ProductContext();
        // private Money MB = new Money();

        // GET: Products
        public ActionResult Index()
        {
            
            ViewBag.Message = Message;
            ViewBag.Debt = Money.Debt;
            ViewBag.M10 = CMoneyBag.ten;
            ViewBag.M5 = CMoneyBag.five;
            ViewBag.M2 = CMoneyBag.two;
            ViewBag.M1 = CMoneyBag.one;
            
            return View(db.Products.ToList());
        }

       

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Rest")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
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
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Rest")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
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
            Product product = db.Products.Find(id);
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
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //
        // Покупка товара
        //
        public ActionResult BuyProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                if(product.Price <= Money.Debt)
                {
                    product.Rest -= 1;
                    Message = "Спасибо!";
                    Money.Debt -= product.Price;

                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();

                }
                else
                {
                    Message = "Не достаточно средств!";
                }
            }
               
            return RedirectToAction("Index");
        }
        //
        // Выдача сдачи
        //
        public ActionResult Change()
        {
            int n_ten = Money.Debt / 10;
            int n_five = (Money.Debt - n_ten * 10) / 5;
            int n_two = (Money.Debt - n_ten * 10 - n_five * 5) / 2;
            int n_one = (Money.Debt - n_ten * 10 - n_five * 5 - n_two * 2);
            //
            //
            CMoneyBag.ten = CMoneyBag.ten + n_ten;
            CMoneyBag.five = CMoneyBag.five + n_five;
            CMoneyBag.two = CMoneyBag.two + n_two;
            CMoneyBag.one = CMoneyBag.one + n_one;

            Money.Debt = 0;
            Message = "Внесите оплату, выберите товар, нажмите Купить";

            return RedirectToAction("Index");
        }
        //
        //   Внесение монет
        //
        public ActionResult Pay_10()
        {
            if (CMoneyBag.ten != 0)
            {
                Money.Debt = Money.Debt + 10;
                CMoneyBag.ten = CMoneyBag.ten - 1;
                MMoneyBag.ten += 1;
                Message = "Давай ещё!";
            }
            return RedirectToAction("Index");

        }

        public ActionResult Pay_5()
        {
            if (CMoneyBag.five != 0)
            {
                Money.Debt = Money.Debt + 5;
                CMoneyBag.five = CMoneyBag.five - 1;
                MMoneyBag.five += 1;
                Message = "Давай ещё!";
            }
            return RedirectToAction("Index");

        }
       
        public ActionResult Pay_2()
        {
            if (CMoneyBag.two != 0)
            {
                Money.Debt = Money.Debt + 2;
                CMoneyBag.two = CMoneyBag.two - 1;
                MMoneyBag.two += 1;
                Message = "Давай ещё!";
            }
            return RedirectToAction("Index");
            
        }

        public ActionResult Pay_1()
        {
            if (CMoneyBag.one != 0)
            {
                Money.Debt = Money.Debt + 1;
                CMoneyBag.one = CMoneyBag.one - 1;
                MMoneyBag.one += 1;
                Message = "Давай ещё!";
            }
            return RedirectToAction("Index");

        }
    }
}
