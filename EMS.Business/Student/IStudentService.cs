﻿using EMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.Business
{
    public interface IStudentService
    {
        Task<List<StudentDetailsModel>> GetAll();

        Task<StudentDetailsModel> FindById(Guid id);

        Task<Guid> CreateNew(Guid userId, string json);

        Task<bool> UpdateAsync(Guid id, Student studentUpdated);

        Task<bool> CheckExam(Guid id, Guid examId);
    }
}
