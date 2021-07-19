using Domain.AggregatesModel.CourseAggregate;
using Domain.AggregatesModel.StudentAggregate;

namespace API.Models
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
