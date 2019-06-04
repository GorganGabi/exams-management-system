import {Component, OnInit} from '@angular/core';
import {Grade} from '../../../models/grade';
import {CourseService} from '../../../services/course.service';
import {switchMap} from 'rxjs/operators';
import {ActivatedRoute, ParamMap} from '@angular/router';


@Component({
  selector: 'app-course-grades',
  templateUrl: './course-grades.component.html',
  styleUrls: ['./course-grades.component.css']
})
export class CourseGradesComponent implements OnInit {
  grades: Grade[];

  constructor(private courseService: CourseService,
              private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.getCourseGrades();
  }

  getCourseGrades() {
    this.route.paramMap.pipe(
      switchMap((map: ParamMap) => this.courseService.getCourseGrades(map.get('id')))
    ).subscribe(grades => this.grades = grades);
  }
}
