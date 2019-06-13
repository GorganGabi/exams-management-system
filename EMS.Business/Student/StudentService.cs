using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EMS.Domain.Entities;
using EMS.Domain;
using Newtonsoft.Json.Linq;
using AutoMapper;

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
            string email = response["response"]["Email"].ToString();
            string rNumber = response["response"]["NrMatricol"].ToString();
            string group = response["response"]["Grupa"].ToString();
            string fInitial = response["response"]["FatherInitial"].ToString();
            int year = Int32.Parse(response["response"]["year"].ToString());
            var student = Student.Create(
                userId: userId,
                fInitial: fInitial,
                group: group,
                year: year,
                rnumber: rNumber,
                email: email
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

        public async Task<bool> CheckExam(Guid id, Guid examId)
        {

            //var student = await repository.FindByIdAsync<Student>(id);
            //var exam = repository.GetAll<Exam>().Include(e => e.StudentExams).SingleOrDefault(e => e.Id == examId);

            var studentExam = repository.GetAll<StudentExam>().SingleOrDefault(se => se.ExamId == examId && se.StudentId == id);

            var updatedStudentExam = await repository.FindByIdAsync<StudentExam>(studentExam.Id);
            updatedStudentExam.Checked = "yes";

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
                Professor = Mapper.Map<Professor, ProfessorDetailsModel>(c.ProfessorCourses.FirstOrDefault(pc => pc.CourseId == c.Id).Professor)
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