using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstituteWeb.Models
{
    public class Subject
    {
        [Key]
        public int SubjectID { get; set; }
        [Required]
        public int CareerID { get; set; }
        public Career Career { get; set; }
        [Required]
        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Schedule { get; set; }
        [Required]
        public int Maximum_Number_Of_Students { get; set; }
        public List<Student> Students { get; set; }

    }
}