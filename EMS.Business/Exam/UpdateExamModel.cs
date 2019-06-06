using System;
using System.ComponentModel.DataAnnotations;

namespace EMS.Business
{
    public class UpdateExamModel
    {
        [MaxLength(10)]
        public string Type { get; set; }

        public DateTime Date { get; set; }

        public Guid CourseId { get; set; }     

        public string Room { get; set; }

        public string ImagePath { get; set; }
    }
}
