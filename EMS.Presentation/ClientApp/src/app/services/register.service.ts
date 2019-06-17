import {Injectable} from '@angular/core';
import {Observable, throwError as observableThrowError} from 'rxjs';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {catchError} from "rxjs/operators";
import * as HTTPStatusCodes from "http-status-codes";
import {Register} from "ts-node";

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  url = 'http://localhost:11111/api/v1/Account/Register';

  constructor(private http: HttpClient) {
  }

// todo: consider handling the errors at component level
  static errorHandler(error: HttpErrorResponse) {
    if (error.status === HTTPStatusCodes.UNPROCESSABLE_ENTITY) {
      alert('Email-ul nu este valid');
    }
    return observableThrowError(error.message);
  }

  createUser(email: string, password: string, confirmPassword: string, role: string): Observable<any> {
    const data = {
      email: email,
      password: password,
      confirmPassword: confirmPassword,
      role: role
    };

    return this.http.post(this.url, data).pipe(
      catchError(RegisterService.errorHandler)
    );
  }

}
