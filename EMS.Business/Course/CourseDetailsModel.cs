﻿using EMS.Domain;
using EMS.Domain.Entities;
using System;
using System.Collections.Generic;

namespace EMS.Business
{
    public class CourseDetailsModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public ProfessorDetailsModel Professor { get; set; }

        public List<ExamDetailsModel> Exams { get; set; }

        public string UniversityYear { get; set; }

        public int StudentYear { get; set; }

        public int Semester { get; set; }
    }
}
