import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Professor } from '../models/professor';
import { Course } from '../models/course';
import {Exam} from '../models/exam';

@Injectable({
  providedIn: 'root'
})
export class ProfessorService {
  private professorUrl = 'http://localhost:11111/api/v1/professors';

  constructor(private http: HttpClient) { }

  getProfessors(): Observable<Professor[]> {
    return this.http.get<Professor[]>(this.professorUrl);
  }

  getProfessorById(id: string): Observable<Professor> {
    const url = `${this.professorUrl}/${id}`;
    return this.http.get<Professor>(url);
  }

  getProfessorCourses(id: string): Observable<Course[]> {
    const url = `http://localhost:11111/api/v1/professors/${id}/courses`;
    return this.http.get<Course[]>(url);
  }

  getProfessorExams(id: string): Observable<Exam[]> {
    const url = `http://localhost:11111/api/v1/professors/${id}/exams`;
    return this.http.get<Exam[]>(url);
  }
}
