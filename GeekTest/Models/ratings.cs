//------------------------------------------------------------------------------
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

    [Table("ratings")]
    public partial class ratings
    {
        public int id { get; set; }
        [Display(Name = "������������")]
        public string userName { get; set; }
        [Display(Name = "�������")]
        public Nullable<double> rating { get; set; }
    }
}
