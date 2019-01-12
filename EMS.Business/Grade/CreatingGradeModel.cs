﻿using System;
using System.ComponentModel.DataAnnotations;

namespace EMS.Business
{
    public class CreatingGradeModel
    {

        [Required]
        public float Nota { get; set; }

        public Guid ExamId { get; set; }

        public Guid StudentId { get; set; }
    }
}
