import { AddCarsComponent } from './Components/dashboard/add-cars/add-cars.component';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { RegisterComponent } from './Components/Auth/register/register.component';
import { LoginComponent } from './Components/Auth/login/login.component';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { CartComponent } from './Components/dashboard/cart/cart.component';
import { RentCarsComponent } from './Components/dashboard/rent-cars/rent-cars.component';
import { BuyCarsComponent } from './Components/dashboard/buy-cars/buy-cars.component';
import { HomeComponent } from './Components/dashboard/home/home.component';
import { CarDetailsComponent } from './Components/dashboard/home/car-details/car-details.component';
import { EditCarDetailsComponent } from './Components/dashboard/home/edit-car-details/edit-car-details.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    DashboardComponent,
    HomeComponent,
    BuyCarsComponent,
    RentCarsComponent,
    CartComponent,
    AddCarsComponent,
    CarDetailsComponent,
    EditCarDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
