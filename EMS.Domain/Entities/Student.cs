using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EMS.Domain.Entities
{
    public class Student : Entity, IUpdatable<Student>
    {
        public Guid UserId { get; private set; }

        public string Name { get; private set; }

        public string Email { get; set; }

        public string RegistrationNumber { get; private set; }

        public string FatherInitial { get; private set; }

        public string Group { get; private set; }

        public int Year { get; set; }

        public string Specialty { get; set; }

        public List<StudentCourse> StudentCourses { get; private set; }

        public List<StudentExam> StudentExams { get; private set; }

        public static Student Create(Guid userId) => new Student
        {
            UserId = userId
        };

        public static Student Create (Guid userId, string name, string fInitial, string group, int year, string specialty, string rnumber, string email) => new Student
        {
            UserId = userId,
            Name = name,
            RegistrationNumber = rnumber,
            FatherInitial = fInitial,
            Group = group,  
            Year = year,
            Specialty = specialty,
            Email = email
        };

        public void Update(Student updatedEntity)
        {
            FatherInitial = updatedEntity.FatherInitial == null ? FatherInitial : updatedEntity.FatherInitial;
            Name = updatedEntity.Name == null ? Name : updatedEntity.Name;
            Group = updatedEntity.Group == null ? Group : updatedEntity.Group;
            RegistrationNumber = updatedEntity.RegistrationNumber == null ? RegistrationNumber : updatedEntity.RegistrationNumber;
            Email = updatedEntity.Email == null ? Email : updatedEntity.Email;
        }
    }
}
