﻿using EMS.Domain.Entities;
using System.Collections.Generic;

namespace EMS.Domain
{
    public class Course : Entity, IUpdatable<Course>
    {
        public string Title { get; set; }

        public List<CourseProfessor> CourseProfessors { get; set; }

        public List<Exam> Exams { get; set; }

        public string UniversityYear { get; set; }

        public int StudentYear { get; set; }

        public int Semester { get; set; }

        public static Course Create(string title, string universityYear, int studentYear, int semester) => new Course
        {
            Title = title,
            UniversityYear = universityYear,
            StudentYear = studentYear,
            Semester = semester
        };

        public void Update(Course updatedEntity)
        {
            Title = updatedEntity.Title == null ? Title : updatedEntity.Title;
            UniversityYear = updatedEntity.UniversityYear == null ? UniversityYear : updatedEntity.UniversityYear;
            StudentYear = updatedEntity.StudentYear == 0 ? StudentYear : updatedEntity.StudentYear;
            Semester = updatedEntity.Semester == 0 ? Semester : updatedEntity.Semester;
        }
    }
}
