using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseReg.Models.ViewModels;

namespace OnlineCourseReg.Controllers
{
    public class AccountController : Controller
    {
        #region Configuration
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signinManager;

        public AccountController(UserManager<IdentityUser> usrMgr, SignInManager<IdentityUser> signInManager)
        {
            _userManager = usrMgr;
            _signinManager = signInManager;
        }
        #endregion

        #region Users
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
                var result = await _signinManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);


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
        #endregion
    }
}
