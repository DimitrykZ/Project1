using Start_1.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System;

namespace Start_1.Controllers
{
    public class AccountController : Controller //Класс ответчает за регистрацию клиентов и за вход по своим логином
    {
        //
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)  //Регистрация нового клиента
        {
            if (ModelState.IsValid)
            {
                Client user = null;
                using (StoreContext db = new StoreContext())
                {
                    user = db.Clients.FirstOrDefault(u => u.Email == model.Name);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (StoreContext db = new StoreContext())
                    {
                        db.Clients.Add(new Client { Email = model.Name, Password = model.Password, Name = model.First_name,
                                                   Phone_Number=model.Phone_Number, RoleId = 1, Level=0,VIP=0 });
                        db.SaveChanges();

                        user = db.Clients.Where(u => u.Email == model.Name && u.Password == model.Password).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("ProductView", "Look");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        } 

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)  //Авторизация под своим логином
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                Person user = null;
                using (StoreContext db = new StoreContext())
                {
                    user = db.Clients.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password);
                    if (user==null)
                   user = db.Managers.FirstOrDefault(u => u.Email == model.Name && u.Password == model.Password);
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("ProductView", "Look");          
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        } 

        [Authorize]
        public ActionResult Exit()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("ProductView", "Look");
        }
    }
}