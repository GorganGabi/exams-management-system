using EMS.Business;
using EMS.Domain;
using Xunit;
using Moq;
using exams_management_system.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;

namespace EMS.API.Tests
{
    [Collection("EMS Collection")]
    public class ExamControllerTests
    {
        private readonly CreatingExamModel createExamModel;
        private readonly UpdateExamModel updateExamModel;
        private readonly Mock<IExamService> mockRepo;
        private readonly Mock<IGradeService> mockRepoGrade;
        private readonly ExamsController controller;
        private readonly Exam examModel;

        public ExamControllerTests()
        {
            createExamModel = new CreatingExamModel();
            updateExamModel = new UpdateExamModel();
            mockRepo = new Mock<IExamService>();
            mockRepoGrade = new Mock<IGradeService>();
            //controller = new ExamsController(mockRepo.Object,mockRepoGrade.Object);
        }

        [Fact]
        public async Task Given_GetExams_When_ModelIsValid_Then_OkStatusCode()
        {
            // Arrange
            mockRepo
            .Setup(e => e.GetAll())
            .ReturnsAsync(new List<ExamDetailsModel>());

            //Act
            var result = await controller.GetExams();

            //Arrange
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_CreateExam_When_ModelIsValid_Then_OkStatusCode()
        {
            // Arrange
            var examGuid = new Guid("ef7e98df-26ed-4b21-b874-c3a2815d18ac");
            var Id = Task.FromResult(examGuid);
            mockRepo
            .Setup(e => e.CreateNew(createExamModel))
            .Returns(Id);

            // Act
            var Result = (OkObjectResult)await controller.CreateExam(createExamModel);

            // Assert
            Assert.IsType<OkObjectResult>(Result);
            Assert.Equal(Id.Result, Result.Value);
        }

        [Fact]
        public async Task Given_CreateExam_When_ModelIsInvalid_Then_BadStatusCode()
        {
            // Arrange
            var guid = new Guid("ef7e98df-26ed-4b21-b874-c3a2815d18ac");
            mockRepo
            .Setup(e => e.CreateNew(createExamModel))
            .ReturnsAsync(guid);

            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.CreateExam(createExamModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task Given_GetExamById_When_IdIsValid_Then_OkStatusCode()
        {
            //Arrange
            var guid = new Guid("ef7e98df-26ed-4b21-b874-c3a2815d18ac");
            mockRepo
            .Setup(e => e.FindById(guid))
            .ReturnsAsync(new ExamDetailsModel());

            // Act
            var result = await controller.GetExamById(guid);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_GetGradeByExamId_When_IdIsValid_Then_OkStatusCode()
        {
            //Arrange
            var guid = new Guid("ef7e98df-26ed-4b21-b874-c3a2815d18ac");
            //mockRepoGrade
            //    .Setup(e => e.FindByExamId(guid))
            //    .ReturnsAsync(new GradeDetailsModel());

            // Act
            var result = await controller.GetGradeByExamId(guid);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_GetGradeByExamId_When_IdIsValidButNoGradeFound_Then_BadStatusCode()
        {
            //mockRepoGrade
            //    .Setup(e => e.FindByExamId(It.IsIn<Guid>()))
            //    .ReturnsAsync((GradeDetailsModel)null);

            // Act
            var result = (StatusCodeResult)await controller.GetGradeByExamId(It.IsAny<Guid>());

            // Assert
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Given_GetExamById_When_IdIsValidButNoExamFound_Then_BadStatusCode()
        {
            mockRepo
            .Setup(e => e.FindById(It.IsIn<Guid>()))
            .ReturnsAsync((ExamDetailsModel)null);

            // Act
            var result = (StatusCodeResult)await controller.GetExamById(It.IsAny<Guid>());

            // Assert
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Given_UpdateExam_When_ModelIsValid_Then_OkStatusCode()
        {
            // Arrange
            /*mockRepo
            .Setup(e => e.Update(It.IsAny<Guid>(), It.IsAny<Exam>()))
            .ReturnsAsync(true);
            */

            mockRepo
            .Setup(u => u.FindById(It.IsAny<Guid>()))
            .ReturnsAsync(new ExamDetailsModel());

            //Act
            var result = await controller.UpdateExam(updateExamModel, It.IsAny<Guid>());

            //Arrange
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_UpdateExam_When_ModelIsInvalid_Then_BadStatusCode()
        {
            // Arrange
            /*mockRepo
            .Setup(e => e.Update(It.IsAny<Guid>(), It.IsAny<Exam>()))
            .ReturnsAsync(true);*/

            mockRepo
            .Setup(u => u.FindById(It.IsIn<Guid>()))
            .ReturnsAsync((ExamDetailsModel)null);

            controller.ModelState.AddModelError("id", "1234");
            
            //Act
            var result = await controller.UpdateExam(updateExamModel, It.IsAny<Guid>());

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task Given_DeleteExam_When_IdIsValid_Then_OkStatusCode()
        {
            //Arrange
            /*mockRepo
            .Setup(e => e.Delete(It.IsAny<Guid>()))
            .ReturnsAsync(true);
            */
            mockRepo
            .Setup(u => u.FindById(It.IsAny<Guid>()))
            .ReturnsAsync(new ExamDetailsModel());

            //Act
            var result = await controller.DeleteExam(It.IsAny<Guid>());

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_DeleteExam_When_IdNotFound_Then_Status404NotFount()
        {
            //Arrange
            mockRepo
            .Setup(u => u.FindById(It.IsAny<Guid>()))
            .ReturnsAsync((ExamDetailsModel)null);

            //Act
            var result = (StatusCodeResult)await controller.DeleteExam(It.IsAny<Guid>());

            //Assert
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Given_DeleteExam_When_IdIsValid_Then_Status409Conflict()
        {
            //Arrange
            mockRepo
            .Setup(u => u.FindById(It.IsAny<Guid>()))
            .ReturnsAsync(new ExamDetailsModel());

            /*mockRepo
            .Setup(e => e.Delete(It.IsAny<Guid>()))
            .ReturnsAsync(false);
            */
            //Act
            var result = (ObjectResult)await controller.DeleteExam(It.IsAny<Guid>());

            //Assert
            Assert.Equal(StatusCodes.Status409Conflict, result.StatusCode);
        }
 }
}