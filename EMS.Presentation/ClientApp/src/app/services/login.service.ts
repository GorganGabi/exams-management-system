import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
};

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private loginUrl = "http://localhost:11111/api/v1/Account/Login"

  constructor(private http: HttpClient) { }

  getUser(email: string, password: string, role: string): Observable<User> {
    var body = {
      "email": email,
      "password": password,
      "role": role
    }
    return this.http.post<User>(this.loginUrl, body, httpOptions);
  }
}
