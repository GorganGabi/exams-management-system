using EMS.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.Business
{
    public interface ICourseService
    {
        Task<List<CourseDetailsModel>> GetAll();

        Task<CourseDetailsModel> FindByTitle(String Title);

        Task<CourseDetailsModel> FindById(Guid id);

        Task<List<GradeDetailsModel>> GetCourseGrades(Guid id);

        Task<Guid> CreateNew(CreatingCourseModel newCourse);

        Task Update(Guid id, Course updatedCourse);

        Task Delete(Guid id);

        Task<bool> AssignStudentToCourse(Guid courseId, Guid studentId);

        Task<ProfessorDetailsModel> GetProfessorCourse(Guid id);
    }
}
