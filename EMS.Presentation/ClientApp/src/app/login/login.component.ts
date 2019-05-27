import { Component, OnInit } from '@angular/core';
import { LoginService } from '../login.service';
import { User } from '../user';
import { ParamMap, Router } from '@angular/router';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user: User;
  email: string;
  password: string;
  role: string;

  constructor(private loginService: LoginService,
              private route: Router) { }

  ngOnInit() {
  }

  login(): void {
    console.log(this.email, this.password, this.role);
    this.loginService.getUser(this.email, this.password, this.role)
       .subscribe(user => {
         this.user = user,
         localStorage.setItem("userID", this.user.id),
         this.route.navigate(['/'])});
  }
}
