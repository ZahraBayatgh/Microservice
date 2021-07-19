using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace Domain.AggregatesModel.StudentAggregate
{
    public class Name : ValueObject
    {
        public string FullName { get; }
        public string Last { get; }
        protected Name()
        {
        }

        private Name(string name)
            : this()
        {
            FullName = name;
        }

        public static Result<Name> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Name>("First name should not be empty");
           
            name = name.Trim();

            if (name.Length > 200)
                return Result.Failure<Name>("First name is too long");

            return Result.Success(new Name(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FullName;
            yield return Last;
        }
    }
}