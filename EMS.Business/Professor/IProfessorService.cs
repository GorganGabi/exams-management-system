using EMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Business
{
    public interface IProfessorService
    {
        Task<List<ProfessorDetailsModel>> GetAll();

        Task<ProfessorDetailsModel> FindByTitle(String Title);

        Task<List<CourseDetailsModel>> GetCourseByProfId(Guid id);

        Task<List<ExamDetailsModel>> GetExamByProfId(Guid id);

        Task<ProfessorDetailsModel> FindByUserId(Guid id);

        Task<Guid> CreateNew(Guid userId);

        Task UpdateAsync(Guid id, Professor professorUpdated);
    }
}
