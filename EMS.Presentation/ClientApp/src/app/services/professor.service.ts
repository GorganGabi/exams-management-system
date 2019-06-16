import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Professor } from '../models/professor';
import { Course } from '../models/course';
import {Exam} from '../models/exam';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${localStorage.getItem('token')}`
  })
};

@Injectable({
  providedIn: 'root'
})
export class ProfessorService {
  private professorUrl = 'http://localhost:11111/api/v1/professors';

  constructor(private http: HttpClient) { }

  getProfessors(): Observable<Professor[]> {
    return this.http.get<Professor[]>(this.professorUrl, httpOptions);
  }

  getProfessorById(id: string): Observable<Professor> {
    const url = `${this.professorUrl}/${id}`;
    return this.http.get<Professor>(url, httpOptions);
  }

  getProfessorCourses(id: string): Observable<Course[]> {
    const url = `http://localhost:11111/api/v1/professors/${id}/courses`;
    return this.http.get<Course[]>(url, httpOptions);
  }

  getProfessorExams(id: string): Observable<Exam[]> {
    const url = `http://localhost:11111/api/v1/professors/${id}/exams`;
    return this.http.get<Exam[]>(url, httpOptions);
  }
}
