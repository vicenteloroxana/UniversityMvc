using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Data;
using System.Data.Common;
using InstituteWeb.Data;
using InstituteWeb.Models;
using InstituteWeb.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InstituteWeb.Controllers
{
    public class StudentsController : Controller
    {
        private InstituteContext _context;
        
        public StudentsController(InstituteContext context)
        {
            _context = context;
        }
        
        // //lIsta de materias
        // public IActionResult Index()
        // {
        //     IEnumerable<Subject> subjects = _context.Subjects;
        //     return View(subjects);
        // }
        //inscipcion
        public IActionResult Inscription()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            
                lst = (from d in _context.Careers
                        select new SelectListItem
                        {
                            Value = d.CareerID.ToString(),
                            Text = d.Name
                        }
                    ).ToList();
             
            
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "SubjectID", "Name");
            return View(lst);
        }
        [HttpGet]
        public JsonResult Subjects (int id){
            List<ElementJsonKey> lst = new List<ElementJsonKey>();
            
                lst = (from d in _context.Subjects
                        where d.CareerID == id
                        select new ElementJsonKey
                        {
                            Value = d.SubjectID,
                            Text = d.Name
                        }
                    ).ToList();
            
            return Json(lst);
        }
        // [HttpPost]
        // public IActionResult Guardar([Bind("Nombre,Apellido,Email,Domicilio,Telefono,FechaNacimiento,DNI,Legajo")] Alumno alumno)
        // {
        //     alumno.ID = Guid.NewGuid();
            
        //     _context.Alumnos.Add(alumno);
        //     _context.SaveChanges();

        //     return View("Details", alumno);
        // }

        // public IActionResult Edit(Guid id)
        // {
        //     Alumno alumnoEditar = _context.Alumnos.Find(id);
        //     return View(alumnoEditar);
        // }

        // [HttpPost]
        // public IActionResult Edit([Bind("ID,Nombre,Apellido,Email,Domicilio,Telefono,FechaNacimiento,DNI,Legajo")] Alumno alumno)
        // {
        //     _context.Alumnos.Update(alumno);
        //     _context.SaveChanges();

        //     return View(alumno);
        // }
    }
}