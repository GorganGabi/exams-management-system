import {Component, OnInit, Input} from '@angular/core';
import {ActivatedRoute, ParamMap} from '@angular/router';
import {switchMap} from 'rxjs/operators';
import {CourseService} from '../../../services/course.service';
import {Course} from '../../../models/course';
import {Location} from '@angular/common';

@Component({
  selector: 'app-course-detail',
  templateUrl: './course-detail.component.html',
  styleUrls: ['./course-detail.component.css']
})
export class CourseDetailComponent implements OnInit {
  course: Course;
  isEditable = false;
  isMyCourse = false;
  description: string;
  url: string;
  professors: string[];

  constructor(
    private courseService: CourseService,
    private route: ActivatedRoute,
    private location: Location) {
  }

  ngOnInit() {
    this.getCourse();
  }

  getCourse(): void {
    this.route.paramMap.pipe(
      switchMap((map: ParamMap) => this.courseService.getCourse(map.get('id')))
    ).subscribe(course => {
      this.course = course;
      for (let i = 0; i < course.professors.length; i++) {
        if (course.professors[i].id === localStorage.getItem('userID')) {
          this.isMyCourse = true;
          break;
        }
      }
    });
  }

  updateCourse(): void {
    this.courseService.updateCourse(this.course)
      .subscribe(() => this.location.back());
  }

  edit(): void {
    this.isEditable = true;
  }

  saveChanges(): void {
    this.updateCourse();
  }
}
