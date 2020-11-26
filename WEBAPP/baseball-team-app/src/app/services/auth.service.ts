import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import {HttpClient} from '@angular/common/http'
import { Login } from '../models/login';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  loggedIn: BehaviorSubject<boolean>;
  authLevel: BehaviorSubject<boolean>;
  //apiURL: "https://localhost:44338/api";

  constructor(private _http: HttpClient, private jwtHelper: JwtHelperService) { 
    this.loggedIn = new BehaviorSubject(null);
    this.authLevel = new BehaviorSubject(null);
    this.loggedIn.next(false);
    if(this.isLoggedIn()) {
      this.loggedIn.next(true);
      if (this.jwtHelper.decodeToken(localStorage.getItem('Authorisation')).AuthLevel == "Manager")
          this.authLevel.next(true);
          else
          this.authLevel.next(false);
    }
    else
      this.loggedIn.next(false);
      this.authLevel.next(false);
  }

  isLoggedIn() {
    return !this.jwtHelper.isTokenExpired(localStorage.getItem('Authorisation'));
  }

  login(login: Login){
    console.log(login.Username, login.Password);
    return new Promise ((resolve, reject) => {
      this._http.post("https://dip-challenge-apim.azure-api.net/v1/api/Auth/member", login).subscribe(
        (token) => {
          console.log("logged in");
          localStorage.setItem('Authorisation', JSON.stringify(token['token']))
          this.loggedIn.next(true);
          if (this.jwtHelper.decodeToken(token['token']).AuthLevel == "Manager")
          this.authLevel.next(true);
          else
          this.authLevel.next(false);
          console.log(this.jwtHelper.decodeToken(token['token']));
          resolve();
        },
        err => {
          console.log("error login");
          this.loggedIn.next(false);
          this.authLevel.next(false);
          reject(err);
        });
    })
  }

  logout() {
    localStorage.removeItem('Authorisation');
    this.loggedIn.next(false);
    this.authLevel.next(false);
  }

}
