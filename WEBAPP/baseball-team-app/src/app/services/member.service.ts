import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import {HttpClient} from '@angular/common/http'
import { SignUp } from '../models/signup';

@Injectable({
  providedIn: 'root'
})
export class MemberService {

  constructor(private _http: HttpClient, private jwtHelper: JwtHelperService) { }

  signup(signup) {
    return new Promise ((resolve, reject) => {
      this._http.post("https://dip-challenge-apim.azure-api.net/v1/api/members", signup).subscribe(
        (token) => {
          console.log("member created");

          resolve();
        },
        err => {
          console.log("error in signup");
          reject(err);
        });
    })
  }
}
