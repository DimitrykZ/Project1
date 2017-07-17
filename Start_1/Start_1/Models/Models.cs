using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Start_1.Models
{
    public class LoginModel
    {
        public string Email{ get; set; }
        public string Password { get; set; }
    }
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
       

    }
}