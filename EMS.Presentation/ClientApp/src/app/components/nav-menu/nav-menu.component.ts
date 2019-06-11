import {AfterViewInit, Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  hideLogin() {
    return !!localStorage.getItem('userID');
  }

  logout() {
    localStorage.removeItem('userID');
    localStorage.removeItem('token');
  }

  ngOnInit() {
    // (document.querySelector('.navbar') as HTMLElement).style.backgroundColor = 'red';
    // (document.querySelector('.navbar') as HTMLElement).style.backgroundColor = 'red';
  }
}
