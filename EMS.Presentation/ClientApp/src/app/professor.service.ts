import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Professor } from './Professor';

@Injectable({
  providedIn: 'root'
})
export class ProfessorService {
  private professorUrl = 'http://localhost:52729/api/v1/courses';

  constructor(private http: HttpClient) { }

  getProfessors(): Observable<Professor[]> {
    return this.http.get<Professor[]>(this.professorUrl);
  }
}
