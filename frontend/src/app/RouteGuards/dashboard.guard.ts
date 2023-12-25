import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class DashboardGuard implements CanActivate {

  constructor(private router: Router, private jwtHelper: JwtHelperService){}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    let gotJwt = localStorage.getItem('jwt');
      
    // Token is present and not expired
    if (gotJwt) {
      if(!this.jwtHelper.isTokenExpired(gotJwt)){
        return true; 
      }

      else{
        alert("Token Expired")
        this.router.navigate(['/login']);
        return false;
      }
      // const decodedToken = this.jwtHelper.decodeToken(gotJwt);

      // // Check if the user has the required role (e.g., 'Seller')
      // if (decodedToken.roles && decodedToken.roles.includes('Seller')) {
      //   return true;
      // }
    }

    else{
      alert("No jwt found!!")
      this.router.navigate(['/login']);
      return false;
    }
  
  }
  
}
