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
        StoreContext db2 = new StoreContext();
        Person user=new Person();
        List<Order> orders = new List<Order>();

        public void Login()  //вспомогательный метод, получает информациюо зарегистрированном пользователе
        {
         user = db.Clients.FirstOrDefault(u => u.Email == User.Identity.Name);
            if (user==null)
           user = db.Managers.FirstOrDefault(u => u.Email == User.Identity.Name);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult ClientView( string order="Name")  //отвечает за создание создание таблицы  клиентов
        {
            var s = from us in db.Clients select us;
            switch (order)  //используется для сортировки по таблицам
            {
                case "Name": s = db.Clients.OrderBy(p => p.Name); break;
                case "Email": s = db.Clients.OrderBy(p => p.Email); break;
                case "Phone_Number": s = db.Clients.OrderBy(p => p.Phone_Number); break;
                case "Level": s = db.Clients.OrderByDescending(p => p.Level); break;
                case "VIP": s = db.Clients.OrderByDescending(p => p.VIP); break;
                default: s = db.Clients.OrderBy(p => p.Name); break;
            }
            ViewBag.Clients = s;
            return View();
        }

        [Authorize(Roles = "Manager")]
        public ActionResult ManagerView(string order = "Name") //отвечает за создание создание таблицы  работников компании
        {
            var s = from us in db.Managers select us;
            switch (order)
            {
                case "Name": s = db.Managers.OrderBy(p => p.Name); break;
                case "Email": s = db.Managers.OrderBy(p => p.Email); break;
                case "Phone_Number": s = db.Managers.OrderBy(p => p.Phone_Number); break;
                case "Salary": s = db.Managers.OrderByDescending(p => p.Salary); break;
                case "Job_Title": s = db.Managers.OrderByDescending(p => p.Job_Title); break;
                default: s = db.Managers.OrderBy(p => p.Name); break;
            }
            ViewBag.Managers = s;
            return View();
        }
     

        public ActionResult ProductView(string order="P_Name") //отвечает за создание создание таблицы  продукции компании
        {
            var s = from prod in db.Products select prod;
            switch (order)
            {
                case "P_Name": s = db.Products.OrderBy(p => p.P_Name); break;
                case "Year": s = db.Products.OrderBy(p => p.Year); break;
                case "Director": s = db.Products.OrderBy(p => p.Director); break;
                case "Genre": s = db.Products.OrderBy(p => p.Genre); break;
                case "Cost": s = db.Products.OrderBy(p => p.Cost); break;
                case "Have": s = db.Products.OrderByDescending(p => p.Have); break;
                default: s = db.Products.OrderBy(p => p.P_Name); break;
            }
            ViewBag.Products = s;
            ViewBag.Authen = User.Identity.IsAuthenticated;
            Login();
            if (user != null)
            {
                ViewBag.User = user;
            };
            return View();
        }

        [HttpPost]
        public ActionResult SearchTime(int id) //метод для обработки запроса,отвечающий за показ времени возвращения продукта
        {
            foreach (Start_1.Models.Order or in db.Orders)
            {
                if (or.Product_Id == id && or.Complete == 0)
                {
                    ViewBag.date = or.Date_End.ToString();
                    return PartialView();
                }
            }

            return PartialView();
        }





        [Authorize(Roles ="Manager")]
        public ActionResult OrderView(string order="Name") //отвечает за создание создание таблицы заказов для работников
        {
            var s = from ord in db.Orders select ord;

            Login();
            switch (order)
            {
                case "Person_Id": s = db.Orders.OrderBy(p => p.Person_Id); break;
                case "Product_Id":
                    var pr = from prod in db.Products select prod;
                    pr = db.Products.OrderBy(u => u.P_Name);
                    foreach (Product product in pr)
                    {
                        foreach (Order o in db2.Orders)
                            if (o.Product_Id == product.Product_Id)
                                orders.Add(o);
                        continue;
                    }
                    s = orders.AsQueryable().Cast<Order>();
                    break;
                case "Date_Begin": s = db.Orders.OrderBy(p => p.Date_Begin); break;
                case "Date_End": s = db.Orders.OrderBy(p => p.Date_End); break;
                case "Price": s = db.Orders.OrderBy(p => p.Price); break;
                case "Complete": s = db.Orders.OrderBy(p => p.Complete); break;
                default: s = db.Orders.OrderBy(p => p.Person_Id); break;
            }
            ViewBag.Orders = s;
            ViewBag.Products = db2.Products;
            ViewBag.Clients = db2.Clients;
            return View();
        }

        [Authorize]
        public ActionResult OrderUser(string order = "Name")//отвечает за создание создание таблицы  закзаков для клиента
        {
            var s = from ord in db.Orders select ord;
            Login();
            switch(order)
            {
                case "Product_Id":
                    var pr = from prod in db.Products select prod;
                    pr = db.Products.OrderBy(u => u.P_Name);

                    foreach (Product product in pr)
                    {
                        foreach (Order o in db2.Orders)
                            if (o.Product_Id == product.Product_Id)
                                orders.Add(o);
                        continue;
                    }
                    s=orders.AsQueryable().Cast<Order>();
                break;
                case "Date_Begin": s = db.Orders.OrderBy(p => p.Date_Begin); break;
                case "Date_End": s = db.Orders.OrderBy(p => p.Date_End); break;
                case "Price": s = db.Orders.OrderBy(p => p.Price); break;
                case "Complete": s = db.Orders.OrderBy(p => p.Complete); break;
                default: s = db.Orders.OrderBy(p => p.Person_Id); break;
            }

            ViewBag.Orders = s;
            ViewBag.Products = db2.Products;
            ViewBag.Person_Id = user.Person_Id;
            return View();
        }

        public ActionResult SuppleView(string order="P_Name") //отвечает за создание создание таблицы  пополнения ассортимента
        {
            var s = from supple in db.Supplements select supple;
            Login();
            if (user != null)
            {
                ViewBag.User = user;
            }

            switch (order)
            {
                case "P_Name": s = db.Supplements.OrderBy(p => p.P_Name); break;
                case "Year": s = db.Supplements.OrderBy(p => p.Year); break;
                case "Date_Supple_Begin": s = db.Supplements.OrderBy(p => p.Date_Supple_Begin); break;
                case "Date_Supple_End": s = db.Supplements.OrderBy(p => p.Date_Supple_End); break;
                case "Supple_Done": s = db.Supplements.OrderBy(p => p.Supple_Done); break;
                default: s = db.Supplements.OrderBy(p => p.P_Name); break;
            }

            ViewBag.Supplements = s;
            return View();
        }

    }
}