import {Injectable} from '@angular/core';
import {Observable, of} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor() {
  }

  createUser(email: string, password: string, confirmPassword: string, role: string): Observable<any> {
    return of([]);
  }
}
