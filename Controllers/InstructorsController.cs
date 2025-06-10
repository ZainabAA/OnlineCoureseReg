using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCourseReg.Data;
using OnlineCourseReg.Models;

namespace OnlineCourseReg.Controllers
{
    public class InstructorsController : Controller
    {
        #region Configuration
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signinManager;
        private RoleManager<IdentityRole> _roleManager;
        private AppDbContext _context;

        public InstructorsController(UserManager<IdentityUser> usrMgr, SignInManager<IdentityUser> signInManager,
            AppDbContext ctx, RoleManager<IdentityRole> roleManager)
        {
            _userManager = usrMgr;
            _signinManager = signInManager;
            _context = ctx;
            _roleManager = roleManager;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListInstructors()
        {

            return View(_context.Instructors);
        }
        [HttpGet]
        public async Task<IActionResult> EditInstructor(string id)
        {
            IdentityUser usr = (Instructor)await _userManager.FindByIdAsync(id);
            if (usr != null)
            {
                return View(usr);

            }
            return RedirectToAction(nameof(ListInstructors));
        }

        [HttpPost]
        public async Task<IActionResult> EditInstructor(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                Instructor usr = (Instructor)_userManager.Users.Where(r => r.Id == instructor.Id).FirstOrDefault();
                if (usr != null)
                {
                    usr.PhoneNumber = instructor.PhoneNumber;
                    usr.UserName = instructor.UserName;
                    usr.Email = instructor.Email;
                    usr.Major = instructor.Major;

                    var result = await _userManager.UpdateAsync(usr);
                    if (result.Succeeded)
                        return RedirectToAction("ListInstructors", "Instructors");
                    else
                    {
                        foreach (var err in result.Errors)
                        {
                            ModelState.AddModelError(err.Code, err.Description);
                        }
                    }
                }
            }
            return View(instructor);
        }

        [HttpGet]
        public IActionResult DeleteInstructor(string? id)
        {
            var current = _context.Instructors.Where(e => e.Id == id).FirstOrDefault();
            return View(current);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteInstructor(Instructor instructor)
        {
            Instructor usr = (Instructor)_userManager.Users.Where(r => r.Id == instructor.Id).FirstOrDefault();
            if (usr != null)
            {
                var result = await _userManager.DeleteAsync(usr);
                if (result.Succeeded)
                    return RedirectToAction("ListInstructors", "Instructors");
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
            }
            return View(instructor);
        }


    }
}
