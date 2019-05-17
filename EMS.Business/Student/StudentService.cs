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
                rnumber: rNumber
                );

            await repository.AddNewAsync(student);
            await repository.SaveAsync();

            return student.Id;
        }

        public async Task<bool> CheckExam(Guid id, Guid examId)
        {

            var student = await repository.FindByIdAsync<Student>(id);
            var exam = await repository.FindByIdAsync<Exam>(examId);
            var studentExam = new StudentExam(student, exam);

            exam.StudentExams.Add(studentExam);

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
            .Include(e => e.Course)
                .ThenInclude(s => s.StudentCourses)
            .Select(e => new ExamDetailsModel
            {
                Id = e.Id,
                Type = e.Type,
                CourseName = e.Course.StudentCourses.SingleOrDefault(sc => sc.StudentId == studId).Course.Title,
                Date = e.Date,
                Room = e.Room
            });
    }
}