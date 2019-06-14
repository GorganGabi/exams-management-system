using EMS.Domain.Entities;
using System;
using System.Collections.Generic;

namespace EMS.Domain
{
    public class Course : Entity, IUpdatable<Course>
    {
        public string Title { get; private set; }

        public List<ProfessorCourse> ProfessorCourses { get; private set; }

        public List<StudentCourse> StudentCourses { get; private set; }

        public List<Exam> Exams { get; private set; }

        public string UniversityYear { get; private set; }

        public int StudentYear { get; private set; }

        public int Semester { get; private set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public Course()
        {
            StudentCourses = new List<StudentCourse>();
        }

        public static Course Create(string title, string universityYear, int studentYear, int semester, string description, string url) => new Course
        {
            Title = title,
            UniversityYear = universityYear,
            StudentYear = studentYear,
            Semester = semester,
            Description = description,
            Url = url,
            ProfessorCourses = new List<ProfessorCourse>()
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
