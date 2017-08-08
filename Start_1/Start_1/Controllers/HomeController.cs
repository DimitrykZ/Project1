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
        Person user = new Person();

        public void Login()  //вспомогательный метод, получает информациюо зарегистрированном пользователе
        {
            user = db.Clients.FirstOrDefault(u => u.Email == User.Identity.Name);
            if (user == null)
                user = db.Managers.FirstOrDefault(u => u.Email == User.Identity.Name);
        }

        public RedirectResult Index()
        {
            return Redirect("/Look/ProductView");
        }
     
        [Authorize]
        [HttpGet]
        public ActionResult GetOrder(int id)  // GetOrder отвечате за формирование заказа
        {
            Product p = db.Products.Find(id);
            ViewBag.P = p;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult GetOrder(Order order, uint Days,int id) // GetOrder отвечате за формирование заказа 
        {
            Login();
            order.Date_Begin = DateTime.Now;
            order.Date_End = DateTime.Now.AddDays(Days);
            order.Price = db.Products.Find(id).Cost * (int?)Days;
            order.Complete = 0;
            order.Person_Id = user.Person_Id;
            db.Orders.Add(order);
            db.SaveChanges();
            db.Products.Find(id).Have = 0;
            db.SaveChanges();
            db.Clients.Find(user.Person_Id).Level++;
            if (db.Clients.Find(user.Person_Id).Level > 15)
                db.Clients.Find(user.Person_Id).VIP = 1;
            db.SaveChanges();
            ViewBag.Order = order;
            return View("Order_Done");
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult ClientNew() //Данный класс отвечает за создание нового клиента напрямую, безрегистрации
        {
            return View();
        }  

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public RedirectResult ClientNew(Client client)////Данный класс отвечает за создание нового клиента напрямую, безрегистрации
        {

            client.Level = 0;
            client.VIP = 0;
            client.RoleId = 1;
            db.Clients.Add(client);
            db.SaveChanges();
            return Redirect("/Look/ClientView");
        }
        [Authorize]
        [HttpGet]
        public ActionResult Supple_Get() //Создает запрос на пополнение ассортимента
        {
            return View();
        }  

        [Authorize]
        [HttpPost]
        public ActionResult Supple_Get(Supplement supple) //Создает запрос на пополнение ассортимента
        {
            supple.Date_Supple_Begin = DateTime.Now;
            supple.Date_Supple_End = null;
            supple.Supple_Done = 0;
            db.Supplements.Add(supple);
            db.SaveChanges();
            ViewBag.S = supple;
            return View("Supple_Wait");
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult Supple_Done(int id) //Метод отвечает за выполнение пополнения ассортимента
        {
            ViewBag.id = id;
            return View();
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public RedirectResult Supple_Done(int id, string genre, string director, uint cost) //Метод отвечает за выполнение пополнения ассортимента 
        {                                           //менеджеры, данный метод отвечает за выполнение запроса.
            Supplement supple = db.Supplements.Find(id);
            Product pr = new Product();
            pr.Cost = (int?)cost;
            pr.Director = director;
            pr.Genre = genre;
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

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult Manager_Add() 
        {
            return View();
        } //добавление нового работника, напрямуюбез регистрации

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public RedirectResult Manager_Add(Manager manager)//Добавление работника
        {
            manager.RoleId = 2;
            db.Managers.Add(manager);
            db.SaveChanges();
            return Redirect("/Look/ManagerView/");
        }

        [Authorize(Roles = "Manager")]
        public RedirectResult OrderChange(int id) //Закрытие заказа
        {

                db.Orders.Find(id).Date_End = DateTime.Now;
                db.Orders.Find(id).Complete = 1;
                db.SaveChanges();
                int prod_id = db.Orders.Find(id).Product_Id;
                db.Products.Find(prod_id).Have = 1;
                db.SaveChanges();
                return Redirect("/Look/OrderView/");
          
        }  
    }
}