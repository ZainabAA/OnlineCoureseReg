using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineCourseReg.Data;
using OnlineCourseReg.Models;

namespace OnlineCourseReg.Controllers
{
    public class CoursesController : Controller
    {
        #region Configuration
        private AppDbContext _context;

        public CoursesController(AppDbContext ctx)
        {
            _context = ctx;
        }
        #endregion

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AllInstructors = new SelectList(_context.Instructors, "Id", "UserName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Course course)
        {
            ViewBag.AllInstructors = new SelectList(_context.Instructors, "Id", "UserName");
            if (ModelState.IsValid)
            {
                // Save Data
                _context.Courses.Add(course);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");//(nameof(Index));
            }
            return View(course);
        }

    }
}
