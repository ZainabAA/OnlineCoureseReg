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
        public IActionResult ListCourses()
        {

            return View(_context.Courses);
        }
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

        [HttpGet]
        public IActionResult EditCourse(int id)
        {
            Course crs = _context.Courses.Where(c => c.Id == id).FirstOrDefault();
            ViewBag.AllInstructors = new SelectList(_context.Instructors, "Id", "UserName");
            if (crs != null)
            {
                return View(crs);

            }
            return RedirectToAction(nameof(ListCourses));
        }

        [HttpPost]
        public async Task<IActionResult> EditCourse(Course course)
        {
            ViewBag.AllInstructors = new SelectList(_context.Instructors, "Id", "UserName");

            if (ModelState.IsValid)
            {
                _context.Courses.Update(course);
                _context.SaveChanges();
                return RedirectToAction(nameof(ListCourses));
            }
            return View(course);
        }

        [HttpGet]
        public IActionResult DeleteCourse(int? id)
        {
            var current = _context.Courses.Where(e => e.Id == id).FirstOrDefault();
            return View(current);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCourse(Course course)
        {
            _context.Courses.Remove(course);
            _context.SaveChanges();
            return RedirectToAction(nameof(ListCourses));
        }

    }
}
