using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EMS.Business;
using exams_management_system;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EMS.API.Controllers
{
    [VersionedRoute("api/[controller]/[action]", 1)]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly IStudentService studentService;
        private readonly IProfessorService professorService;

        public AccountController(IStudentService studentService, IProfessorService professorService)
        {
            this.studentService = studentService;
            this.professorService = professorService;
        }

        [HttpGet]
        public async Task<ServiceContract> SendHttpRequest(string link, string json)
        {
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            //trateaza exceptia la post; cazul in care nu api-ul e down; global exception handler
            var response = await client.PostAsync(link, httpContent);
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ServiceContract>(responseString);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            string json = JsonConvert.SerializeObject(model);

            var result = await SendHttpRequest("http://localhost:8080/api/account/register", json);

            if (result.StatusCode == StatusCodes.Status201Created)
            {
                if (result.ResultModel.Role == "Student")
                {
                    await studentService.CreateNew(Guid.Parse(result.ResultModel.Id));
                }
                else
                {
                    await professorService.CreateNew(Guid.Parse(result.ResultModel.Id));
                }
                return StatusCode(StatusCodes.Status201Created, result);
            }
            else if (result.StatusCode == StatusCodes.Status422UnprocessableEntity)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, result);
            }

            return BadRequest();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            string json = JsonConvert.SerializeObject(model);

            var result = await SendHttpRequest("http://localhost:8080/api/account/login", json);

            //acceaseaza status code direct din result
            if (result.StatusCode == StatusCodes.Status200OK)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else if (result.StatusCode == StatusCodes.Status422UnprocessableEntity)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, result);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            string json = JsonConvert.SerializeObject(model);

            var result = await SendHttpRequest("http://localhost:8080/api/account/resetpassword", json);

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            string json = JsonConvert.SerializeObject(model);

            var result = await SendHttpRequest("http://localhost:8080/api/account/forgotpassword", json);

            return Ok(result);
        }

    }
}