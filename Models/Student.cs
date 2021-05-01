using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InstituteWeb.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        
        public int CareerID { get; set; }
        public Career Career { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Last_Name { get; set; }
        [Required]   
        public int DNI { get; set; }
        [Required]
        public string File { get; set; }
        public List<Subject> Subjects { get; set; }
        
    }
}