import {Injectable} from '@angular/core';
import {HttpHeaders, HttpClient} from '@angular/common/http';
import {Observable, of, throwError as observableThrowError} from 'rxjs';
import {Course} from '../models/course';
import {Exam} from '../models/exam';
import {Student} from '../models/student';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${localStorage.getItem('token')}`
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
    return this.http.get<Student[]>(this.url, httpOptions);
  }

  getStudentCourses(id: string): Observable<Course[]> {
    this.url = `http://localhost:11111/api/v1/students/${id}/courses`;
    return this.http.get<Course[]>(this.url, httpOptions);
  }

  getStudentExams(id: string): Observable<Exam[]> {
    this.url = `http://localhost:11111/api/v1/students/${id}/exams`;
    return this.http.get<Exam[]>(this.url, httpOptions);
  }

  getStudentsByNameAndCourse(name: string, course: string): Observable<Student[]> {
    if (!name.trim()) {
      return of([]);
    }
    this.url = `http://localhost:11111/api/v1/students/${name}/${course}`;
    return this.http.get<Student[]>(this.url, httpOptions);
  }

  getStudentsByName(name: string): Observable<Student[]> {
    if (!name) {
      alert('Alege un student din lista');
    }
    if (!name.trim()) {
      return of([]);
    }
    this.url = `http://localhost:11111/api/v1/students/${name}`;
    return this.http.get<Student[]>(this.url, httpOptions);
  }

  updateStudent(student: Student): Observable<Student> {
    const updateStudent = {
      id: student.id,
      name: student.name
    };
    this.url = `http://localhost:11111/api/v1/students/${updateStudent.id}/`;
    return this.http.put<Student>(this.url, updateStudent, httpOptions);
  }

  checkIn(studentId: string, examId: string, url: string): Observable<any> {
    return this.http.put<any>(url, null, httpOptions);
  }

}
