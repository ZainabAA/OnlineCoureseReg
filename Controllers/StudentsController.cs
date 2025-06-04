using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseReg.Models;
using OnlineCourseReg.Models.ViewModels;

namespace OnlineCourseReg.Controllers
{
    public class StudentsController : Controller
    {
        #region Configuration
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signinManager;

        public StudentsController(UserManager<IdentityUser> usrMgr, SignInManager<IdentityUser> signInManager)
        {
            _userManager = usrMgr;
            _signinManager = signInManager;
        }
        #endregion

        #region Students
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterStudentViewModel model)
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

    }
}
