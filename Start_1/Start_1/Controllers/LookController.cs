using Start_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Start_1.Controllers
{
    public class LookController : Controller  //Данный класс и его методы отвечают за 
    {                                           //представление таблиц избазы данных +
        StoreContext db = new StoreContext();   //возможности работы с ними
      
        public ActionResult ClientView()
        {
            ViewBag.Clients = db.Clients;
            return View();
        }

        public ActionResult ManagerView()
        {
            ViewBag.Managers = db.Managers;
            return View();
        }

        public ActionResult ProductView()
        {
            ViewBag.Products = db.Products;
            return View();
        }
        public ActionResult OrderView()
        {
            ViewBag.Orders = db.Orders;
            return View();
        }

        public ActionResult SuppleView()
        {
            ViewBag.Supplements = db.Supplements;
            return View();
        }

    }
}