import {Component, OnInit} from '@angular/core';
import {LoginService} from '../../services/login.service';
import {User} from '../../models/user';
import {Router} from '@angular/router';

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
              private route: Router) {
  }

  ngOnInit() {
  }

  login(): void {
    this.loginService.getUser(this.email, this.password, this.role)
      .subscribe(user => {
        this.user = user,
          localStorage.setItem('userID', this.user.id),
          localStorage.setItem('token', this.user.token),
          this.route.navigate(['/']);
      });
  }
}
