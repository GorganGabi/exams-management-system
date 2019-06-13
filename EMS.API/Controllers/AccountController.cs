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
        private Guid entityId = new Guid();
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
                var response = await client.GetAsync($"http://localhost:3000?email={result.ResultModel.Email}");
                var responseString = await response.Content.ReadAsStringAsync();

                if (result.ResultModel.Role == "Student")
                {
                    entityId = await studentService.CreateNew(Guid.Parse(result.ResultModel.Id), responseString);
                }
                else
                {
                    entityId = await professorService.CreateNew(Guid.Parse(result.ResultModel.Id));
                }
                return StatusCode(StatusCodes.Status201Created, entityId);
            }
            else if (result.StatusCode == StatusCodes.Status422UnprocessableEntity)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity);
            }

            return BadRequest();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            string json = JsonConvert.SerializeObject(model);

            var result = await SendHttpRequest("http://localhost:8080/api/account/login", json);

            if (result.StatusCode == StatusCodes.Status200OK)
            {
                //todo: refactor the code
                if (result.ResultModel.Role == "Student")
                {
                    var student = await studentService.FindByUserId(Guid.Parse(result.ResultModel.Id));
                    entityId = student.Id;
                }
                else
                {
                    var professor = await professorService.FindByUserId(Guid.Parse(result.ResultModel.Id));
                    entityId = professor.Id;
                }
                return StatusCode(StatusCodes.Status200OK, new UserDetailsModel
                {
                    Id = entityId.ToString(),
                    Token = result.ResultModel.Token
                });
            }
            else if (result.StatusCode == StatusCodes.Status422UnprocessableEntity)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity);
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