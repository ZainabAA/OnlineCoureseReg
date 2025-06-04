using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OnlineCourseReg.Models
{
    public class Student : IdentityUser
    {
        //[Required(ErrorMessage = "Please enter name")]
        //[MinLength('3', ErrorMessage = "Min 3 char")]
        //public string Name { get; set; }
        //[EmailAddress]
        //public string Email { get; set; }
        //public int Phone { get; set; }
        public string City { get; set; }
        public List<Course>? RegisteredCourses { get; set; }
    }
}
