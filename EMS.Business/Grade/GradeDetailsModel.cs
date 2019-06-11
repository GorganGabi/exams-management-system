using System;

namespace EMS.Business
{
    public class GradeDetailsModel
    {
        public float Value { get; set; }

        public string ExamName { get; set; }

        public Guid ExamId { get; set; }

        public StudentDetailsModel Student { get; set; }

        public Guid Id { get; internal set; }

        public bool IsConfirmed { get; set; }
    }
}