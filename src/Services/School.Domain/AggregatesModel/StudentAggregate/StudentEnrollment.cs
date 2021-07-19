using CSharpFunctionalExtensions;

namespace Domain.AggregatesModel.StudentAggregate
{
    public class StudentEnrollment : Entity
    {
        public Student Student { get; private set; }
        public Enrollment Enrollment { get; private set; }
        public StudentEnrollment()
        {

        }
        public StudentEnrollment(Student student, Enrollment enrollment)
        {
            Student = student;
            Enrollment = enrollment;
        }
    }
}
