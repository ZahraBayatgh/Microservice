namespace API.Models
{
    public class GetResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public CourseEnrollmentDto[] Enrollments { get; set; }
    }
}
