using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Domain.Entities
{
    public class ProfessorCourse
    {
        public Guid ProfessorId { get; set; }

        public Professor Professor { get; set; }

        public Guid CourseId { get; set; }

        public Course Course { get; set; }
    }
}
