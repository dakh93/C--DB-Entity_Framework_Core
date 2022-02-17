
namespace StudentSystemDatabase.Data.Models
{
    public class Student
    {
        public Student()
        {
            this.HomeworkSubmissions = new List<HomeworkSubmission>();
            this.StudentsCourses = new List<StudentCourse>();
        }

        public int StudentId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime? BirthDate { get; set; }

        public ICollection<HomeworkSubmission> HomeworkSubmissions { get; set; }

        public ICollection<StudentCourse> StudentsCourses { get; set; }
    }
}
