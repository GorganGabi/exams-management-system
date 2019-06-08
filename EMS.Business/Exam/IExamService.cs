using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.Business
{
  public interface IExamService
    {
        Task<List<ExamDetailsModel>> GetAll();

        Task<ExamDetailsModel> FindById(Guid id);

        Task<ExamDetailsModel> FindByTime (DateTime date);

        Task<Guid> CreateNew(CreatingExamModel newExam);

        Task Update(Guid id, Domain.Exam examModel);

        Task Delete(Guid id);

        Task<List<ExamDetailsModel>> GetAllCheckedExams();
    }
 }
