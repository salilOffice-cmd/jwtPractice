import { CarDetailsComponent } from './Components/dashboard/home/car-details/car-details.component';
import { AddCarsComponent } from './Components/dashboard/add-cars/add-cars.component';
import { CartComponent } from './Components/dashboard/cart/cart.component';
import { RentCarsComponent } from './Components/dashboard/rent-cars/rent-cars.component';
import { BuyCarsComponent } from './Components/dashboard/buy-cars/buy-cars.component';
import { HomeComponent } from './Components/dashboard/home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './Components/Auth/register/register.component';
import { LoginComponent } from './Components/Auth/login/login.component';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { DashboardGuard } from './RouteGuards/dashboard.guard';
import { AddCarGuard } from './RouteGuards/add-car.guard';

const routes: Routes = [
  {
    component: RegisterComponent,
    path: 'register'
  },
  {
    component: LoginComponent,
    path: 'login'
  },
  {
    component: DashboardComponent,
    path: 'dashboard',
    canActivate: [DashboardGuard],
    children : [
      {
        component: HomeComponent,
        path : 'home',
        // children: [
        //   {
        //     component: CarDetailsComponent,
        //     path : 'cardetails'
        //   }
        // ]
      },
      {
        component: BuyCarsComponent,
        path: 'buycars'
      },
      {
        component: RentCarsComponent,
        path: 'rentcars'
      },
      {
        component: AddCarsComponent,
        path: 'addcars',
        canActivate: [AddCarGuard],
      },
      {
        component: CartComponent,
        path: 'cart'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
