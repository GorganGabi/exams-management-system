import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Exam } from './exam';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class ExamService {
  url: string = "http://localhost:11111/api/v1/exams";

  constructor(private http: HttpClient) { }

  getExams(): Observable<Exam[]> {
    return this.http.get<Exam[]>(this.url)
  }

  getExam(id: string): Observable<Exam> {
    const url = `${this.url}/${id}`;
    return this.http.get<Exam>(url);
  }

  updateExam(exam: Exam): Observable<any> {
    const url = `${this.url}/${exam.id}`;
    const body = {
      type: exam.type,
      date: exam.date,
      courseId: exam.courseId,
      room: exam.room
    }

    return this.http.put<Exam>(url, body, httpOptions);
  }

  deleteExam(id: string): Observable<Exam> {
    const url = `${this.url}/${id}`;
    return this.http.delete<Exam>(url, httpOptions);
  }

  createExam(exam: Exam): Observable<Exam> {
    var examCreateModel = {
      type: exam.type,
      date: exam.date,
      courseId: exam.courseId,
      professorId: localStorage.getItem('userID'),
      room: exam.room
    }
    console.log(examCreateModel);
    return this.http.post<Exam>(this.url, examCreateModel, httpOptions);
  }
}
