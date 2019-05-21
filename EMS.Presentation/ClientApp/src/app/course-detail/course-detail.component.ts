import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { CourseService } from '../course.service';
import { Course } from '../course';
import { Location } from '@angular/common';

@Component({
  selector: 'app-course-detail',
  templateUrl: './course-detail.component.html',
  styleUrls: ['./course-detail.component.css']
})
export class CourseDetailComponent implements OnInit {
  course: Course;
  isEditable: boolean = false;

  constructor(
    private courseService: CourseService,
    private route: ActivatedRoute,
    private location: Location) { }

  ngOnInit() {
    this.getCourse();
  }

  goBack() {
    this.location.back();
  }
  
  getCourse(): void {
    this.route.paramMap.pipe(
      switchMap((map: ParamMap) => this.courseService.getCourse(map.get('id')))
    ).subscribe(course => this.course = course);
  }

  updateCourse(): void {
    this.courseService.updateCourse(this.course)
      .subscribe(() => this.goBack());
  }

  edit(): void {
    this.isEditable = true;
  }

  saveChanges(): void {
    this.updateCourse();
  }
}
