using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstituteWeb.Models.ViewModels
{
    public class SubjectViewModel
    {
        [Key]
        public int SubjectID { get; set; }
        [Required]
        public int CareerID { get; set; }
        [Required]
        public int TeacherID { get; set; }
        public int StudentID { get; set; }
        public Career Career { get; set; }
        public Teacher Teacher { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Schedule { get; set; }
        [Required]
        [Display(Name = "Maximum Of Students")]
        public int Maximum_Number_Of_Students { get; set; }
        public string Name_Teacher { get; set; }
        public string Last_Name_Teacher { get; set; }
        public string Name_Career { get; set; }

    }
}