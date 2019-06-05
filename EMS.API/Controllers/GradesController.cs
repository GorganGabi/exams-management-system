using System;
using System.Threading.Tasks;
using AutoMapper;
using EMS.Business;
using EMS.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exams_management_system.Controllers
{
    [VersionedRoute("api/grades", 1)]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradeService gradeService;
        private readonly IStudentService studentService;

        public GradesController(IGradeService gradeService, IStudentService studentService)
        {
            this.gradeService = gradeService;
            this.studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGrades()
        {
            var exams = await gradeService.GetAll();

            return Ok(exams);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGrade([FromBody] CreatingGradeModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gradeId = await gradeService.CreateNew(model);

            if (gradeId == default(Guid))
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity);
            }

            var gradeModel = await gradeService.FindById(gradeId);
            var student = await studentService.FindById(gradeModel.StudentId);

            SMTPClient.ProfessorSendMail(gradeModel, student);
            return StatusCode(StatusCodes.Status201Created);

        }

        [HttpGet("{id:guid}", Name = "GetGradeById")]
        public async Task<IActionResult> GetGradeById(Guid id)
        {
            var grade = await gradeService.FindById(id);

            if (grade == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(grade);
        }

        [HttpPut("{id:guid}", Name = "UpdateGrade")]
        public async Task<IActionResult> UpdateGrade([FromBody] UpdateGradeModel updateGradeModel, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var grade = await gradeService.FindById(id);
            if (grade == null)
            {
                return NotFound();
            }

            var gradeModel = Mapper.Map<UpdateGradeModel, Grade>(updateGradeModel);

            await gradeService.Update(id, gradeModel);
            var updatedGrade = await gradeService.FindById(id);
            var student = await studentService.FindById(updatedGrade.StudentId);
        
            SMTPClient.ProfessorSendMail(updatedGrade, student);

            return NoContent();
        }

        [HttpDelete("{id:guid}", Name = "DeleteGrade")]
        public async Task<IActionResult> DeleteGrade(Guid id)
        {
            var grade = await gradeService.FindById(id);
            if (grade == null)
            {
                return NotFound();
            }

            await gradeService.Delete(id);
            return NoContent();
        }
    }
}