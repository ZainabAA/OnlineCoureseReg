using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace OnlineCourseReg.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter course title")]
        public string Title { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public decimal Duration { get; set; }

        [ForeignKey("Instructor")]
        public string InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
        public List<Student>? Students { get; set; }
    }
}
