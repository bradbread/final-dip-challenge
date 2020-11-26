import { Injectable } from '@angular/core';
import { 
  Router,
  CanActivate,
  ActivatedRouteSnapshot
} from '@angular/router';
import { AuthService } from './auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})

export class RoleGuardService implements CanActivate {
  constructor(public auth: AuthService, private jwtHelper: JwtHelperService, public router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {

    // this will be passed from the route config
    // on the data property
    const expectedRole = route.data.expectedRole;
    const tokenPayload = this.jwtHelper.decodeToken(localStorage.getItem('Authorisation'));
    

    if (tokenPayload.AuthLevel !== expectedRole) {
      console.log(false);
      console.log(tokenPayload);
      console.log(expectedRole);
      return false;
    }
    console.log(true);
    return true;
  }

}