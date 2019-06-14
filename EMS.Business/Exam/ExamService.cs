using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EMS.Domain;
using Microsoft.EntityFrameworkCore;

namespace EMS.Business
{
    public sealed class ExamService : IExamService
    {
        private readonly IRepository repository;

        public ExamService(IRepository repository) => this.repository = repository;

        public async Task<Guid> CreateNew(CreatingExamModel newExam)
        {
            var exam = Exam.Create(
                type: newExam.Type,
                date: newExam.Date,
                courseId: newExam.CourseId,
                room: newExam.Room);

            await repository.AddNewAsync(exam);
            await repository.SaveAsync();

            return exam.Id;
        }

        public Task<ExamDetailsModel> FindById(Guid id) => AllExamDetails.SingleOrDefaultAsync(e => e.Id == id);

        public Task<ExamDetailsModel> FindByTime(DateTime date) => AllExamDetails.SingleOrDefaultAsync(e => e.Date == date);

        public async Task Update(Guid id, Exam updatedExam)

        {
            var examToUpdate = await repository.FindByIdAsync<Exam>(id);

            await repository.TryUpdateModelAsync(
                    examToUpdate,
                    updatedExam
                    );

            await repository.SaveAsync();
        }

        public async Task Delete(Guid id)
        {
            var exam = await repository.FindByIdAsync<Exam>(id);
            await repository.RemoveAsync(exam);
            await repository.SaveAsync();

        }

        private IQueryable<ExamDetailsModel> AllExamDetails => repository.GetAll<Exam>()
            .Include(e => e.Course)
                //.ThenInclude(c => c.Professor)
          .Select(e => new ExamDetailsModel
          {
              Id = e.Id,
              Type = e.Type,
              Date = e.Date,
              Room = e.Room,
              Course = Mapper.Map<Course, CourseDetailsModel>(e.Course),
              imagePath = e.ImagePath,
              ProfessorIds = e.Course.ProfessorCourses.Where(pc => pc.CourseId == e.CourseId).Select(pc => pc.ProfessorId).ToList()
          });

        public Task<List<ExamDetailsModel>> GetAll() => repository.GetAll<Exam>()
          .Include(e => e.Course)
            //.ThenInclude(c => c.Professor)
          .Select(e => new ExamDetailsModel
          {
              Id = e.Id,
              Type = e.Type,
              Date = e.Date,
              Course = Mapper.Map<Course, CourseDetailsModel>(e.Course),
              Room = e.Room,
              imagePath = e.ImagePath,
              ProfessorIds = e.Course.ProfessorCourses.Where(pc => pc.CourseId == e.CourseId).Select(pc => pc.ProfessorId).ToList()
          }).ToListAsync();

        public Task<List<ExamDetailsModel>> GetAllCheckedExams() => repository.GetAll<Exam>()
            .Where(e => e.StudentExams.Any(se => se.ExamId == e.Id))
            .Select(e => new ExamDetailsModel
            {
                Id = e.Id
            })
            .Distinct()
            .ToListAsync();

        public Task<List<GradeDetailsModel>> GetAllExamsGrades(Guid id) => repository.GetAll<Grade>()
            .Where(g => g.ExamId == id)
            .Select(g => new GradeDetailsModel
            {
                Value = g.Value
            }).ToListAsync();
    }
}