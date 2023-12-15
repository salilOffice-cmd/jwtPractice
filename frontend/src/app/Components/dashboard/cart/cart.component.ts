import { CarsAPIService } from 'src/app/Services/cars-api.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent {

  gotCars: any[] | undefined;

  constructor(private carsAPIService: CarsAPIService){
    this.gotCars = carsAPIService.localCart 
  }

  sendToCartAPI(cartItems: any){
    
  }


}
