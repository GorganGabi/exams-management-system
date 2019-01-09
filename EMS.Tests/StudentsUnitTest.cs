﻿using AutoMapper;
using EMS.Business;
using EMS.Domain;
using EMS.Domain.Entities;
using EMS.Tests;
using exams_management_system.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EMS.Tests
{
    [Collection("EMS Collection")]
    public class StudentsUnitTest
    {
        private readonly UpdateStudentModel updateStudentModel;
        private readonly Mock<IStudentService> mockRepo;
        private readonly StudentsController controller;
        private readonly Student studentModel;

        public StudentsUnitTest()
        {
            updateStudentModel = new UpdateStudentModel();
            mockRepo = new Mock<IStudentService>();
            controller = new StudentsController(mockRepo.Object);


            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UpdateStudentModel, Student>();
            });


            studentModel = Mapper.Map<UpdateStudentModel, Student>(updateStudentModel);
        }

        [Fact]
        public async Task Given_GetStudents_When_ThereAreNoStudents_Then_OkStatusCode()
        {
            mockRepo.Setup(p => p.GetAll()).ReturnsAsync(new List<StudentDetailsModel>());

            // Act
            var result = await controller.GetStudents();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task Given_GetStudents_When_ThereAreStudents_Then_OkStatusCode()
        {
            var students = new List<StudentDetailsModel>();
            students.Add(new StudentDetailsModel());
            mockRepo.Setup(p => p.GetAll()).ReturnsAsync(students);

            // Act
            var result = await controller.GetStudents();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_GetStudentById_When_IdIsValid_Then_OkStatusCode()
        {
            var guid = new Guid("0f959f87-14f4-42d2-bf70-ba84af24ab51");
            mockRepo.Setup(p => p.FindById(guid)).Returns(Task.FromResult(new StudentDetailsModel()));

            // Act
            var result = await controller.GetStudentById(guid);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public async Task Given_GetStudentById_When_IdIsValidButNoCourseFound_Then_BadStatusCode()
        {
            mockRepo.Setup(p => p.FindById(It.IsIn<Guid>())).Returns(Task.FromResult<StudentDetailsModel>(null));

            var controller = new StudentsController(mockRepo.Object);

            // Act
            var result = (StatusCodeResult)await controller.GetStudentById(It.IsAny<Guid>());

            // Assert
            Assert.Equal(422, result.StatusCode);
        }

        [Fact]
        public async Task Given_UpdateStudent_When_ModelIsValid_Then_OkStatusCode()
        {
            // Arrange
            mockRepo.Setup(u => u.UpdateAsync(It.IsAny<Guid>(), It.IsAny<Student>())).
                Returns(Task.FromResult(true));

            //Act
            var result = await controller.UpdateStudent(updateStudentModel, It.IsAny<Guid>());

            //Arrange
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_UpdateStudent_When_ModelIsValid_Then_NoContentStatusCode()
        {
            // Arrange
            mockRepo.Setup(u => u.UpdateAsync(It.IsAny<Guid>(), studentModel)).Returns(Task.FromResult(true));

            //Act
            var result = await controller.UpdateStudent(updateStudentModel, It.IsAny<Guid>());

            //Arrange
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Given_UpdateStudent_When_ModelIsInValid_Then_BadModel()
        {
            // Arrange
            mockRepo.Setup(u => u.UpdateAsync(It.IsAny<Guid>(), studentModel)).Returns(Task.FromResult(true));
            controller.ModelState.AddModelError("password", "Required");

            //Act
            var result = await controller.UpdateStudent(updateStudentModel, It.IsAny<Guid>());

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

    }
}