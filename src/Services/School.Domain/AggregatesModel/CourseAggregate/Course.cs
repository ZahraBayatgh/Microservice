using CSharpFunctionalExtensions;

namespace Domain.AggregatesModel.CourseAggregate
{
    public class Course : Entity
    {
        public string Name { get; private set; }
        public int Credits { get; private set; }
        public Course(long id, string name, int credits)
        {
            Id = id;
            Name = name;
            Credits = credits;
        }
    }
}
