using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InstituteWeb.Models
{
    public class Students_Subjects
    {
        
        public int IDStudent { get; set; }
        public int IDSubject { get; set; }
        
        public Student Student  { get; set; }

        public Subject Subject  { get; set; }

    }
}        