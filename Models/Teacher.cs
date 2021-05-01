using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstituteWeb.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Last_Name { get; set; }
        [Required]
        public int DNI { get; set; }
        [Required]
        public string Active { get; set; }
        public List<Subject> Subjects { get; set; }
        [NotMapped]
        public string LastName_Name 
        { 
            get
            {
                return Last_Name + ", " + Name;
            }
        }

        
    }
}