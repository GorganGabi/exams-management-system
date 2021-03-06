﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EMS.Domain;
using EMS.Domain.Entities;
using AutoMapper;
using Newtonsoft.Json.Linq;

namespace EMS.Business
{
    public sealed class ProfessorService : IProfessorService
    {
        private readonly IRepository repository;

        public ProfessorService(IRepository repository) => this.repository = repository;

        public async Task<Guid> CreateNew(Guid userId)
        {
            var professor = Professor.Create(
                userId: userId
                );

            await repository.AddNewAsync(professor);
            await repository.SaveAsync();

            return professor.Id;
        }

        public async Task<Guid> CreateNew(Guid userId, string json)
        {
            JObject response = JObject.Parse(json);
            var name = response["response"]["Name"].ToString();
            var title = response["response"]["Title"].ToString();
            var email = response["response"]["Email"].ToString();
            var courses = response["response"]["Courses"].ToString();

            var professor = Professor.Create(
                userId: userId,
                email: email,
                name: name,
                title: title                
                );

            await repository.AddNewAsync(professor);
            await repository.SaveAsync();

            return professor.Id;
        }

        public async Task UpdateAsync(Guid id, Professor professorUpdated)
        {
            var professorToUpdate = await repository.FindByIdAsync<Professor>(id);

            await repository.TryUpdateModelAsync(
                professorToUpdate,
                professorUpdated
            );
            await repository.SaveAsync();
        }

        public Task<List<ProfessorDetailsModel>> GetAll() => GetAllProfessorDetails().ToListAsync();

        public Task<ProfessorDetailsModel> FindByTitle(string title) => GetAllProfessorDetails().SingleOrDefaultAsync(p => p.Title == title);

        public Task<ProfessorDetailsModel> FindById(Guid id) => GetAllProfessorDetails().SingleOrDefaultAsync(p => p.Id == id);

        public Task<List<CourseDetailsModel>> GetCourseByProfId(Guid id) => repository.GetAll<Course>()
            .Where(c => c.ProfessorCourses.Any(pc => pc.ProfessorId == id))
            .Include(c => c.Exams)
            .Select(c => new CourseDetailsModel
            {
                Id = c.Id,
                Title = c.Title,
                Semester = c.Semester,
                StudentYear = c.StudentYear,
                UniversityYear = c.UniversityYear,
                Exams = Mapper.Map<List<Exam>, List<ExamDetailsModel>>(c.Exams),
                Professors = Mapper.Map<List<Professor>, List<ProfessorDetailsModel>>(c.ProfessorCourses.Where(pc => pc.ProfessorId == id).Select(pc => pc.Professor).ToList())
            }).ToListAsync();

        public Task<List<ExamDetailsModel>> GetExamByProfId(Guid id) => repository.GetAll<Exam>()
           .Where(e => e.Course.ProfessorCourses.Any(pc => pc.ProfessorId == id))
           .Include(e => e.Course)
           //.ThenInclude(c => c.Professor)
           .Select(e => new ExamDetailsModel
           {
               Room = e.Room,
               Id = e.Id,
               //CourseName = e.Course.Title,
               //CourseId = e.CourseId,
               Course = Mapper.Map<Course, CourseDetailsModel>(e.Course),
               Date = e.Date,
               Type = e.Type
           }).ToListAsync();

        private IQueryable<ProfessorDetailsModel> GetAllProfessorDetails() => repository.GetAll<Professor>()
            .Select(p => new ProfessorDetailsModel
            {
                Id = p.Id,
                UserId = p.UserId,
                Title = p.Title,
                Name = p.Name
            });

        public Task<ProfessorDetailsModel> FindByUserId(Guid id) => GetAllProfessorDetails().SingleOrDefaultAsync(p => p.UserId == id);
    }
}
