import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  url = 'http://localhost:11111/api/v1/Account/Register';

  constructor(private http: HttpClient) {
  }

  createUser(email: string, password: string, confirmPassword: string, role: string): Observable<any> {
    const data = {
      email: email,
      password: password,
      confirmPassword: confirmPassword,
      role: role
    };
    return this.http.post(this.url, data);
  }
}
