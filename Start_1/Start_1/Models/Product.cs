using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Start_1.Models
{
    public class Product //Модель продукции
    {
        [Key]
        public int Product_Id { get; set; }
        public string P_Name { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public int? Year { get; set; }
        public int? Have { get; set; }
        public DateTime? Date_Buy { get; set; }
        public int? Cost { get; set; }

    }
}