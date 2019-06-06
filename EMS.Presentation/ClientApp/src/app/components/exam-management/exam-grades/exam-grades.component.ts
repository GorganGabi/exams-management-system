import {Component, OnInit} from '@angular/core';
import {Grade} from '../../../models/grade';
import {ExamService} from '../../../services/exam.service';
import {ActivatedRoute} from '@angular/router';
import {GradeService} from '../../../services/grade.service';
import {Professor} from '../../../models/professor';
import {ProfessorService} from "../../../services/professor.service";

@Component({
  selector: 'app-exam-grades',
  templateUrl: './exam-grades.component.html',
  styleUrls: ['./exam-grades.component.css']
})
export class ExamGradesComponent implements OnInit {
  grades: Grade[];
  examId: string;
  role: string;
  professor: Professor;

  constructor(private examService: ExamService,
              private gradeService: GradeService,
              private route: ActivatedRoute,
              private professorService: ProfessorService) {
  }

  ngOnInit() {
    this.examId = this.route.snapshot.paramMap.get('id');
    this.getGrades();
    this.role = localStorage.getItem('userID');
    if (this.role) {
      this.professorService.getProfessorById(this.role)
        .subscribe(professor => {
          this.professor = professor;
        });
    }
  }

  getGrades() {
    if (this.examId) {
      this.examService.getExamGrades(this.examId)
        .subscribe(grades => {
          this.grades = grades;
        });
    } else {
      this.gradeService.getGrades()
        .subscribe(grades => {
          const id = localStorage.getItem('userID');
          this.grades = grades.filter((grade) => {
            return grade.studentId === id;
          });
        });
    }
  }

  deleteGrade(grade: Grade) {
    this.gradeService.deleteGrade(grade)
      .subscribe(() => location.reload());
  }

  getStatus(isConfirmed: boolean): string {
    if (isConfirmed) {
      return 'Confirmată';
    }
    return 'Neconfirmată';
  }

  confirmGrade(grade: Grade) {
    grade.isConfirmed = true;
    this.gradeService.updateGrade(grade)
      .subscribe();
  }
}
