import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {

  constructor(private router:Router){}
  signout(){
    localStorage.clear();
    this.router.navigate(['/login']);
    alert("Sign out Successfully!")
  }
}
