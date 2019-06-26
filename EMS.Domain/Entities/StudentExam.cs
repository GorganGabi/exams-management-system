using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Domain.Entities
{
    public class StudentExam : Entity, IUpdatable<StudentExam>
    {
        public Guid StudentId { get; private set; }

        public Student Student { get; private set; }

        public string Checked{ get; set; }

        public Guid ExamId { get; private set; }

        public Exam Exam { get; private set; }


        public StudentExam()
        {

        }

        public StudentExam(Student student, Exam exam)
        {
            Id = Guid.NewGuid();
            StudentId = student.Id;
            Student = student;
            ExamId = exam.Id;
            Exam  = exam;
            Checked = "no";
        }

        public void Update(StudentExam updatedEntity)
        {
            StudentId = updatedEntity.StudentId == null ? StudentId : updatedEntity.StudentId;
            ExamId = updatedEntity.ExamId == null ? ExamId : updatedEntity.ExamId;
            Checked = updatedEntity.Checked == null ? Checked : updatedEntity.Checked;
        }

        public void CheckIn()
        {
            this.Checked = "yes";
        }
    }


}
