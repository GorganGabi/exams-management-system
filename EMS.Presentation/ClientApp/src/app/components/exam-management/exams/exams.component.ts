import {Component, OnInit} from '@angular/core';
import {Exam} from '../../../models/exam';
import {ExamService} from '../../../services/exam.service';
import {StudentService} from '../../../services/student.service';
import {ProfessorService} from 'src/app/services/professor.service';
import {Professor} from 'src/app/models/professor';

@Component({
  selector: 'app-exams',
  templateUrl: './exams.component.html',
  styleUrls: ['./exams.component.css']
})
export class ExamsComponent implements OnInit {
  exams: Exam[];
  role: string;
  professor: Professor;

  constructor(
    private examService: ExamService,
    private studentService: StudentService,
    private professorService: ProfessorService) {
  }

  ngOnInit() {
    this.getExams();
    this.role = localStorage.getItem('userID');
    if (this.role) {
      this.professorService.getProfessorById(this.role)
        .subscribe(professor => {
          this.professor = professor;
        });
    }
  }

  getExams() {
    this.examService.getExams()
      .subscribe(exams => {
        this.exams = exams;
      });
  }

  getExamsByUserId() {
    if (this.professor) {
      this.professorService.getProfessorExams(localStorage.getItem('userID'))
        .subscribe(exams => {
          this.exams = exams;
        });
    } else {
      this.studentService.getStudentExams(localStorage.getItem('userID'))
        .subscribe(exams => {
          this.exams = exams;
        });
    }
  }

  delete(exam: Exam) {
    this.examService.deleteExam(exam.id).subscribe(() => location.reload());
  }
}
