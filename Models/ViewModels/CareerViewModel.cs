using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstituteWeb.Models.ViewModels
{
    public class CareerViewModel
    {
        [Key]
        public int CareerID { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public List<Subject> Subjects { get; set; }
        
        
    }
}