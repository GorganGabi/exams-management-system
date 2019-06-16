import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
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
export class GradeService {
  url = 'http://localhost:11111/api/v1/grades';

  constructor(private http: HttpClient) {
  }

  getGrades(): Observable<Grade[]> {
    return this.http.get<Grade[]>(this.url, httpOptions);
  }

  getGrade(id: string): Observable<Grade> {
    const url = `${this.url}/${id}`;
    return this.http.get<Grade>(url, httpOptions);
  }

  createGrade(grade: object): Observable<Grade> {
    console.log(grade);
    return this.http.post<Grade>(this.url, grade, httpOptions);
  }

  updateGrade(grade: Grade): Observable<Grade> {
    const gradeUrl = `${this.url}/${grade.id}`;
    const updateGradeModel = {
      value: grade.value,
      examId: grade.examId,
      studentId: grade.student.id,
      isConfirmed: grade.isConfirmed
    };

    return this.http.put<Grade>(gradeUrl, updateGradeModel, httpOptions);
  }

  deleteGrade(grade: Grade): Observable<Grade> {
    const gradeUrl = `${this.url}/${grade.id}`;
    return this.http.delete<Grade>(gradeUrl, httpOptions);
  }
}
