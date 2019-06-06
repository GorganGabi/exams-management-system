using System;

namespace EMS.Business
{
    public class ExamDetailsModel
    {
        public string Type { get; set; }

        public CourseDetailsModel Course { get; set; }

        public DateTime Date { get; set; }
      
        public string Room { get; set; }

        public Guid Id { get; internal set; }

        public string imagePath { get; set; }

        public Guid ProfessorId { get; set; }
    }
}
