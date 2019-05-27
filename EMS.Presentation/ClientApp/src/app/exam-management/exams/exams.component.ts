import { Component, OnInit } from '@angular/core';
import { Exam } from '../../exam';
import { ExamService } from '../../exam.service';
import { StudentService } from '../../student.service';

@Component({
  selector: 'app-exams',
  templateUrl: './exams.component.html',
  styleUrls: ['./exams.component.css']
})
export class ExamsComponent implements OnInit {
  exams: Exam[];

  constructor(
    private examService: ExamService,
    private studentService: StudentService) { }

  ngOnInit() {
    this.getExams();   
  }

  getExams() {
    this.examService.getExams()
      .subscribe(exams => {
        this.exams = exams
      });
  }

  getExamsByUserId() {
    this.studentService.getStudentExams(localStorage.getItem('userID'))
      .subscribe(exams => this.exams = exams);
  }

  delete(exam: Exam) {
    this.examService.deleteExam(exam.id).subscribe(() => location.reload());
  }
}
