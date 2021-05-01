using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace InstituteWeb.Models
{
    public class Usuario : IdentityUser
    {
      [Required]
      public bool isAdmin { get; set; }
    }
}