import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {Grade} from '../models/grade';

@Injectable({
  providedIn: 'root'
})
export class GradeService {
  url = 'http://localhost:11111/api/v1/grades';

  constructor(private http: HttpClient) {
  }

  getGrades(): Observable<Grade[]> {
    return this.http.get<Grade[]>(this.url);
  }

  getGrade(id: string): Observable<Grade> {
    const url = `${this.url}/${id}`;
    return this.http.get<Grade>(url);
  }

  createGrade(grade: Grade): Observable<Grade> {
    return this.http.post<Grade>(this.url, grade);
  }

  updateGrade(grade: Grade): Observable<Grade> {
    const gradeUrl = `${this.url}/${grade.id}`;
    const updateGradeModel = {
      value: grade.value,
      examId: grade.examId,
      studentId: grade.student.id,
      isConfirmed: grade.isConfirmed
    };

    return this.http.put<Grade>(gradeUrl, updateGradeModel);
  }

  deleteGrade(grade: Grade): Observable<Grade> {
    const gradeUrl = `${this.url}/${grade.id}`;
    return this.http.delete<Grade>(gradeUrl);
  }
}
