import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  hideLogin(){
    if (localStorage.getItem('userID'))
    {
      return true;
    }
    return false;
  }

  logout(){
    localStorage.removeItem('userID');
  }
}
