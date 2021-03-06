import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {Course} from '../models/course';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Grade} from '../models/grade';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${localStorage.getItem('token')}`
  })
};

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  private coursesUrl = 'http://localhost:11111/api/v1/courses';

  constructor(private http: HttpClient) {
  }

  getCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(this.coursesUrl, httpOptions);
  }

  getCourse(id: string): Observable<Course> {
    const url = `${this.coursesUrl}/${id}`;
    return this.http.get<Course>(url, httpOptions);
  }

  updateCourse(course: Course): Observable<any> {
    const url = `${this.coursesUrl}/${course.id}`;
    const body = {
      title: course.title,
      universityYear: course.universityYear,
      studentYear: course.studentYear,
      semester: course.semester
    };

    return this.http.put<Course>(url, body, httpOptions);
  }

  createCourse(course: Course): Observable<Course> {
    const courseCreateModel = {
      title: course.title,
      universityYear: course.universityYear,
      studentYear: course.studentYear,
      semester: course.semester,
      professorId: localStorage.getItem('userID')
    };
    return this.http.post<Course>(this.coursesUrl, courseCreateModel, httpOptions);
  }

  deleteCourse(id: string): Observable<Course> {
    const url = `${this.coursesUrl}/${id}`;
    return this.http.delete<Course>(url, httpOptions);
  }

  getCourseGrades(id: string): Observable<Grade[]> {
    const url = `${this.coursesUrl}/${id}/grades`;
    return this.http.get<Grade[]>(url, httpOptions);
  }
}
