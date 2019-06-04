import {Component, OnInit} from '@angular/core';
import {Grade} from '../../../models/grade';
import {GradeService} from '../../../services/grade.service';

@Component({
  selector: 'app-grades',
  templateUrl: './grades.component.html',
  styleUrls: ['./grades.component.css']
})
export class GradesComponent implements OnInit {
  grades: Grade[];

  constructor(private gradeService: GradeService) {
  }

  ngOnInit() {
    this.getGrades();
  }

  getGrades() {
    this.gradeService.getGrades()
      .subscribe(grades => this.grades = grades);
  }

}
