using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Start_1.Models
{
    public  class Person
    {
        [Key]
        public int Person_Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Phone_Number { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }


    }

    public class Client: Person
    {
        public int Level { get; set; }
        public int VIP { get; set; }
    }

    public class Manager: Person
    {
        public string Job_Title { get; set; }
        public int Salary { get; set; }
    }


    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}