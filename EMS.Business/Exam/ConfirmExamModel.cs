using System;

namespace EMS.Business
{
    public class ConfirmExamModel
    {
        public Guid StudentId { get; set; }

        public Guid ExamId { get; set; }

        public bool isConfirmed { get; set; }
    }
}
