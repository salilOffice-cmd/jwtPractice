import { CarsAPIService } from './../../../../Services/cars-api.service';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-edit-car-details',
  templateUrl: './edit-car-details.component.html',
  styleUrls: ['./edit-car-details.component.css']
})
export class EditCarDetailsComponent {
  @Input() ClickedCar:any;

  constructor(private carsAPIService: CarsAPIService){

  }

  onSubmit(formValues: any){

    let editCar = {
      carId : this.ClickedCar.carId,
      carName : formValues.carName,
      contact_Details : formValues.contact_Details,
      price : +formValues.price,
      category : formValues.category
    }

    this.carsAPIService.editCarDetailsAPI(editCar).subscribe(response => {
      alert(response._message);
    })
  }
}
