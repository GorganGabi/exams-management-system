import { Component, OnInit } from '@angular/core';
import { Course } from '../course';
import { CourseService } from '../course.service';
import { StudentService } from '../student.service';
import { ProfessorService } from '../professor.service';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent implements OnInit {
  courses: Course[];

  constructor(
    private courseService: CourseService,
    private studentService: StudentService,
    private profesorService: ProfessorService) { }

  ngOnInit() {
    this.getCourses();
  }

  getCourses(): void {
    this.courseService.getCourses()
      .subscribe(courses => { this.courses = courses }, error => console.log(error));
  }

  getCoursesByUserId(): void {
    this.studentService.getStudentCourses(localStorage.getItem("userID"))
      .subscribe(courses => this.courses = courses);
    this.profesorService.getProfessorCourses(localStorage.getItem("userID"))
      .subscribe(courses => this.courses = courses);
  }
}
