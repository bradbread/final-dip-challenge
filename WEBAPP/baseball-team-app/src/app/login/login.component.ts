import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  Login(form) {
    console.log(form.value);
    this.authService.login(form.value);
    console.log(this.authService.loggedIn);
    this.router.navigate(['/']);
    form.reset();
  }

}