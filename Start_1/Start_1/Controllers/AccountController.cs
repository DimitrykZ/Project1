using Start_1.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;


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
        public ActionResult Register(RegisterModel model)
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
                        db.Clients.Add(new Client { Email = model.Email, Password = model.Password, Name = model.Name, RoleId = 1, Level=0,VIP=0 });
                        db.SaveChanges();

                        user = db.Clients.Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefault();
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
        } //Регистрация нового клиента

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                Client user = null;
                using (StoreContext db = new StoreContext())
                {
                    user = db.Clients.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return RedirectToAction("ProductView", "Look");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        } //Вход под своим логином
    }
}