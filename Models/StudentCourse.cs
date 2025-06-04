using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCourseReg.Models
{
    public class StudentCourse
    {
        public int Id { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        [ForeignKey("Student")]
        public string StudentId { get; set; }
    }
}
