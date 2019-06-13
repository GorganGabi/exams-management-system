using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EMS.Domain;
using EMS.Domain.Entities;
using AutoMapper;

namespace EMS.Business
{
    public sealed class CourseService : ICourseService
    {
        private readonly IRepository repository;

        public CourseService(IRepository repository) => this.repository = repository;

        public async Task<Guid> CreateNew(CreatingCourseModel newCourse)
        {
            var professor = await repository.FindByIdAsync<Professor>(newCourse.ProfessorId);

            var course = Course.Create(
                title: newCourse.Title,
                universityYear: newCourse.UniversityYear,
                studentYear: newCourse.StudentYear,
                semester: newCourse.Semester);//,
                                              //professorId: newCourse.ProfessorId);

            await repository.AddNewAsync(course);
            await repository.SaveAsync();

            return course.Id;
        }

        public Task<List<CourseDetailsModel>> GetAll() => GetAllCourseDetails().ToListAsync();

        public Task<CourseDetailsModel> FindByTitle(string title) => GetAllCourseDetails().SingleOrDefaultAsync(s => s.Title == title);

        public Task<CourseDetailsModel> FindById(Guid id) => GetAllCourseDetails().SingleOrDefaultAsync(s => s.Id == id);

        private IQueryable<CourseDetailsModel> GetAllCourseDetails() => repository.GetAll<Course>()
            .Include(c => c.Exams)
            .Select(c => new CourseDetailsModel
            {
                Id = c.Id,
                Title = c.Title,
                UniversityYear = c.UniversityYear,
                Professors = Mapper.Map<List<Professor>, List<ProfessorDetailsModel>>(c.ProfessorCourses.Where(pc => pc.CourseId == c.Id).Select(pc => pc.Professor).ToList()),
                Exams = Mapper.Map<List<Exam>, List<ExamDetailsModel>>(c.Exams),
                StudentYear = c.StudentYear,
                Semester = c.Semester,
                Description = c.Description,
                Url = c.Url

            });

        public async Task Update(Guid id, Course updatedCourse)
        {
            var courseToUpdate = await repository.FindByIdAsync<Course>(id);

            await repository.TryUpdateModelAsync(courseToUpdate, updatedCourse);
            await repository.SaveAsync();
        }

        public async Task Delete(Guid id)
        {
            var course = await repository.FindByIdAsync<Course>(id);

            await repository.RemoveAsync(course);
            await repository.SaveAsync();
        }

        public async Task<bool> AssignStudentToCourse(Guid courseId, Guid studentId)
        {
            var course = await repository.FindByIdAsync<Course>(courseId);
            var student = await repository.FindByIdAsync<Student>(studentId);
            var studentCourse = new StudentCourse(student, course);

            course.StudentCourses.Add(studentCourse);

            await repository.SaveAsync();
            return true;
        }

        public Task<ProfessorDetailsModel> GetProfessorCourse(Guid id) => repository.GetAll<Course>()
            .Include(c => c.ProfessorCourses)
            .Where(c => c.ProfessorCourses.Any(pc => pc.ProfessorId == id))
            .Select(c => new ProfessorDetailsModel
            {
                Name = c.ProfessorCourses.FirstOrDefault(pc => pc.ProfessorId == id).Professor.Name,
                Title = c.ProfessorCourses.FirstOrDefault(pc => pc.ProfessorId == id).Professor.Title,
            }).SingleOrDefaultAsync();

        public Task<List<GradeDetailsModel>> GetCourseGrades(Guid id) => repository.GetAll<Grade>()
            .Where(g => g.Exam.CourseId == id)
            //.Include(g => g.Exam)
            //.Include(g => g.Student)
            .Select(g => new GradeDetailsModel
            {
                Value = g.Value,
                ExamName = g.Exam.Course.Title,
                ExamId = g.ExamId,
                Student = Mapper.Map<Student, StudentDetailsModel>(g.Student),
                Id = g.Id
            }).ToListAsync();
    }
}
