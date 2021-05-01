using System.Net;
using System.Reflection;
using System.Xml.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using InstituteWeb.Data;
using InstituteWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InstituteWeb.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace InstituteWeb.Controllers
{
    public class HomeController : Controller
    {
        // private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole>gestionRoles;
        private readonly UserManager<Usuario>gestionUsuarios;
        private readonly InstituteContext _context;
        // public HomeController(ILogger<HomeController> logger)
        // {
        //     _logger = logger;
        // }
        public HomeController(InstituteContext context,RoleManager<IdentityRole>gestionRoles, UserManager<Usuario> gestionUsuarios)
        {
            _context = context;
            this.gestionRoles=gestionRoles;
            this.gestionUsuarios = gestionUsuarios;
        }

        public IActionResult Index()
        {
            ViewBag.UserId = gestionUsuarios.GetUserId(HttpContext.User);
            return View();
        }
        

        

        
    }
}
