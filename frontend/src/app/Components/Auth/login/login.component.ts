import { Router } from '@angular/router';
import { AuthAPIService } from './../../../Services/auth-api.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(private authAPIService: AuthAPIService, private router: Router){

  }
  


  onSubmit(formValues : any){
    console.log(formValues);
    this.authAPIService.loginAPI(formValues).subscribe(response => {
      console.log(response);
      
      alert(response._message)
      if(response._statusCode == 200){

        this.authAPIService.getUserByUserNameAPI(formValues.userName).subscribe(response => {
          console.log(response);
          
          localStorage.setItem('role', response.role);
          localStorage.setItem('isLoggedIn', response.isLoggedIn);
          this.router.navigate(['/dashboard']);

        });

      }
    })
  }
}
