﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using EMS.Business;
using EMS.Domain;
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

        public ExamsController(IExamService examService, IGradeService gradeService)
        {
            this.examService = examService;
            this.gradeService = gradeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetExams()
        {
            var exams = await examService.GetAll();

            return Ok(exams);
        }

        [HttpGet("{id:guid}/grades", Name = "GetGradeByExamId")]
        public async Task<IActionResult> GetGradeByExamId(Guid id)
        {
            var grade = await gradeService.FindByExamId(id);

            if (grade.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(grade);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExam([FromBody] CreatingExamModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            // Todo: need to check all fields before entering a new one. 
            // Todo: Exams with the same date, but with different rooms, are two separate exams
            var exam = examService.FindByTime(model.Date);
            if (exam.Result == null)
            {
                var examId = await examService.CreateNew(model);
                return StatusCode(StatusCodes.Status201Created, examId);
            }

            return StatusCode(StatusCodes.Status422UnprocessableEntity);
        }

        [HttpGet("{id:guid}", Name = "GetExamById")]
        public async Task<IActionResult> GetExamById(Guid id)
        {
            var exam = await examService.FindById(id);

            if (exam == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(exam);
        }

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
                return StatusCode(StatusCodes.Status404NotFound);
            }

            var examModel = Mapper.Map<UpdateExamModel, Exam>(updateExamModel);
            await examService.Update(id, examModel);

            return NoContent();
        }

        [HttpDelete("{id:guid}", Name = "DeleteExam")]
        public async Task<IActionResult> DeleteExam(Guid id)
        {
            var exam = await examService.FindById(id);
            if (exam == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            await examService.Delete(id);

            return NoContent();
        }
    }
}