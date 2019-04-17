using EMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.Business
{
    public interface IProfessorService
    {
        Task<List<ProfessorDetailsModel>> GetAll();

        Task<ProfessorDetailsModel> FindByTitle(String Title);

        Task<ProfessorDetailsModel> FindById(Guid id);

        Task<Guid> CreateNew(Guid userId);

        Task UpdateAsync(Guid id, Professor professorUpdated);
    }
}
