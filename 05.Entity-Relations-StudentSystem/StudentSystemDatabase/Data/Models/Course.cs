
namespace StudentSystemDatabase.Data.Models
{
    public class Course
    {
        public Course()
        {
            this.StudentsCourses = new List<StudentCourse>();
            this.HomeworkSubmissions = new List<HomeworkSubmission>();
            this.Resources = new List<Resource>();
        }

        public int CourseId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<StudentCourse> StudentsCourses { get; set; }
        public ICollection<HomeworkSubmission> HomeworkSubmissions { get; set; }

        public ICollection<Resource> Resources { get; set; }  

    }
}
