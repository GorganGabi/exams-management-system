import {Component} from '@angular/core';
import {LoginService} from '../../services/login.service';
import {User} from '../../models/user';
import {Router} from '@angular/router';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  user: User;
  email: string;
  password: string;
  role: string;

  constructor(private modalService: NgbModal,
              private loginService: LoginService,
              private route: Router) {
  }

  open(content) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'});
  }

  login(): void {
    this.loginService.getUser(this.email, this.password, this.role)
      .subscribe(user => {
        console.log(user);
        this.user = user;
        localStorage.setItem('userID', this.user.id);
        localStorage.setItem('token', this.user.token);
        this.route.navigate(['/']);
      });
  }
}
