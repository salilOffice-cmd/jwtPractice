import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CarsAPIService {

  localCart:any[] = [];

  private url: string = 'https://localhost:7032/api/Car'

  constructor(private http: HttpClient){}


  getAllCarsAPI() : Observable<any>{
    return this.http.get<any>(this.url + '/allCars');
  }

  getCarByIdAPI(carId: number) : Observable<any>{
    return this.http.get<any>(this.url + `/carByID/${carId}`);
  }

  editCarDetailsAPI(_carObj: any) : Observable<any>{
    return this.http.put<any>(this.url + "/editCar", _carObj);
  }

  getCarsByCategoryAPI(_carCategory: string) : Observable<any>{
    return this.http.get<any>(this.url + `/carsByCategory/${_carCategory}`,);
  }

  addCarAPI(_newCar: any) : Observable<any>{
    
    return this.http.post<any>(this.url + '/addCar', _newCar);
  }

  addBookingDetailsAPI(_newBookings: any) : Observable<any>{
    return this.http.post<any>(this.url + '/addBooking', _newBookings);
  }


  addToLocalCart(gotCar: any){
    this.localCart.push(gotCar);
  }

}
