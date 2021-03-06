﻿using System.Threading.Tasks;
using EMS.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using EMS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace exams_management_system.Controllers
{
    [VersionedRoute("api/students", 1)]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentService studentService;
        private readonly IGradeService gradeService;

        public StudentsController(IStudentService studentService, IGradeService gradeService)
        {
            this.studentService = studentService;
            this.gradeService = gradeService;
        }

        [Authorize]
        [HttpGet("{id:guid}/grades", Name = "GetGradeByStudentId")]
        public async Task<IActionResult> GetGradeByStudentId(Guid id)
        {
            var grade = await gradeService.FindByStudentId(id);

            if (grade == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(grade);
        }

        [Authorize]
        [HttpGet("{id:guid}/exams", Name = "GetExamsByStudentId")]
        public async Task<IActionResult> GetExamsByStudentId(Guid id)
        {
            var exam = studentService.FindExamsByStudentId(id);

            if (exam == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(exam);
        }

        [Authorize]
        [HttpGet("{id:guid}/exams/checkin", Name = "GetCheckInExamsByStudentId")]
        public async Task<IActionResult> GetCheckInExamsByStudentId(Guid id)
        {
            var exams = await studentService.FindCheckInExamsByStudentId(id);

            if (exams == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(exams);
        }

        [Authorize]
        [HttpGet("{id:guid}/courses", Name = "GetCoursesByStudentId")]
        public async Task<IActionResult> GetCoursesByStudentId(Guid id)
        {
            var courses = studentService.FindCoursesByStudentId(id);

            if (courses == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(courses);
        }

        [Authorize]
        [HttpGet("{id:guid}", Name = "GetStudentById")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var student = await studentService.FindById(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [Authorize]
        [HttpGet("{name}", Name = "GetStudentsByName")]
        public async Task<IActionResult> GetStudentsByName(string name)
        {
            var students = await studentService.FindStudentsbyName(name);

            if (students == null)
            {
                return NotFound();
            }

            return Ok(students);
        }

        [Authorize]
        [HttpGet("{studentName}/{courseName}", Name = "GetStudentsByNameAndCourse")]
        public async Task<IActionResult> GetStudentsByNameAndCourse(string studentName, string courseName)
        {
            var students = await studentService.FindStudentsbyNameAndCourse(studentName, courseName);

            if (students == null)
            {
                return NotFound();
            }

            return Ok(students);
        }

        [Authorize]
        [HttpPost("{id:guid}/exams/{examId:guid}", Name = "AssignStudentToExam")]
        public async Task<IActionResult> AssignStudentToExam(Guid id, Guid examId)
        {
            var result = await studentService.AssignStudentExam(id,examId);

            if (result)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return UnprocessableEntity();
            }
        }

        [Authorize]
        [HttpPut("{id:guid}/exams/{examId:guid}", Name = "CheckExam")]
        public async Task<IActionResult> CheckExam(Guid id, Guid examId)
        {
            var result = await studentService.CheckExam(id, examId);

            if (result)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
            else
            {
                return UnprocessableEntity();
            }
        }

        //send mail
        [HttpGet("{id:guid}/sendmail", Name = "SendMail")]
        public async Task<IActionResult> SendMail(Guid id)
        {
            var studentModelDetails = await studentService.FindById(id);

            if (studentModelDetails == null)
            {
                return NotFound();
            }

            SMTPClient.StudentSendMail(studentModelDetails);
            return Ok();
        }

        [Authorize]
        [HttpPut("{id:guid}", Name = "UpdateStudent")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentModel updateStudentModel, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var studentModel = Mapper.Map<UpdateStudentModel, Student>(updateStudentModel);

            await this.studentService.UpdateAsync(id, studentModel);

            return NoContent();
        }
        
    }
    
}