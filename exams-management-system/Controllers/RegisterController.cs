﻿using System.Threading.Tasks;
using EMS.Business;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;

namespace exams_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService userService;

        public RegisterController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreatingUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = this.userService.FindByEmail(model.Email);
            if (user.Result == null)
            {
                var userId = await this.userService.CreateNew(model);
                return Ok(userId);
            }

            return StatusCode(422);
        }
    }
}