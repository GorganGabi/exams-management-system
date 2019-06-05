import {Injectable} from '@angular/core';
import {HttpHeaders, HttpClient} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {Course} from '../models/course';
import {Exam} from '../models/exam';
import {Student} from '../models/student';


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

  getStudentCourses(id: string): Observable<Course[]> {
    this.url = `http://localhost:11111/api/v1/students/${id}/courses`;
    return this.http.get<Course[]>(this.url);
  }

  getStudentExams(id: string): Observable<Exam[]> {
    this.url = `http://localhost:11111/api/v1/students/${id}/exams`;
    return this.http.get<Exam[]>(this.url);
  }

  getStudentsByName(name: string): Observable<Student[]> {
    if (!name.trim()) {
      // if not search term, return empty hero array.
      return of([]);
    }
    this.url = `http://localhost:11111/api/v1/students/${name}`;
    return this.http.get<Student[]>(this.url);
  }


}
