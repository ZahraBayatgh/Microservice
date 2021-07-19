using API.Controllers.Base;
using API.Infrastructure.Repositories;
using API.Models;
using CSharpFunctionalExtensions;
using Domain.AggregatesModel;
using Domain.AggregatesModel.CourseAggregate;
using Domain.AggregatesModel.StudentAggregate;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace API.Controllers
{
    [Route("api/students")]
    public class StudentController : ApplicationController
    {
        private readonly StudentRepository _studentRepository;
        private readonly CourseRepository _courseRepository;

        public StudentController(
            StudentRepository studentRepository, CourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        [HttpPost]
        public IActionResult Register(RegisterRequest request)
        {
            Email email = Email.Create(request.Email).Value;
            var name = Name.Create(request.Name);

            Student existingStudent = _studentRepository.GetByEmail(email);
            if (existingStudent != null)
                return Error(Errors.Student.EmailIsTaken());

            var student = new Student(email, name.Value,request.Phone);
            _studentRepository.Create(student);
            _studentRepository.Save();

            var response = new RegisterResponse
            {
                Id = student.Id
            };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult EditPersonalInfo(long id, EditPersonalInfoRequest request)
        {
            Student student = _studentRepository.GetById(id);
            if (student == null)
                return Error(Errors.General.NotFound(), nameof(id));

            var name =Name.Create( request.Name);

            student.EditPersonalInfo(name.Value);
            _studentRepository.GetByEmail(student.Email);
            _studentRepository.Save();
            return Ok();
        }

        [HttpPost("{id}/enrollments")]
        public IActionResult Enroll(long id, EnrollRequest request)
        {
            Student student = _studentRepository.GetById(id);
            if (student == null)
                return Error(Errors.General.NotFound(), nameof(id));

            (string Course, string Grade)[] input = request.Enrollments
                .Select(x => (x.Course, x.Grade))
                .ToArray();
            Course[] allCourses = _courseRepository.GetAll();

            Result<Enrollment[], Error> enrollmentsOrError = Enrollment.Create(input, allCourses);
            if (enrollmentsOrError.IsFailure)
                return Error(enrollmentsOrError.Error);

            Result<object, Error> result = student.Enroll(enrollmentsOrError.Value);
            if (result.IsFailure)
                return Error(result.Error);

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            Student student = _studentRepository.GetById(id);

            var response = new GetResponse
            {
             
                Email = student.Email.Value,
                Name = student.Name.FullName,
                Enrollments = student.Enrollments.Select(x => new CourseEnrollmentDto
                {
                    Course = x.Enrollment.Course.Name,
                    Grade = x.Enrollment.Grade.ToString()
                }).ToArray()
            };
            return Ok(response);
        }
    }
}
