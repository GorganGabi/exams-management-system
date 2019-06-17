import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import {Observable, of, throwError as observableThrowError} from 'rxjs';
import {User} from '../models/user';
import {catchError} from 'rxjs/operators';
import * as HTTPStatusCodes from 'http-status-codes';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
};

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private loginUrl = 'http://localhost:11111/api/v1/Account/Login';

  constructor(private http: HttpClient) {
  }

// todo: consider handling the errors at component level
  static errorHandler(error: HttpErrorResponse) {
    if (error.status === HTTPStatusCodes.UNPROCESSABLE_ENTITY) {
      alert('Date invalide. Te rog să reintroduci credențialele');
    }
    return observableThrowError(error.message);
  }

  getUser(email: string, password: string, role: string): Observable<User> {
    const body = {
      'email': email,
      'password': password,
      'role': role
    };
    return this.http.post<User>(this.loginUrl, body, httpOptions).pipe(
      catchError(LoginService.errorHandler)
    );
  }
}
