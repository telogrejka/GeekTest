﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GeekTest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    [Table("results")]
    public partial class results
    {
        public int id { get; set; }
        [Display(Name = "Пользователь")]
        public string user_name { get; set; }
        [Display(Name = "Название теста")]
        public string test_name { get; set; }
        [Display(Name = "Результат, %")]
        public double point { get; set; }
        [Display(Name = "Дата прохождения теста")]
        public System.DateTime date { get; set; }
    }
}
