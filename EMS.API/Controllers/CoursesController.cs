﻿using System.Threading.Tasks;
using EMS.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using AutoMapper;
using EMS.Domain;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace exams_management_system.Controllers
{
    [VersionedRoute("api/courses", 1)]
    [ApiController]
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var Courses = await courseService.GetAll();

            if (Courses.Count == 0)
            {
                return Ok(new List<CourseDetailsModel>());
            }

            return Ok(Courses);
        }

        [Authorize]
        [HttpGet("{id:guid}", Name = "GetCourseById")]
        public async Task<IActionResult> GetCourseById(Guid id)
        {
            var course = await courseService.FindById(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [Authorize]
        [HttpGet("{id:guid}/grades", Name = "GetCourseGrades")]
        public async Task<IActionResult> GetCourseGrades(Guid id)
        {
            var courses = await courseService.GetCourseGrades(id);

            if (courses == null)
            {
                return NotFound();
            }

            return Ok(courses);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CreatingCourseModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            /*var professor = await courseService.GetProfessorCourse(model.ProfessorId);

            if (professor != null)
            {
                return Conflict();
            }*/

            var courseId = await courseService.CreateNew(model);
            return StatusCode(StatusCodes.Status201Created, courseId);

        }

        [Authorize]
        [HttpPut("{id:guid}", Name = "UpdateCourse")]
        public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseModel updateCourseModel, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = await courseService.FindById(id);
            if (course == null)
            {
                return NotFound();
            }

            var courseModel = Mapper.Map<UpdateCourseModel, Course>(updateCourseModel);
            await courseService.Update(id, courseModel);

            return NoContent();
        }

        [Authorize]
        [HttpGet("{courseId:guid}/students/{studentId:guid}", Name = "AssignStudentToCourse")]
        public async Task<IActionResult> AssignStudentToCourse(Guid courseId, Guid studentId)
        {
            var course = await courseService.FindById(courseId);
            if (course == null)
            {
                return NotFound();
            }
            var response = await courseService.AssignStudentToCourse(courseId, studentId);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id:guid}", Name = "DeleteCourse")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var course = await courseService.FindById(id);
            if (course == null)
            {
                return NotFound();
            }

            await courseService.Delete(id);
            return NoContent();
        }
    }
}