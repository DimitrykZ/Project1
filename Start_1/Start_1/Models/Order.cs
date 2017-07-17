using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Start_1.Models
{
    public class Order
    {   [Key]
        public int Order_Id { get; set; }
        public int Person_Id { get; set; }
        public int Product_Id { get; set; }
        public DateTime Date_Begin { get; set; }
        public DateTime Date_End { get; set; }
        public int? Price { get; set; }
        public int Complete { get; set; }
    }

}