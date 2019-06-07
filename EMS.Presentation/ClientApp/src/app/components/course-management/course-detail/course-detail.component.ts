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
    ).subscribe(course => this.course = course);
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
