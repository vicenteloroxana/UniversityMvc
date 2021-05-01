using System.ComponentModel;
using System.Globalization;
using System;
using System.Net;
using System.Reflection;
using System.Transactions;
using System.Security.Cryptography.X509Certificates;
using System.Web;
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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace InstituteWeb.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly InstituteContext _context;
        private readonly RoleManager<IdentityRole>gestionRoles;
        private readonly UserManager<Usuario>gestionUsuarios;

        public SubjectsController(RoleManager<IdentityRole>gestionRoles, UserManager<Usuario> gestionUsuarios,InstituteContext context)
        {
            this.gestionRoles=gestionRoles;
            this.gestionUsuarios = gestionUsuarios;
            _context = context;
        }
        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(s => s.SubjectID == id);
        }
        [Authorize(Roles = "Student")]
        public int StudentID(int dni)
        {   
            
            var studentIdBD = 
                from st in _context.Students
                where st.DNI == dni
                select st.StudentID;
            var idStudent = studentIdBD.First();    
            return (idStudent);
        }
        [Authorize(Roles = "Administrator")]
        //List All Subjects
        public async Task<IActionResult> IndexAll()
        {
            // var subjectsBD = await _context.Subjects.ToListAsync();
            var subjectsBD = await (from sub in   _context.Subjects
                            join teach in _context.Teachers on sub.TeacherID equals teach.TeacherID
                            join career in _context.Careers on sub.CareerID equals career.CareerID
                            orderby sub.Name ascending
                            select new { 
                                SubjectID = sub.SubjectID,
                                Name = sub.Name, 
                                Schedule = sub.Schedule,
                                Maximum_Number_Of_Students = sub.Maximum_Number_Of_Students,
                                Name_Subj = teach.Name,
                                Last_Name = teach.Last_Name,
                                Name_Career = career.Name
                            }).ToListAsync();
            List<SubjectViewModel> subjects = new List<SubjectViewModel>();
            
            foreach (var item in subjectsBD)
            {
                subjects.Add(new SubjectViewModel() 
                {
                    SubjectID = item.SubjectID,
                    Name = item.Name,
                    Schedule = item.Schedule,
                    Maximum_Number_Of_Students = item.Maximum_Number_Of_Students,
                    Name_Teacher = item.Name_Subj,
                    Last_Name_Teacher = item.Last_Name,
                    Name_Career = item.Name_Career
                });
            }
            
            return View(subjects);
        }
        [Authorize(Roles = "Student")]
        //List Subjects according to Career
        public async Task<IActionResult> Index(int idCareer)
        { 
            
            
            var subjectsBD = await (from sub in   _context.Subjects
                            join teach in _context.Teachers on sub.TeacherID equals teach.TeacherID
                            where sub.CareerID == idCareer
                            orderby sub.Name ascending
                            select new { 
                                SubjectID = sub.SubjectID,
                                Name = sub.Name, 
                                Schedule = sub.Schedule,
                                Maximum_Number_Of_Students = sub.Maximum_Number_Of_Students,
                                Name_Subj = teach.Name,
                                Last_Name = teach.Last_Name
                            }).ToListAsync();
            List<SubjectViewModel> subjectsViewModel = new List<SubjectViewModel>();
            
            foreach (var item in subjectsBD)
            {
                subjectsViewModel.Add(new SubjectViewModel() 
                {
                    
                    SubjectID = item.SubjectID,
                    Name = item.Name,
                    Schedule = item.Schedule,
                    Maximum_Number_Of_Students = item.Maximum_Number_Of_Students,
                    Name_Teacher = item.Name_Subj,
                    Last_Name_Teacher = item.Last_Name
                    
                });
            }                
               
            return View(subjectsViewModel);
        }
        [Authorize(Roles = "Administrator,Student")]
        public IActionResult Details(int idSubject)
        {             
            var subjectBD = from sub in   _context.Subjects
                            join teach in _context.Teachers on sub.TeacherID equals teach.TeacherID
                            join career in _context.Careers on sub.CareerID equals career.CareerID
                            
                            where sub.SubjectID == idSubject
                            //select sub).ToListAsync();
                            select new { 
                                SubjectID = sub.SubjectID,
                                Name = sub.Name, 
                                Schedule = sub.Schedule,
                                Maximum_Number_Of_Students = sub.Maximum_Number_Of_Students,
                                Name_Subj = teach.Name,
                                Last_Name = teach.Last_Name,
                                Name_Career = career.Name,
                                CareerID = career.CareerID
                            };
            var subject = subjectBD.First();                            
            SubjectViewModel subjectsViewModel = new SubjectViewModel();
            subjectsViewModel.SubjectID = subject.SubjectID;
            subjectsViewModel.CareerID = subject.CareerID;
            subjectsViewModel.Name = subject.Name;
            subjectsViewModel.Schedule = subject.Schedule;
            subjectsViewModel.Maximum_Number_Of_Students = subject.Maximum_Number_Of_Students;
            subjectsViewModel.Name_Career = subject.Name_Career;
            subjectsViewModel.Name_Teacher = subject.Name_Subj;
            subjectsViewModel.Last_Name_Teacher = subject.Last_Name;             
            return View(subjectsViewModel);
        }
        [Authorize(Roles = "Student")]
        //Sign Up Subject
        public void Inscription(int idSubject)
        {
            string userName = HttpContext.User.Identity.Name;
            string userId = gestionUsuarios.GetUserId(HttpContext.User);            
            int userDni = int.Parse(userName);
            int studentId = StudentID(userDni);
            var studentSubject = new Students_Subjects();
            studentSubject.IDSubject = idSubject;
            studentSubject.IDStudent = studentId;
            _context.Students_Subjects.Add(studentSubject);
            _context.SaveChanges();
                
        }

        [Authorize(Roles = "Administrator")]
        //CREATE GET
        [HttpGet]
        public IActionResult Create(){
            //cargar en un select los profesores
            ViewData["TeacherID"] = new SelectList(_context.Teachers, "TeacherID", "LastName_Name");
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "Name");
            return View();
            
        }
        [Authorize(Roles = "Administrator")]
        //CREATE POST
        [HttpPost]
        public async Task<IActionResult> Create( SubjectViewModel subject)
        {
            if (ModelState.IsValid)
            {
                Subject subjectModel = new Subject();
                subjectModel.Name = subject.Name;
                subjectModel.Schedule = subject.Schedule;
                subjectModel.Name = subject.Name;
                subjectModel.TeacherID = subject.TeacherID;
                subjectModel.CareerID = subject.CareerID;
                subjectModel.Maximum_Number_Of_Students = subject.Maximum_Number_Of_Students;
                

                _context.Add(subjectModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexAll));
            }
            ViewData["TeacherID"] = new SelectList(_context.Teachers, "TeacherID", "LastName_Name", subject.TeacherID);
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "Name", subject.CareerID);
            return View(subject);
        }
        [Authorize(Roles = "Administrator")]
        //EDIT GET
        public async Task<IActionResult> Edit(int? idSubject)
        {
            if (idSubject == null)
            {
                return NotFound();
            }
            var subjectBD = await (from sub in   _context.Subjects
                            // join teach in _context.Teachers on sub.SubjectID equals teach.SubjectID
                            // join career in _context.Careers on sub.CareerID equals career.CareerID
                            
                            where sub.SubjectID == idSubject
                            //select sub).ToListAsync();
                            select new { 
                                SubjectID = sub.SubjectID,
                                Name = sub.Name, 
                                Schedule = sub.Schedule,
                                Maximum_Number_Of_Students = sub.Maximum_Number_Of_Students,
                                TeacherID = sub.TeacherID,
                                CareerID = sub.CareerID
                            }).FirstOrDefaultAsync(); 
            if (subjectBD == null)
            {
                return NotFound();
            }        
            SubjectViewModel subjectViewModel = new SubjectViewModel();

            subjectViewModel.SubjectID = subjectBD.SubjectID;
            subjectViewModel.Name = subjectBD.Name;
            subjectViewModel.Schedule = subjectBD.Schedule;
            subjectViewModel.Maximum_Number_Of_Students = subjectBD.Maximum_Number_Of_Students;
            subjectViewModel.TeacherID = subjectBD.TeacherID;
            subjectViewModel.CareerID = subjectBD.CareerID;  
            ViewData["TeacherID"] = new SelectList(_context.Teachers, "TeacherID", "LastName_Name");
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "Name");        
            return View(subjectViewModel);
            
        }
        [Authorize(Roles = "Administrator")]
        //EDIT POST
        [HttpPost]
        public async Task<IActionResult> Edit(SubjectViewModel subject)
        {

            if (ModelState.IsValid)
            {
                
                try
                {
                    var subjectBD = await _context.Subjects.FirstOrDefaultAsync(s => s.SubjectID == subject.SubjectID);
                    subjectBD.TeacherID = subject.TeacherID;
                    subjectBD.CareerID = subject.CareerID;
                    subjectBD.Name = subject.Name;
                    subjectBD.Schedule = subject.Schedule;
                    subjectBD.Maximum_Number_Of_Students = subject.Maximum_Number_Of_Students;

                    _context.Subjects.Update(subjectBD);
                    await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.SubjectID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("IndexAll");
            }
            
            return View(subject);
        }
        [Authorize(Roles = "Administrator")]
        //GET DELETE
        public IActionResult Delete(int? idSubject)
        {
            if (idSubject == null)
            {
                return NotFound();
            }

            var subjectBD = from sub in   _context.Subjects
                            join teach in _context.Teachers on sub.TeacherID equals teach.TeacherID
                            join career in _context.Careers on sub.CareerID equals career.CareerID
                            
                            where sub.SubjectID == idSubject
                            //select sub).ToListAsync();
                            select new { 
                                SubjectID = sub.SubjectID,
                                Name = sub.Name, 
                                Schedule = sub.Schedule,
                                Maximum_Number_Of_Students = sub.Maximum_Number_Of_Students,
                                Name_Subj = teach.Name,
                                Last_Name = teach.Last_Name,
                                Name_Career = career.Name
                            };
            if (subjectBD == null)
            {
                return NotFound();
            }
            var subject = subjectBD.First();                            
            SubjectViewModel subjectsViewModel = new SubjectViewModel();
            subjectsViewModel.SubjectID = subject.SubjectID;
            subjectsViewModel.Name = subject.Name;
            subjectsViewModel.Schedule = subject.Schedule;
            subjectsViewModel.Maximum_Number_Of_Students = subject.Maximum_Number_Of_Students;
            subjectsViewModel.Name_Career = subject.Name_Career;
            subjectsViewModel.Name_Teacher = subject.Name_Subj;
            subjectsViewModel.Last_Name_Teacher = subject.Last_Name;             
            

            return View(subjectsViewModel);
            
        }
        //POST DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        // GET POST
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return RedirectToAction("IndexAll");
        }

    }
}