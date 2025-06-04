using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourseReg.Models
{
    public class CourseStudent
    {
        public int Id { get; set; }
        [ForeignKey("Course")]
        public int? CourseId { get; set; }
        public Course? Course { get; set; }
        [ForeignKey("Student")]
        public string? StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
