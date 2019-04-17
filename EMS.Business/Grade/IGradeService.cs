using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.Business
{
    public interface IGradeService
    {
        Task<List<GradeDetailsModel>> GetAll();

        Task<GradeDetailsModel> FindById(Guid id);

        Task<List<GradeDetailsModel>> FindByExamId(Guid id);

        Task<List<GradeDetailsModel>> FindByStudentId(Guid id);

        Task<Guid> CreateNew(CreatingGradeModel newGrade);

        Task Update(Guid id, Domain.Grade examModel);

        Task Delete(Guid id);
    }
}
