import { Router } from '@angular/router';
import { AuthAPIService } from './../../../Services/auth-api.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  constructor(private authAPIService: AuthAPIService, private router: Router){

  }
  

  onSubmit(formValues : any){
    console.log(formValues);
    this.authAPIService.registerAPI(formValues).subscribe(response => {
      alert(response._message);

      if(response._statusCode === 200){
        this.router.navigate(['login'])
      }
    })
  }
}
