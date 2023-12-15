import { CarsAPIService } from './../../../Services/cars-api.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-buy-cars',
  templateUrl: './buy-cars.component.html',
  styleUrls: ['./buy-cars.component.css']
})
export class BuyCarsComponent {

  gotCars: any[] | undefined;

  constructor(private carsAPIService: CarsAPIService){
    this.fetchCars();
  }


  fetchCars(){
    this.carsAPIService.getCarsByCategoryAPI("Buy").subscribe(response => {
      this.gotCars = response;
    })
  }

}
