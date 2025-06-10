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
        private RoleManager<IdentityRole> _roleManager;
        private AppDbContext _context;

        public AccountController(UserManager<IdentityUser> usrMgr, SignInManager<IdentityUser> signInManager, 
            AppDbContext ctx, RoleManager<IdentityRole> roleManager)
        {
            _userManager = usrMgr;
            _signinManager = signInManager;
            _context = ctx;
            _roleManager = roleManager;
        }
        #endregion

        #region Students
        
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
            { 
                return RedirectToAction("ListStudents", "Students");
            }
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
                    IdentityUser usr = await _userManager.FindByNameAsync(model.UserName);
                    var rolesList = await _userManager.GetRolesAsync(usr);
                    //if (rolesList.Any() && rolesList.Where(r => r == "Admin").Any())
                    //{
                        return RedirectToAction("Index", "Home", new {area = "Administrator" });
                    //}
                    //return RedirectToAction("Index", "Home");
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

        [HttpGet]
        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }
        public IActionResult CreateRole()
        {
            return View();
        }
    }
}
