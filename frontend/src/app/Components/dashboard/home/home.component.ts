import { CarsAPIService } from './../../../Services/cars-api.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  AllCarsList: any[] | undefined;
  foundClickedCar: any;
  editClickedCar: number | undefined;

  constructor(private carsAPIService: CarsAPIService){
    this.getAllCars();
  }

  getAllCars(){
    this.carsAPIService.getAllCarsAPI().subscribe(response => {
      console.log(response);
      this.AllCarsList = response;
    })
  }

  findClickedCar(_carId: number){
    this.carsAPIService.getCarByIdAPI(_carId).subscribe(response => {
      this.foundClickedCar = response;
    });
  }
  
  EditClickedCar(_carId: number){
    
    let gotRole = localStorage.getItem('role');
    if(gotRole === 'Seller'){
      this.editClickedCar = _carId;
    }

    else{
      alert("Only seller can edit the car")
    }

  }


}
