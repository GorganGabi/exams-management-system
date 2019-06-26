using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EMS.Domain.Entities;
using EMS.Domain;
using AutoMapper;
using Newtonsoft.Json.Linq;

namespace EMS.Business
{
    public sealed class StudentService : IStudentService
    {
        private readonly IRepository repository;

        public StudentService(IRepository repository) => this.repository = repository;

        public async Task<Guid> CreateNew(Guid userId)
        {
            var student = Student.Create(userId);

            await repository.AddNewAsync(student);
            await repository.SaveAsync();

            return student.Id;
        }

        public async Task<Guid> CreateNew(Guid userId, string json)
        {
            JObject response = JObject.Parse(json);
            var email = response["response"]["Email"].ToString();
            var rNumber = response["response"]["RegistrationNumber"].ToString();
            var group = response["response"]["Group"].ToString();
            var fInitial = response["response"]["FatherInitial"].ToString();
            var year = int.Parse(response["response"]["Year"].ToString());
            var name = response["response"]["Name"].ToString();
            var specialty = response["response"]["Specialty"].ToString();

            var student = Student.Create(
                userId: userId,
                fInitial: fInitial,
                group: group,
                year: year,
                rnumber: rNumber,
                email: email,
                name: name,
                specialty: specialty
                );

            await repository.AddNewAsync(student);
            await repository.SaveAsync();

            return student.Id;
        }

        public async Task<Guid> CreateNew(Guid userId, StudentExcelDetailsModel studentDetails)
        {
            var student = Student.Create(
                userId: userId,
                fInitial: studentDetails.FatherInitial,
                group: studentDetails.Group,
                year: studentDetails.Year,
                rnumber: studentDetails.RegistrationNumber,
                email: studentDetails.Email,
                name: studentDetails.Name,
                specialty: studentDetails.Specialty
                );

            await repository.AddNewAsync(student);
            await repository.SaveAsync();

            return student.Id;
        }

        public async Task<bool> AssignStudentExam(Guid id, Guid examId)
        {

            var student = await repository.FindByIdAsync<Student>(id);
            var exam = await repository.FindByIdAsync<Exam>(examId);
            var studentExam = new StudentExam(student, exam);

            exam.StudentExams.Add(studentExam);

            await repository.SaveAsync();
            return true;
        }

        public async Task<bool> AssignStudentCourse(Guid id, Guid courseId)
        {

            var student = await repository.FindByIdAsync<Student>(id);
            var course = await repository.FindByIdAsync<Course>(courseId);
            var studentCourse = new StudentCourse(student, course);

            course.StudentCourses.Add(studentCourse);

            await repository.SaveAsync();
            return true;
        }

        public async Task<bool> CheckExam(Guid id, Guid examId)
        {
            //todo: create a find on composite key instead of id from Entity base class
            var studentExam = repository.GetAll<StudentExam>().SingleOrDefault(se => se.ExamId == examId && se.StudentId == id);

            var updatedStudentExam = await repository.FindByIdAsync<StudentExam>(studentExam.Id);
            //updatedStudentExam.CheckIn(); //todo: test with this line; fix encapsulation          

            await repository.TryUpdateModelAsync(studentExam, updatedStudentExam);
            await repository.SaveAsync();

            return true;
        }

        public async Task UpdateAsync(Guid id, Student studentUpdated)
        {
            var studentToUpdate = await repository.FindByIdAsync<Student>(id);

            await repository.TryUpdateModelAsync(
                    studentToUpdate, studentUpdated);
            await repository.SaveAsync();
        }

        public Task<List<StudentDetailsModel>> GetAll() => GetAllStudentDetails().ToListAsync();

        public Task<StudentDetailsModel> FindById(Guid id) => GetAllStudentDetails().SingleOrDefaultAsync(p => p.Id == id);

        private IQueryable<StudentDetailsModel> GetAllStudentDetails() => repository.GetAll<Student>()
                .Select(s => new StudentDetailsModel
                {
                    Id = s.Id,
                    UserId = s.UserId,
                    FatherInitial = s.FatherInitial,
                    Name = s.Name,
                    Email = s.Email,
                    Group = s.Group,
                    RegistrationNumber = s.RegistrationNumber,
                    Courses = Mapper.Map<List<Course>, List<CourseDetailsModel>>(s.StudentCourses.Where(sc => sc.StudentId == s.Id)
                                                                                                 .Select(sc => sc.Course)
                                                                                                 .ToList()),
                    Exams = Mapper.Map<List<Exam>, List<ExamDetailsModel>>(s.StudentExams.Where(sc => sc.StudentId == s.Id)
                                                                                         .Select(sc => sc.Exam)
                                                                                         .ToList()),
                });

        public IQueryable<ExamDetailsModel> FindExamsByStudentId(Guid studId) => repository.GetAll<Exam>()
            .Where(e => e.StudentExams.Any(se => se.StudentId == studId))
            .Include(e => e.Course)
                //.ThenInclude(c => c.Professor)
            .Select(e => new ExamDetailsModel
            {
                Id = e.Id,
                Type = e.Type,
                Course = Mapper.Map<Course, CourseDetailsModel>(e.Course),
                Date = e.Date,
                Room = e.Room,           
            });

        public IQueryable<CourseDetailsModel> FindCoursesByStudentId(Guid studId) => repository.GetAll<Course>()
            .Where(c => c.StudentCourses.Any(cs => cs.StudentId == studId))
            .Include(c => c.Exams)
            .Select(c => new CourseDetailsModel
            {
                Id = c.Id,
                Title = c.Title,
                StudentYear = c.StudentYear,
                UniversityYear = c.UniversityYear,
                Semester = c.Semester,
                Exams = Mapper.Map<List<Exam>, List<ExamDetailsModel>>(c.Exams),
                Professors = Mapper.Map<List<Professor>, List<ProfessorDetailsModel>>(c.ProfessorCourses.Where(pc => pc.CourseId == c.Id).Select(pc => pc.Professor).ToList())
            });

        public Task<StudentDetailsModel> FindByUserId(Guid id) => GetAllStudentDetails().SingleOrDefaultAsync(s => s.UserId == id);

        public Task<StudentDetailsModel> FindbyName(string name) => GetAllStudentDetails().SingleOrDefaultAsync(s => s.Name == name);

        public Task<List<StudentDetailsModel>> FindStudentsbyName(string name) => repository.GetAll<Student>()
            .Where(s => s.Name.StartsWith(name))          
            .Select(s => new StudentDetailsModel
            {
                Name = s.Name,
                Id = s.Id
            }).ToListAsync();

        public Task<List<StudentDetailsModel>> FindStudentsbyNameAndCourse(string studentName, string courseName) => repository.GetAll<Student>()
            .Include(s => s.StudentCourses)
            .Where(s => s.Name.StartsWith(studentName) && s.StudentCourses.Any(sc => sc.Course.Title == courseName))
            .Select(s => new StudentDetailsModel
            {
                Name = s.Name,
                Id = s.Id
            }).ToListAsync();

        public Task<List<ExamDetailsModel>> FindCheckInExamsByStudentId(Guid id) => repository.GetAll<Exam>()
            .Where(e => e.StudentExams.Any(se => se.StudentId == id && se.Checked.Equals("yes")))
            .Select(e => new ExamDetailsModel
            {
                Id = e.Id
            }).ToListAsync();
    }
}