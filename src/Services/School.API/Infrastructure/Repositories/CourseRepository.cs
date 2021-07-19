using Domain.AggregatesModel.CourseAggregate;
using Infrastructure;
using System.Linq;

namespace API.Infrastructure.Repositories
{
    public sealed class CourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Course GetByName(string name)
        {
            return _context.Courses.SingleOrDefault(x => x.Name == name);
        }

        public Course[] GetAll()
        {
            return _context.Courses.ToArray();
        }
    }
}
