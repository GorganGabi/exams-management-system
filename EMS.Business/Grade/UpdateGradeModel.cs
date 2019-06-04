using System;
using System.ComponentModel.DataAnnotations;

namespace EMS.Business
{
    public class UpdateGradeModel
    {
        [Required]
        public float Value { get; set; }

        public Guid ExamId { get; set; }

        public Guid StudentId { get; set; }

        public string StudentName { get; set; }

        public Boolean IsConfirmed { get; set; }
    }
}
