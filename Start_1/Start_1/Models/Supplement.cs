using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Start_1.Models
{
    public class Supplement //Модель для создания пополнения продукции
    {
        [Key]
        public int Supple_Id { get; set; }
        public int Product_Id { get; set; }
        public string P_Name { get; set; }
        public int Year { get; set; }
        public int Person_Id { get; set; }
        public DateTime Date_Supple_Begin { get; set; }
        public DateTime? Date_Supple_End { get; set; }
        public int? Supple_Done { get; set; }
    }
}