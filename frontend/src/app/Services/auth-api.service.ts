import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthAPIService {

  private url: string = 'https://localhost:7032/api/User'

  constructor(private http: HttpClient){}


  registerAPI(newUser: any) : Observable<any>{
    return this.http.post<any>(this.url + '/register', newUser);
  }

  loginAPI(_user: any) : Observable<any>{
    return this.http.post<any>(this.url + '/login', _user);
  }

  getUserByUserNameAPI(userName: string) : Observable<any>{
    return this.http.get<any>(this.url + `/GetUserByUserName/${userName}`);
  }

}
