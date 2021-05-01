using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstituteWeb.Models.ViewModels
{
    public class TeacherViewModel
    {
        [Key]
        public int TeacherID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string Last_Name { get; set; }
        [Required]
        public int DNI { get; set; }
        [Required]
        public string Active { get; set; }

        

        
    }
}