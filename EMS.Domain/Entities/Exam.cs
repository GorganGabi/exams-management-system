using EMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Domain
{
    public class Exam : Entity, IUpdatable<Exam>
    {
        public string Type { get; private set; }

        public DateTime Date { get; private set; }

        public Course Course { get; private set; } //this is for code-first approach

        public Guid CourseId { get; private set; }

        public string Room { get; private set; }

        public string ImagePath { get; set; }

        public List<Grade> Grades { get; private set; }

        public List<StudentExam> StudentExams { get; private set; }

        public Exam()
        {
            StudentExams = new List<StudentExam>();
        }

        public static Exam Create(string type, DateTime date, Guid courseId, string room) => new Exam
        {
            Type = type,
            Date = date,
            Room = room,
            CourseId = courseId
        };

        public void Update(Exam updatedEntity)
        {
            Type = updatedEntity.Type == null ? Type : updatedEntity.Type;
            Date = updatedEntity.Date == null ? Date : updatedEntity.Date;
            CourseId = updatedEntity.CourseId == null ? CourseId : updatedEntity.CourseId;
            Room = updatedEntity.Room == null ? Room : updatedEntity.Room;
            ImagePath = updatedEntity.ImagePath == null ? ImagePath : updatedEntity.ImagePath;
        }
    }
}
