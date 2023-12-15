import { Router } from '@angular/router';
import { CarsAPIService } from './../../../Services/cars-api.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-add-cars',
  templateUrl: './add-cars.component.html',
  styleUrls: ['./add-cars.component.css']
})
export class AddCarsComponent {

  constructor(private carsAPIService: CarsAPIService, private router: Router){}

  onSubmit(formValues : any){
    console.log(formValues);
    this.carsAPIService.addCarAPI(formValues).subscribe(response => {
      console.log(response);
      alert(response._message);
      this.router.navigate(['dashboard/home'])
    })
  }
}
