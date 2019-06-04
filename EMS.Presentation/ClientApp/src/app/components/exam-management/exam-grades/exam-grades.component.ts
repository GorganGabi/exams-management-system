import {Component, OnInit} from '@angular/core';
import {Grade} from '../../../models/grade';
import {ExamService} from '../../../services/exam.service';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-exam-grades',
  templateUrl: './exam-grades.component.html',
  styleUrls: ['./exam-grades.component.css']
})
export class ExamGradesComponent implements OnInit {
  grades: Grade[];
  examId: string;

  constructor(private examService: ExamService,
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
        console.log(this.grades);
      });
  }
}
