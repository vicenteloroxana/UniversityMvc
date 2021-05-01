using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstituteWeb.Models.ViewModels
{
    public class ElementJsonKey
    {
        [Key]
        public int Value { get; set; }
        public string Text { get; set; }
        
    }
}