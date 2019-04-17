using System.Threading.Tasks;
using EMS.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using AutoMapper;
using EMS.Domain.Entities;
using System.Collections.Generic;

namespace exams_management_system.Controllers
{
    [VersionedRoute("api/professors", 1)]
    [ApiController]
    public class ProfessorController : Controller
    {
        private readonly IProfessorService professorService;

        public ProfessorController(IProfessorService professorService)
        {
            this.professorService = professorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfessors()
        {
            var Professors = await professorService.GetAll();

            if (Professors.Count == 0)
            {
                return Ok(new List<ProfessorDetailsModel>());
            }

            return Ok(Professors);
        }

        [HttpGet("{id:guid}", Name = "GetProfessorById")]
        public async Task<IActionResult> GetProfessorById(Guid id)
        {
            var professor = await professorService.FindById(id);

            if (professor == null)
            {
                return NotFound();
            }

            return Ok(professor);
        }

        [HttpPut("{id:guid}", Name = "UpdateProfessor")]
        public async Task<IActionResult> UpdateProfessor([FromBody] UpdateProfessorModel createProfessorModel, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var professor = await professorService.FindById(id);
            if (professor == null)
            {
                return NotFound();
            }

            var professorModel = Mapper.Map<UpdateProfessorModel, Professor>(createProfessorModel);

            await professorService.UpdateAsync(id, professorModel);

            return NoContent();
        }
    }
}