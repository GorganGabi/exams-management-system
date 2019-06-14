using EMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Business
{
    public interface IStudentService
    {
        Task<List<StudentDetailsModel>> GetAll();

        Task<StudentDetailsModel> FindById(Guid id);

        Task<StudentDetailsModel> FindByUserId(Guid id);

        Task<StudentDetailsModel> FindbyName(string name);

        Task<Guid> CreateNew(Guid userId);

        Task<Guid> CreateNew(Guid userId, string json);

        Task<Guid> CreateNew(Guid userId, StudentExcelDetailsModel studentDetails);

        Task UpdateAsync(Guid id, Student studentUpdated);

        Task<bool> AssignStudentExam(Guid id, Guid examId);

        Task<bool> AssignStudentCourse(Guid id, Guid courseId);

        Task<bool> CheckExam(Guid id, Guid examId);

        IQueryable<ExamDetailsModel> FindExamsByStudentId(Guid studId);

        IQueryable<CourseDetailsModel> FindCoursesByStudentId(Guid studId);

        Task<List<StudentDetailsModel>> FindStudentsbyName(string name);

        Task<List<StudentDetailsModel>> FindStudentsbyNameAndCourse(string studentName, string courseName);

        Task<List<ExamDetailsModel>> FindCheckInExamsByStudentId(Guid id);
    }
}
