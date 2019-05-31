import { Component, OnInit } from '@angular/core';
import { Course } from '../../../models/course'
import { CourseService } from '../../../services/course.service';
import { StudentService } from '../../../services/student.service';
import { ProfessorService } from '../../../services/professor.service';
import { Professor } from 'src/app/models/professor';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent implements OnInit {
  courses: Course[];
  professor: Professor;
  role: string;

  constructor(
    private courseService: CourseService,
    private studentService: StudentService,
    private professorService: ProfessorService) { }

  ngOnInit() {
    this.getCourses();
    this.role = localStorage.getItem('userID');
    if (this.role){
    this.professorService.getProfessorById(this.role)
      .subscribe(professor => { this.professor = professor })
    }
  }

  getCourses(): void {
    this.courseService.getCourses()
      .subscribe(courses => { this.courses = courses }, error => console.log(error));
  }

  getCoursesByUserId(): void {
    this.studentService.getStudentCourses(this.role)
      .subscribe(courses => this.courses = courses);
    this.professorService.getProfessorCourses(this.role)
      .subscribe(courses => this.courses = courses);
  }

  deleteCourse(course: Course) {
    this.courseService.deleteCourse(course.id)
      .subscribe(() => location.reload());
  }
}
