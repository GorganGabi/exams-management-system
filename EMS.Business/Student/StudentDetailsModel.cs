﻿using EMS.Domain;
using System;
using System.Collections.Generic;

namespace EMS.Business
{
    public class StudentDetailsModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Group { get; set; }

        public string RegistrationNumber { get; set; }

        public string FatherInitial { get; set; }

        public List<CourseDetailsModel> Courses { get; set; }

        public List<ExamDetailsModel> Exams { get; set; }
    }
}
