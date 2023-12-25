import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AddCarGuard implements CanActivate {

  constructor(private router: Router, private jwtHelper: JwtHelperService){}


  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      
      let gotJwt = localStorage.getItem('jwt');
      
      if(gotJwt){
        if(!this.jwtHelper.isTokenExpired(gotJwt)){

          const decodedToken = this.jwtHelper.decodeToken(gotJwt);
          // console.log(decodedToken);
          
          if (decodedToken.role && decodedToken.role === 'Seller') {
            return true;
          }
  
          else{
            alert("Only sellers can access this!")
            return false;
          } 
  
        }
  
        else{
          alert("Token Expired")
          // localStorage.removeItem('jwt');
          this.router.navigate(['login'])
          return false;
        }
      }
      
      else{
        alert("No jwt found!!")
        this.router.navigate(['/login']);
        return false;
      }
    
  }
  
}
