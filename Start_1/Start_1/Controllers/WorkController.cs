﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Start_1.Models;
using System.Data.Entity;

namespace Start_1.Controllers
{
    public class WorkController : Controller
    {
        // GET: Work
        StoreContext db = new StoreContext();
        StoreContext db1 = new StoreContext();
        [HttpGet]
        public ActionResult SearchProduct()
        {
            return View();
        } //Поиск товара по наименованию
        [HttpPost]
        public ActionResult SearchProduct(string name)
        {
            foreach(Start_1.Models.Product p in db.Products)
            {
                if (String.Compare(p.P_Name,name)==0)
               ViewBag.Product = p;
            }
           
            return View("SearchProductResult");
        }

        public ActionResult ReturnTime(int id) //Возможность просмотра информации когда будет доступен продукт
        {
            foreach (Start_1.Models.Order or in db.Orders)
            {
                if (or.Product_Id==id && or.Complete==0)
                {
                    ViewBag.Order = or;
                    Product p= db1.Products.Find(id);
                    ViewBag.Product = p;
                    return View();
                }
            }

            return View();
        }         

        [HttpGet]
        public ActionResult SearchClient() //Поиск клиента
        {
            return View();
        }   
        [HttpPost]
        public ActionResult SearchClient(string name)
        {
            foreach (Start_1.Models.Client cl in db.Clients)
            {
                if (String.Compare(cl.Name, name) == 0)
                    ViewBag.Client = cl;
            }

            return View("SearchClientResult");
        }

    }
}