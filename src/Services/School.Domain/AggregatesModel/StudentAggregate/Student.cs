using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;

namespace Domain.AggregatesModel.StudentAggregate
{
    public class Student : Entity
    {
        public Email Email { get; }
        public Name Name { get;  set; }
        public string Phone { get; set; }

        private readonly List<StudentEnrollment> _enrollments = new List<StudentEnrollment>();
        public virtual IReadOnlyList<StudentEnrollment> Enrollments => _enrollments.ToList();

        protected Student()
        {
        }

        public Student(Email email, Name name,string phone)
            : this()
        {
            Email = email;
            Phone = phone;
            EditPersonalInfo(name);
        }

        public void EditPersonalInfo(Name name)
        {
            Name = name;
        }

        public virtual Result<object, Error> Enroll(Enrollment[] enrollments)
        {
            if (_enrollments.Count + enrollments.Length > 2)
                return Errors.Student.TooManyEnrollments();

            StudentEnrollment existingEnrollment = _enrollments
                .FirstOrDefault(x => enrollments.Any(e => x.Enrollment == e));

            if (existingEnrollment != null)
                return Errors.Student.AlreadyEnrolled(existingEnrollment.Enrollment.Course.Name);

            foreach (Enrollment enrollment in enrollments)
            {
                _enrollments.Add(new StudentEnrollment(this, enrollment));
            }

            return new object(); // Unit class
        }
    }
}
