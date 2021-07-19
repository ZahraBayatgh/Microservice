using Domain.AggregatesModel.StudentAggregate;
using Infrastructure;
using System.Linq;

namespace API.Infrastructure.Repositories
{
    public class StudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Student GetById(long id)
        {
            // Retrieving from the database
            return _context.Students.SingleOrDefault(x => x.Id == id);
        }

        public Student GetByEmail(Email email)
        {
            return _context.Students.SingleOrDefault(x => x.Email == email);
        }

        public void Create(Student student)
        {
            // Saving to the database
            _context.Students.Add(student);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
