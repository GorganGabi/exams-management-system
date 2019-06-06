import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {User} from '../models/user';
import {error} from '@angular/compiler/src/util';

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

  getUser(email: string, password: string, role: string): Observable<User> {
    const body = {
      'email': email,
      'password': password,
      'role': role
    };
    const http$ = this.http.post<User>(this.loginUrl, body, httpOptions);

    http$.subscribe(
      () => {
      },
      err => {
        if (err.status === 422) {
          alert('Username sau parola sau rol gresit');
        }
      },
      () => {
      }
    );

    return http$;
  }
}
