import {Component, OnInit} from '@angular/core';
import {CourseService} from '../../services/course.service';
import {Course} from '../../models/course';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  courses: Course[];

  constructor(private courseService: CourseService) {
  }

  ngOnInit() {
    this.courseService.getCourses()
      .subscribe(courses => {
        this.courses = courses;
      });
  }

  click(url: string) {
    window.location.href = url;

  }
}
