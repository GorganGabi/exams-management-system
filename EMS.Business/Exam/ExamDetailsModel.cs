using System;
using System.Collections.Generic;

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

        public List<Guid> ProfessorIds { get; set; }
    }
}
