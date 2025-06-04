using System.ComponentModel.DataAnnotations;

namespace OnlineCourseReg.Models.ViewModels
{
    public class RegisterStudentViewModel
    {
        [Required(ErrorMessage = "Enter Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter Email Address")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Email Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Enter Email Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string? City { get; set; }
    }
}
