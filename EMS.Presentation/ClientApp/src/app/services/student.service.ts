import {Injectable} from '@angular/core';
import {HttpHeaders, HttpClient} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {Course} from '../models/course';
import {Exam} from '../models/exam';
import {Student} from '../models/student';
import {catchError} from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
};

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  private url: string;

  constructor(private http: HttpClient) {
  }

  getStudents(): Observable<Student[]> {
    this.url = `http://localhost:11111/api/v1/students/`;
    return this.http.get<Student[]>(this.url);
  }

  getStudentCourses(id: string): Observable<Course[]> {
    this.url = `http://localhost:11111/api/v1/students/${id}/courses`;
    return this.http.get<Course[]>(this.url);
  }

  getStudentExams(id: string): Observable<Exam[]> {
    this.url = `http://localhost:11111/api/v1/students/${id}/exams`;
    return this.http.get<Exam[]>(this.url);
  }

  getStudentsByNameAndCourse(name: string, course: string): Observable<Student[]> {
    if (!name.trim()) {
      return of([]);
    }
    this.url = `http://localhost:11111/api/v1/students/${name}/${course}`;
    return this.http.get<Student[]>(this.url);
  }

  getStudentsByName(name: string): Observable<Student[]> {
    if (!name.trim()) {
      return of([]);
    }
    this.url = `http://localhost:11111/api/v1/students/${name}`;
    return this.http.get<Student[]>(this.url);
  }

  updateStudent(student: Student): Observable<Student> {
    const updateStudent = {
      id: student.id,
      name: student.name,
    };
    this.url = `http://localhost:11111/api/v1/students/${updateStudent.id}/`;
    return this.http.put<Student>(this.url, updateStudent);
  }

  checkIn(studentId: string, examId: string, url: string): Observable<any> {
    return this.http.put<any>(url, null);
  }

}
