using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.Business
{
  public interface IExamService
    {
        Task<List<ExamDetailsModel>> GetAll();

        Task<ExamDetailsModel> FindById(Guid id);

        Task<ExamDetailsModel> FindByTimeAndRoomAndType (DateTime date, string room, string type, Guid id);

        Task<Guid> CreateNew(CreatingExamModel newExam);

        Task Update(Guid id, Domain.Exam examModel);

        Task Delete(Guid id);

        Task<List<ExamDetailsModel>> GetAllCheckedExams();

        Task<List<GradeDetailsModel>> GetAllExamsGrades(Guid id);

        Task AssignStudentsToExam(Guid id, List<Guid> studentsId);
    }
 }
