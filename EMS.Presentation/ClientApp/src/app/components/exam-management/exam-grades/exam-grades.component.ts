import {Component, OnInit} from '@angular/core';
import {Grade} from '../../../models/grade';
import {ExamService} from '../../../services/exam.service';
import {ActivatedRoute} from '@angular/router';
import {GradeService} from "../../../services/grade.service";

@Component({
  selector: 'app-exam-grades',
  templateUrl: './exam-grades.component.html',
  styleUrls: ['./exam-grades.component.css']
})
export class ExamGradesComponent implements OnInit {
  grades: Grade[];
  examId: string;

  constructor(private examService: ExamService,
              private gradeService: GradeService,
              private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.examId = this.route.snapshot.paramMap.get('id');
    this.getGrades();

  }

  getGrades() {
    this.examService.getExamGrades(this.examId)
      .subscribe(grades => {
        this.grades = grades;
      });
  }

  deleteGrade(grade: Grade) {
    this.gradeService.deleteGrade(grade)
      .subscribe(() => location.reload());
  }
}
