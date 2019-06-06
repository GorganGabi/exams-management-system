using EMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Domain
{
    public class Grade : Entity, IUpdatable<Grade>
    {
        public float Value { get; private set; }

        public Exam Exam { get; private set; } 

        public Guid ExamId { get; private set; }

        public Student Student { get; private set; } 

        public Guid StudentId { get; private set; }

        public Boolean IsConfirmed { get; private set; }

        public static Grade Create(float value, Guid examId, Guid studentId) => new Grade
        {
            Value = value,
            ExamId = examId,
            StudentId = studentId,
            IsConfirmed = false
        };

        public void Update(Grade updatedEntity)
        {
            Value = updatedEntity.Value == null ? Value : updatedEntity.Value;
            ExamId = updatedEntity.ExamId == Guid.Empty ? ExamId : updatedEntity.ExamId;
            StudentId = updatedEntity.StudentId == Guid.Empty ? StudentId : updatedEntity.StudentId;
            IsConfirmed = updatedEntity.IsConfirmed == null ? IsConfirmed : updatedEntity.IsConfirmed;
        }
    }
}
