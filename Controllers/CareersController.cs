using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InstituteWeb.Data;
using InstituteWeb.Models;
using InstituteWeb.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace InstituteWeb.Controllers
{
    [Authorize(Roles = "Student")]
    public class CareersController : Controller
    {
        private readonly InstituteContext _context;

        public CareersController(InstituteContext context)
        {
            _context = context;
        }
        //Index Careers
        public async Task<IActionResult> Index()
        {   
            List<Career> getCareersFromDataBase = await  _context.Careers.ToListAsync();
            List<CareerViewModel> careersViewModel = getCareersFromDataBase
            .Select(career => new CareerViewModel()
            {
                CareerID = career.CareerID,
                Name = career.Name
            }).ToList();

            return View(careersViewModel);
        }

        
    }
}
