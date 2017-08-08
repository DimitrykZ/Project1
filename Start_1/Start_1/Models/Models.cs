using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Start_1.Models  
{
    public class LoginModel //Модель для авторизации
    {
        public string Name{ get; set; }
        public string Password { get; set; }
    }
    public class RegisterModel  //Модель для регистрации
    {
        public string Name { get; set; }
        public string First_name { get; set; }
        public decimal Phone_Number { get; set; }
        public string Password { get; set; }
       

    }
}