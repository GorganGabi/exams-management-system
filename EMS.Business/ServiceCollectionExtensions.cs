using AutoMapper;
using EMS.Business;
using EMS.Domain;
using EMS.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Booking.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddTransient<IExamService, ExamService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IProfessorService, ProfessorService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IGradeService, GradeService>();

            return services;
        }

        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UpdateCourseModel, Course>();
                cfg.CreateMap<UpdateExamModel, Exam>();
                cfg.CreateMap<UpdateProfessorModel, Professor>();
                cfg.CreateMap<UpdateStudentModel, Student>();
                cfg.CreateMap<UpdateGradeModel, Grade>();

                cfg.CreateMap<Professor, ProfessorDetailsModel>();
                cfg.CreateMap<Exam, ExamDetailsModel>();
                cfg.CreateMap<Course, CourseDetailsModel>();
            });
            return services;
        }
    }
}
