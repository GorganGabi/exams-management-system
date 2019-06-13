import {Component, OnInit} from '@angular/core';
import {RegisterService} from '../../services/register.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  email: string;
  password: string;
  confirmPassword: string;
  role: string;

  constructor(private registerService: RegisterService,
              private route: Router) {
  }

  ngOnInit() {
  }

  register() {
    this.registerService.createUser(this.email, this.password, this.confirmPassword, this.role)
      .subscribe(() => {
        this.route.navigate(['/login']);
      });
  }
}
