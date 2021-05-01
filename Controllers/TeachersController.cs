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
    [Authorize(Roles = "Administrator")]
    public class TeachersController : Controller
    {
        private readonly InstituteContext _context;

        public TeachersController(InstituteContext context)
        {
            _context = context;
        }

        //GET: Profesores
        public async Task<IActionResult> Index()
        {
            List<Teacher> getTeacherFromDataBase =await  _context.Teachers.ToListAsync();
            List<TeacherViewModel> teachersViewModel = getTeacherFromDataBase
                .Select(teacher => new TeacherViewModel(){
                    TeacherID = teacher.TeacherID,
                    Name = teacher.Name,
                    Last_Name = teacher.Last_Name,
                    DNI = teacher.DNI,
                    Active = teacher.Active
                }).ToList();
            
            return View(teachersViewModel);
        }
        [HttpGet]
        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TeacherViewModel model){
            if(ModelState.IsValid){
                Teacher teacher = new Teacher(); 
                teacher.Name = model.Name; 
                teacher.Last_Name = model.Last_Name; 
                teacher.DNI = model.DNI; 
                teacher.Active = model.Active; 
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Details(int? idTeacher)
        {
            if ( idTeacher == null)
            {
                return NotFound();
            }

            var teacherBD = await _context.Teachers
                .FirstOrDefaultAsync(m => m.TeacherID == idTeacher);
            TeacherViewModel model= new TeacherViewModel();    
            model.TeacherID = teacherBD.TeacherID;  
            model.Name = teacherBD.Name;
            model.Last_Name = teacherBD.Last_Name;
            model.DNI = teacherBD.DNI;    
            model.Active = teacherBD.Active;
            
            if (teacherBD == null)
            {
                return NotFound();
            }

            return View(model);
        }
        //EDIT GET
        public async Task<IActionResult> Edit(int? idTeacher)
        {
            if (idTeacher == null)
            {
                return NotFound();
            }

            var teacherBD = await _context.Teachers.FirstOrDefaultAsync(t => t.TeacherID == idTeacher);
            TeacherViewModel model= new TeacherViewModel();   
            model.TeacherID = teacherBD.TeacherID;  
            model.Name = teacherBD.Name;
            model.Last_Name = teacherBD.Last_Name;
            model.DNI = teacherBD.DNI;
            model.Active = teacherBD.Active;
            if (teacherBD == null)
            {
                return NotFound();
            }
            return View(model);
            
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TeacherViewModel model)
        {
            

            if (ModelState.IsValid)
            {
                try
                {
                    var teacherBD = await _context.Teachers.FirstOrDefaultAsync(t => t.TeacherID == model.TeacherID);

                    
                    teacherBD.Name = model.Name;
                    teacherBD.Last_Name = model.Last_Name;
                    teacherBD.DNI = model.DNI;
                    teacherBD.Active = model.Active;
                    _context.Teachers.Update(teacherBD);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(model.TeacherID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            
            return View(model);
        }
        
        
        public async Task<IActionResult> Delete(int? idTeacher)
        {
            if (idTeacher == null)
            {
                return NotFound();
            }

            var teacherBD = await _context.Teachers
                .FirstOrDefaultAsync(t => t.TeacherID == idTeacher);
            TeacherViewModel model= new TeacherViewModel();  
            model.TeacherID = teacherBD.TeacherID;       
            model.Name = teacherBD.Name; 
            model.Last_Name = teacherBD.Last_Name; 
            model.DNI = teacherBD.DNI; 
            model.Active = teacherBD.Active; 
            if (teacherBD == null)
            {
                return NotFound();
            }

            return View(model);
        }
        //delete post 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacherActive = await _context.Teachers.FindAsync(id);
            Console.WriteLine(id);
        
            teacherActive.Active = "Inactivo";
            _context.Teachers.Update(teacherActive);
            await _context.SaveChangesAsync();
        
            return RedirectToAction("Index");
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(t => t.TeacherID == id);
        }
    }
}
