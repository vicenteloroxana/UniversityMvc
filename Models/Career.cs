using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstituteWeb.Models
{
    public class Career
    {
        [Key]
        public int CareerID { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Student> Students { get; set; }
        
        
    }
}