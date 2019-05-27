import { Component, OnInit } from '@angular/core';
import { Course } from 'src/app/course';
import { CourseService } from 'src/app/course.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-course',
  templateUrl: './add-course.component.html',
  styleUrls: ['./add-course.component.css']
})
export class AddCourseComponent implements OnInit {
  course: Course;
  title: string;
  universityYear: string;
  studentYear: number;
  semester: number;

  constructor(
    private courseService: CourseService,
    private route: Router) { }  

  ngOnInit() {
  }

  createCourse() {
    this.course = new Course();
    this.course.title = this.title;
    this.course.universityYear = this.universityYear;
    this.course.studentYear = this.studentYear;
    this.course.semester = this.semester;
    console.log(this.course);
    this.courseService.createCourse(this.course)
      .subscribe(() =>{
        this.route.navigate(['/courses'])
      } );
  }

}
