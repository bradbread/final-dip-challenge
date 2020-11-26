import { Component } from '@angular/core';
import { AuthService } from './services/auth.service'
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'coffee-club';
  auth: boolean;
  authLevel: boolean;
  
  constructor(private authService: AuthService, private router: Router) {
    this.authService.loggedIn.subscribe(data => {
      this.auth = data;
    });
    this.authService.authLevel.subscribe(data => {
      this.authLevel = data;
    });
   }

   logout() {
    this.authService.logout();
  }
}
