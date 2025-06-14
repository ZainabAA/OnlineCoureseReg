﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCourseReg.Data;
using OnlineCourseReg.Models;
using OnlineCourseReg.Models.ViewModels;

namespace OnlineCourseReg.Controllers
{
    public class StudentsController : Controller
    {
        #region Configuration
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signinManager;
        private AppDbContext _context;

        public StudentsController(UserManager<IdentityUser> usrMgr, SignInManager<IdentityUser> signInManager,
            AppDbContext ctx)
        {
            _userManager = usrMgr;
            _signinManager = signInManager;
            _context = ctx;
        }
        #endregion

        #region Students
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ListStudents()
        {

            return View(_context.Students.Include(c => c.RegisteredCourses));
        }


        [HttpGet]
        public async Task<IActionResult> EditStudent(string id)
        {
            IdentityUser usr = (Student)await _userManager.FindByIdAsync(id);
            if (usr != null)
            {
                return View(usr);

            }
            return RedirectToAction("ListStudents", "Students");
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                Student usr = (Student)_userManager.Users.Where(r => r.Id == student.Id).FirstOrDefault();
                if (usr != null)
                {
                    usr.PhoneNumber = student.PhoneNumber;
                    usr.UserName = student.UserName;
                    usr.Email = student.Email;
                    usr.City = student.City;

                    var result = await _userManager.UpdateAsync(usr);
                    if (result.Succeeded)
                        return RedirectToAction("ListStudents", "Students");
                    else
                    {
                        foreach (var err in result.Errors)
                        {
                            ModelState.AddModelError(err.Code, err.Description);
                        }
                    }
                }
            }
            return View(student);
        }


        [HttpGet]
        public IActionResult DeleteStudent(string? id)
        {
            var current = _context.Students.Where(e => e.Id == id).FirstOrDefault();
            return View(current);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStudent(Student student)
        {
            Student usr = (Student)_userManager.Users.Where(r => r.Id == student.Id).FirstOrDefault();
            if (usr != null)
            {
                var result = await _userManager.DeleteAsync(usr);
                if (result.Succeeded)
                    return RedirectToAction("ListStudents", "Students");
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
            }
            return View(student);
        }

        #endregion

    }
}
