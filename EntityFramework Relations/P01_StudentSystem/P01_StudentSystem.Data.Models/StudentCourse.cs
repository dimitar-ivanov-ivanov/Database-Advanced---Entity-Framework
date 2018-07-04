namespace P01_StudentSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("StudentCourses")]
    public class StudentCourse
    {
        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
