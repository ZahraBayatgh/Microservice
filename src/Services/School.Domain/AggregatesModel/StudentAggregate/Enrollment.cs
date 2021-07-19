using CSharpFunctionalExtensions;
using Domain.AggregatesModel.CourseAggregate;
using System.Collections.Generic;
using System.Linq;

namespace Domain.AggregatesModel.StudentAggregate
{
    public class Enrollment : Entity
    {
        public Grade Grade { get; }
        public virtual Course Course { get; }
        public virtual Student Student { get; }
        public Enrollment()
        {

        }
        public Enrollment(Course course, Grade grade)
        {
            Course = course;
            Grade = grade;
        }

        public static Result<Enrollment[], Error> Create((string course, string grade)[] input, Course[] allCourses)
        {
            var result = new List<Enrollment>();

            foreach ((string courseName, string gradeName) in input)
            {
                Grade grade = Grade.Create(gradeName).Value;

                Course course = allCourses.SingleOrDefault(x => x.Name == courseName.Trim());
                if (course == null)
                    return Errors.Student.CourseIsInvalid();

                result.Add(new Enrollment(course, grade));
            }

            return result.ToArray();
        }
    }
}