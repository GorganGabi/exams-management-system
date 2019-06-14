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
using Newtonsoft.Json.Linq;

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
        private readonly ICourseService courseService;

        public AccountController(IStudentService studentService, IProfessorService professorService, ICourseService courseService)
        {
            this.studentService = studentService;
            this.professorService = professorService;
            this.courseService = courseService;
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
                if (result.ResultModel.Role == "Student")
                {
                    var response = await client.GetAsync($"http://localhost:3000?type=student&&email={result.ResultModel.Email}");
                    var responseString = await response.Content.ReadAsStringAsync();
                    entityId = await studentService.CreateNew(Guid.Parse(result.ResultModel.Id), responseString);
                    await assignStudentToCourse(responseString);
                }
                else
                {
                    var response = await client.GetAsync($"http://localhost:3000?type=professor&&email={result.ResultModel.Email}");
                    var responseString = await response.Content.ReadAsStringAsync();
                    entityId = await professorService.CreateNew(Guid.Parse(result.ResultModel.Id), responseString);
                    await createCourse(responseString);
                    
                }
                return StatusCode(StatusCodes.Status201Created, entityId);
            }
            else if (result.StatusCode == StatusCodes.Status422UnprocessableEntity)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity);
            }

            return BadRequest();
        }

        private async Task assignStudentToCourse(string _responseString)
        {
            //todo: refactor the code
            JObject json = JObject.Parse(_responseString);
            var coursesString = json["response"]["Courses"].ToString();
            var courses = coursesString.Split(", ");
            foreach (var course in courses)
            {
                var response = await client.GetAsync($"http://localhost:3000?type=course&&email={course}");
                var responseString = await response.Content.ReadAsStringAsync();

                json = JObject.Parse(responseString);
                var title = json["response"]["Title"].ToString();
                var courseDetailsModel = await courseService.FindByTitle(title);

                await studentService.AssignStudentCourse(entityId, courseDetailsModel.Id);
            }
        }

        private async Task createCourse(string _responseString)
        {
            //todo: refactor the code
            JObject json = JObject.Parse(_responseString);
            var coursesString = json["response"]["Courses"].ToString();
            var courses = coursesString.Split(", ");
            foreach (var course in courses)
            {
                var response = await client.GetAsync($"http://localhost:3000?type=course&&email={course}");
                var responseString = await response.Content.ReadAsStringAsync();

                json = JObject.Parse(responseString);
                var title = json["response"]["Title"].ToString();
                var universityYear = json["response"]["UniversityYear"].ToString();
                var studentYear = int.Parse(json["response"]["StudentYear"].ToString());
                var semester = int.Parse(json["response"]["Semester"].ToString());
                var description = json["response"]["Description"].ToString();
                var url = json["response"]["URL"].ToString();

                await courseService.CreateNew(new CreatingCourseModel
                {
                    ProfessorId = entityId,
                    Semester = semester,
                    StudentYear = studentYear,
                    Title = title,
                    UniversityYear = universityYear,
                    Description = description,
                    Url = url
                });
            }
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