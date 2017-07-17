using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Start_1.Models;
using System.Data.Entity;

namespace Start_1.Controllers
{
    public class HomeController : Controller
    {
        StoreContext db = new StoreContext();
        public ActionResult Index()
        {
            return View();
        }
     

        [HttpGet]
        public ActionResult GetOrder(int id)  // GetOrder отвечате за формирование заказа
        {
            Product p = db.Products.Find(id);
            ViewBag.Product = p;
            return View();
        }
        [HttpPost]
        public ActionResult GetOrder(Order order, int Days,int id)
        {
            order.Date_Begin = DateTime.Now;
            order.Date_End = DateTime.Now.AddDays(Days);
            order.Price = db.Products.Find(id).Cost * Days;
            order.Complete = 0;
            db.Orders.Add(order);
            db.SaveChanges();
            db.Products.Find(id).Have = 0;
            db.SaveChanges();
            ViewBag.Order = order;
            return View("Order_Done");
        } 

        [HttpGet]
        public ActionResult Client_New()
        {
            return View();
        }  //Данный класс отвечает за создание нового клиента напрямую, безрегистрации

        [HttpPost]
        public RedirectResult Client_New(Client client)
        {
           
            client.Level = 0;
            client.VIP = 0;
            client.RoleId = 1;
            client.Password = "123";
            db.Clients.Add(client);
            db.SaveChanges();
            return Redirect("/Look/ClientView"); 
        }

       // [HttpGet]
       // public ActionResult Product_Buy()
     //   {
      //      return View();
     //   } 
    //
    //    [HttpPost]
    ///    public ActionResult Product_Buy(Product product)
     //   {
      //      product.Date_Buy = DateTime.Now;
     //       db.Products.Add(product);
      //      db.SaveChanges();
     //       return View();
    //    }
    
        [HttpGet]
        public ActionResult Supple_Get()
        {
            return View();
        }  //Создает запрос на пополнение ассортимента

        [HttpPost]
        public ActionResult Supple_Get(Supplement supple)
        {
            supple.Date_Supple_Begin = DateTime.Now;
            supple.Date_Supple_End = null;
            supple.Supple_Done = 0;
            db.Supplements.Add(supple);
            db.SaveChanges();
            ViewBag.S = supple;
            return View("Supple_Wait");
        }


        public RedirectResult Supple_Done(int id) //Запрос на пополнени деалют клинеты, а за его выполнение будут отвечать 
        {                                           //менеджеры, данный метод отвечает за выполнение запроса.
            Supplement supple = db.Supplements.Find(id);
            Product pr = new Product();
            pr.P_Name = supple.P_Name;
            pr.Year = supple.Year;
            pr.Date_Buy = DateTime.Now;
            pr.Have = 1;
            db.Products.Add(pr);
            db.SaveChanges();
            db.Supplements.Find(id).Product_Id = pr.Product_Id;
            db.Supplements.Find(id).Date_Supple_End = DateTime.Now;
            db.Supplements.Find(id).Supple_Done = 1;
            db.SaveChanges();
            return Redirect("/Look/SuppleView/");
        } 

        [HttpGet]
        public ActionResult Manager_Add() 
        {
            return View();
        } //добавление нового работника, напрямуюбез регистрации

        [HttpPost]
        public RedirectResult Manager_Add(Manager manager)
        {
            db.Managers.Add(manager);
            db.SaveChanges();
            return Redirect("/Look/ManagerView/");
        }

        public RedirectResult OrderChange(int id)
        {

                db.Orders.Find(id).Date_End = DateTime.Now;
                db.Orders.Find(id).Complete = 1;
                db.SaveChanges();
                int prod_id = db.Orders.Find(id).Product_Id;
                db.Products.Find(prod_id).Have = 1;
                db.SaveChanges();
                return Redirect("/Look/OrderView/");
          
        }  //Закрытие заказа
    }
}