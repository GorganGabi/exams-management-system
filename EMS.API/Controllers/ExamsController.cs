using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EMS.Business;
using EMS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exams_management_system.Controllers
{
    [VersionedRoute("api/exams", 1)]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamService examService;
        private readonly IGradeService gradeService;
        private readonly ICourseService courseService;

        public ExamsController(IExamService examService, IGradeService gradeService, ICourseService courseService)
        {
            this.examService = examService;
            this.gradeService = gradeService;
            this.courseService = courseService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetExams()
        {
            var exams = await examService.GetAll();

            return Ok(exams);
        }

        [Authorize]
        [HttpGet("checkIn", Name = "GetCheckedExams")]
        public async Task<IActionResult> GetCheckedExams()
        {
            var exams = await examService.GetAllCheckedExams();

            return Ok(exams);
        }

        [Authorize]
        [HttpGet("{id:guid}/grades", Name = "GetGradeByExamId")]
        public async Task<IActionResult> GetGradeByExamId(Guid id)
        {
            var grade = await gradeService.FindByExamId(id);

            if (grade.Count == 0)
            {
                return NotFound();
            }

            return Ok(grade);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateExam([FromBody] CreatingExamModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Todo: need to check all fields before entering a new one. 
            // Todo: Exams with the same date, but with different rooms, are two separate exams
            var exam = examService.FindByTimeAndRoomAndType(model.Date, model.Room, model.Type);
            if (exam.Result == null)
            {
            var examId = await examService.CreateNew(model);
            var students = await courseService.getAllStudentsByCourse(model.CourseId);
            await examService.AssignStudentsToExam(examId, students);
            return StatusCode(StatusCodes.Status201Created, examId);
            }

            return Conflict();
        }

        [Authorize]
        [HttpGet("{id:guid}", Name = "GetExamById")]
        public async Task<IActionResult> GetExamById(Guid id)
        {
            var exam = await examService.FindById(id);

            if (exam == null)
            {
                return NotFound();
            }

            return Ok(exam);
        }

        /*[HttpGet("{id:guid}/grades", Name = "GetExamGrades")]
        public async Task<IActionResult> GetExamGrades(Guid id)
        {
            var grades = await examService.GetAllExamsGrades(id);

            if (grades == null)
            {
                return NotFound();
            }

            return Ok(grades);
        }*/

        [Authorize]
        [HttpPut("{id:guid}", Name = "UpdateExam")]
        public async Task<IActionResult> UpdateExam([FromBody] UpdateExamModel updateExamModel, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exam = await examService.FindById(id);
            if (exam == null)
            {
                return NotFound();
            }

            var examModel = Mapper.Map<UpdateExamModel, Exam>(updateExamModel);
            await examService.Update(id, examModel);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id:guid}", Name = "DeleteExam")]
        public async Task<IActionResult> DeleteExam(Guid id)
        {
            var exam = await examService.FindById(id);
            if (exam == null)
            {
                return NotFound();
            }

            await examService.Delete(id);

            return NoContent();
        }
    }
}