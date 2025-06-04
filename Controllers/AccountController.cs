using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineCourseReg.Data;
using OnlineCourseReg.Models;
using OnlineCourseReg.Models.ViewModels;

namespace OnlineCourseReg.Controllers
{
    public class AccountController : Controller
    {
        #region Configuration
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signinManager;
        private AppDbContext _context;

        public AccountController(UserManager<IdentityUser> usrMgr, SignInManager<IdentityUser> signInManager, 
            AppDbContext ctx)
        {
            _userManager = usrMgr;
            _signinManager = signInManager;
            _context = ctx;
        }
        #endregion

        #region Students
        [HttpGet]
        public IActionResult ListStudents()
        {

            return View(_context.Students.Include(c => c.RegisteredCourses));
        }

        [HttpGet]
        public IActionResult RegisterStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStudent(RegisterStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Student user = new Student
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    City = model.City,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                    return RedirectToAction("Login", "Account");

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditStudent(string id)
        {
            IdentityUser usr = (Student)await _userManager.FindByIdAsync(id);
            if (usr != null)
            {
                return View(usr);

            }
            return RedirectToAction(nameof(ListStudents));
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
                        return RedirectToAction(nameof(ListStudents));
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
                    return RedirectToAction(nameof(ListStudents));
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
            }
            return View(student);
        }

        #endregion

        #region Instructor
        [HttpGet]
        public IActionResult RegisterInstructor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterInstructor(RegisterInstructorViewModel model)
        {
            if (ModelState.IsValid)
            {
                Instructor user = new Instructor
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Major = model.Major,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                    return RedirectToAction("Login", "Account");

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
            }
            return View(model);
        }
        #endregion

        [HttpGet]
        public IActionResult Login()
        {
            if (_signinManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //IdentityUser user = _userManager.Users.Where(usr => usr.Email == model.Email).FirstOrDefault();
                var result = await _signinManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);


                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credentials");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
