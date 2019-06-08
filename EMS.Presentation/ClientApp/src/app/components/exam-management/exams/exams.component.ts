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
  checkedExams: Exam[];
  role: string;
  professor: Professor;
  areMyExams = false;

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
    this.examService.getCheckedInExams(this.role)
      .subscribe(exams => this.checkedExams = exams);
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
    this.areMyExams = true;
  }

  delete(exam: Exam) {
    this.examService.deleteExam(exam.id).subscribe(() => location.reload());
  }

  formatDate(date: Date): string {
    date = new Date(date);
    const monthNames = [
      'Ianuarie', 'Februarie', 'Martie',
      'Aprilie', 'Mai', 'Iunie', 'Iulie',
      'August', 'Septembrie', 'Octombrie',
      'Noiembrie', 'Decembrie'
    ];
    const day = date.getDate();
    const monthIndex = date.getMonth();
    const year = date.getFullYear();

    return day + ' ' + monthNames[monthIndex] + ' ' + year;
  }

  formatTime(date: Date): string {
    date = new Date(date);
    const hours = date.getHours();
    const minutes = date.getMinutes();

    return (hours < 10 ? '0' + hours : hours) + ':' + ((minutes.toString()) === '0' ? '00' : minutes);
  }

  isChecked(exam: Exam) {
    for (let i = 0; i < this.checkedExams.length; i++) {
      if (this.checkedExams[i].id === exam.id) {
        return true;
      }
    }
    return false;
  }
}
