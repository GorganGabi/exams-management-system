using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Domain;
using EMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMS.Business
{
    public sealed class GradeService : IGradeService
    {
        private readonly IRepository repository;

        public GradeService(IRepository repository) => this.repository = repository;

        public async Task<Guid> CreateNew(CreatingGradeModel newGrade)
        {
            var student = await repository.FindByIdAsync<Student>(newGrade.StudentId);
            var exam = await repository.FindByIdAsync<Exam>(newGrade.ExamId);
            var tmpGrade = repository.GetAll<Grade>().Where(e => e.StudentId == student.Id && e.ExamId == exam.Id).FirstOrDefault();

            if (tmpGrade != null)
            {
                return default(Guid);
            }

            var grade = Grade.Create(
                value: newGrade.Value,
                examId: newGrade.ExamId,
                studentId: newGrade.StudentId
                );
                

            await repository.AddNewAsync(grade);
            await repository.SaveAsync();

            return grade.Id;
        }

        public Task<List<GradeDetailsModel>> GetAll() => AllGradeDetails.ToListAsync();

        public Task<GradeDetailsModel> FindById(Guid id) => AllGradeDetails.SingleOrDefaultAsync(g => g.Id == id);

        public Task<List<GradeDetailsModel>> FindByExamId(Guid examId)
        => repository.GetAll<Grade>()
                .Where(g => g.ExamId == examId)
                .Include(g => g.Exam)
                .Include(g => g.Student)
                    .ThenInclude(s => s.StudentCourses)
                .Select(eg => new GradeDetailsModel
                {
                    Id = eg.Id,
                    ExamName = eg.Exam.Course.Title,
                    StudentName = eg.Student.Name,
                    Value = eg.Value,
                    ExamId = eg.ExamId,
                    StudentId = eg.StudentId,
                    IsConfirmed = eg.IsConfirmed
                }).ToListAsync();

        public Task<List<GradeDetailsModel>> FindByStudentId(Guid studentId) => repository.GetAll<Grade>()
                .Where(g => g.StudentId == studentId)
                .Include(g => g.Exam)
                .Include(g => g.Student)
                    .ThenInclude(s => s.StudentCourses)
                .Select(eg => new GradeDetailsModel
                {
                    Id = eg.Id,
                    ExamName = eg.Exam.Course.Title,
                    StudentName = eg.Student.Name,
                    Value = eg.Value,
                    ExamId = eg.ExamId,
                    StudentId = eg.StudentId,
                    IsConfirmed = eg.IsConfirmed
                }).ToListAsync();


        public async Task Update(Guid id, Grade updatedGrade)
        {
            var gradeToUpdate = await repository.FindByIdAsync<Grade>(id);

            await repository.TryUpdateModelAsync(
                    gradeToUpdate,
                    updatedGrade
                    );
            await repository.SaveAsync();
        }

        public async Task Delete(Guid id)
        {
            var grade = await repository.FindByIdAsync<Grade>(id);
            await repository.RemoveAsync(grade);
            await repository.SaveAsync();
        }

        private IQueryable<GradeDetailsModel> AllGradeDetails => repository.GetAll<Grade>()
          .Select(g => new GradeDetailsModel
          {
              Id = g.Id,
              Value = g.Value,
              ExamName = g.Exam.Course.Title,
              StudentName = g.Student.Name,
              ExamId = g.ExamId,
              StudentId = g.StudentId,
              IsConfirmed = g.IsConfirmed
          });


    }
}