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
    public class GradeControllerTests
    {
        private readonly CreatingGradeModel createGradeModel;
        private readonly UpdateGradeModel updateGradeModel;
        private readonly Mock<IGradeService> mockRepo;
        private readonly GradesController controller;
        private readonly Grade gradeModel;

        public GradeControllerTests()
        {
            createGradeModel = new CreatingGradeModel();
            updateGradeModel = new UpdateGradeModel();
            mockRepo = new Mock<IGradeService>();
            controller = new GradesController(mockRepo.Object);
        }

        [Fact]
        public async Task Given_GetGrades_When_ModelIsValid_Then_OkStatusCode()
        {
            // Arrange
            mockRepo
            .Setup(g => g.GetAll())
            .ReturnsAsync(new List<GradeDetailsModel>());

            //Act
            var result = await controller.GetGrades();

            //Arrange
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_GetGradeById_When_IdIsValid_Then_OkStatusCode()
        {
            //Arrange
            var guid = new Guid("ef7e98df-26ed-4b21-b874-c3a2815d18ac");
            mockRepo
            .Setup(g => g.FindById(guid))
            .ReturnsAsync(new GradeDetailsModel());

            // Act
            var result = await controller.GetGradeById(guid);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_GetGradeById_When_IdIsValidButNoExamFound_Then_BadStatusCode()
        {
            //Arrange
            mockRepo
            .Setup(g => g.FindById(It.IsIn<Guid>()))
            .ReturnsAsync((GradeDetailsModel)null);

            // Act
            var result = (StatusCodeResult)await controller.GetGradeById(It.IsAny<Guid>());

            // Assert
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Given_UpdateGrade_When_ModelIsValid_Then_OkStatusCode()
        {
            // Arrange
            /*mockRepo
            .Setup(g => g.Update(It.IsAny<Guid>(), It.IsAny<Grade>()))
            .ReturnsAsync(true);
            */

            mockRepo
            .Setup(g => g.FindById(It.IsAny<Guid>()))
            .ReturnsAsync(new GradeDetailsModel());

            //Act
            var result = await controller.UpdateGrade(updateGradeModel, It.IsAny<Guid>());

            //Arrange
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_UpdateGrade_When_ModelIsInvalid_Then_BadStatusCode()
        {
            // Arrange
            /*mockRepo
            .Setup(g => g.Update(It.IsAny<Guid>(), It.IsAny<Grade>()))
            .ReturnsAsync(true);*/

            mockRepo
            .Setup(g => g.FindById(It.IsIn<Guid>()))
            .ReturnsAsync((GradeDetailsModel)null);

            controller.ModelState.AddModelError("id", "1234");

            //Act
            var result = await controller.UpdateGrade(updateGradeModel, It.IsAny<Guid>());

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task Given_UpdateGrade_When_ModelIsValid_Then_NoContentStatusCode()
        {
            // Arrange
            mockRepo.Setup(g => g.FindById(It.IsAny<Guid>())).ReturnsAsync(new GradeDetailsModel());
            //mockRepo.Setup(g => g.Update(It.IsAny<Guid>(), gradeModel)).ReturnsAsync(true);

            //Act
            var result = await controller.UpdateGrade(updateGradeModel, It.IsAny<Guid>());

            //Arrange
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Given_CreateGrade_When_ModelIsValid_Then_OkStatusCode()
        {
            // Arrange
            var Guid = new Guid("ef7e98df-26ed-4b21-b874-c3a2815d18ac");
            var Id = Task.FromResult(Guid);
            mockRepo.Setup(g => g.CreateNew(createGradeModel)).Returns(Id);

            // Act
            var Result = (OkObjectResult)await controller.CreateGrade(createGradeModel);

            // Assert
            Assert.IsType<OkObjectResult>(Result);
            Assert.Equal(Id.Result, Result.Value);
        }
        [Fact]
        public async Task Given_CreateGrade_When_ModelIsInvalid_Then_BadStatusCode()
        {
            // Arrange
            var guid = new Guid("ef7e98df-26ed-4b21-b874-c3a2815d18ac");
            var id = Task.FromResult(guid);
            mockRepo.Setup(g => g.CreateNew(createGradeModel)).Returns(id);

            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.CreateGrade(createGradeModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        /*[Fact]
        public async Task Given_UpdateGrade_When_IdIsValid_Then_Status409Conflict()
        {
            //Arrange
            mockRepo
                .Setup(g => g.FindById(It.IsAny<Guid>()))
                .ReturnsAsync(new GradeDetailsModel());

            mockRepo
                .Setup(g => g.Update(It.IsAny<Guid>(), gradeModel))
                .ReturnsAsync(false);

            //Act
            var result = (ObjectResult)await controller.Update(It.IsAny<Guid>(), gradeModel);

            //Assert
            Assert.Equal(StatusCodes.Status409Conflict, result.StatusCode);
        }*/

        [Fact]
        public async Task Given_DeleteGrade_When_IdIsValid_Then_OkStatusCode()
        {
            //Arrange
            /*mockRepo
            .Setup(g => g.Delete(It.IsAny<Guid>()))
            .ReturnsAsync(true);*/

            mockRepo
            .Setup(g => g.FindById(It.IsAny<Guid>()))
            .ReturnsAsync(new GradeDetailsModel());

            //Act
            var result = await controller.DeleteGrade(It.IsAny<Guid>());

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_DeleteGrade_When_IdNotFound_Then_Status404NotFount()
        {
            //Arrange
            mockRepo
            .Setup(g => g.FindById(It.IsAny<Guid>()))
            .ReturnsAsync((GradeDetailsModel)null);

            //Act
            var result = (StatusCodeResult)await controller.DeleteGrade(It.IsAny<Guid>());

            //Assert
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Given_DeleteGrade_When_IdIsValid_Then_Status409Conflict()
        {
            //Arrange
            mockRepo
            .Setup(g => g.FindById(It.IsAny<Guid>()))
            .ReturnsAsync(new GradeDetailsModel());

            /*mockRepo
            .Setup(g => g.Delete(It.IsAny<Guid>()))
            .ReturnsAsync(false);*/

            //Act
            var result = (ObjectResult)await controller.DeleteGrade(It.IsAny<Guid>());

            //Assert
            Assert.Equal(StatusCodes.Status409Conflict, result.StatusCode);
        }
    }
}