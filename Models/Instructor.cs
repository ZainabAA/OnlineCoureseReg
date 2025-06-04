using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourseReg.Models
{
    public class Instructor : IdentityUser
    {
        //public int Id { get; set; }
        //[Required(ErrorMessage = "Please enter name")]
        //[MinLength('3', ErrorMessage = "Min 3 char")]
        //public string Name { get; set; }
        //[EmailAddress]
        //public string Email { get; set; }
        public string Major { get; set; }
    }
}
