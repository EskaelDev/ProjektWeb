import { Component } from '@angular/core';
import {User} from './_models/user';
import {Router} from '@angular/router';
import {AuthenticationService} from './_services/authentication-service';
import {Role} from './_models/role';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  currentUser: User;
  isLoggedIn: Boolean = false;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    this.authenticationService.currentUser.subscribe(x => {
      this.currentUser = x;
      this.isLoggedIn = this.currentUser != null;
    });
  }

  get isAdmin() {
    return this.currentUser && this.currentUser.role === Role.Admin;
  }

  logout($event) {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }
}
