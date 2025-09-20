import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginToken } from '../Models/login-token';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginRegisterService {
apiUrl:string="https://localhost:7277/api/account"
  headers=new Headers();
  constructor(private http:HttpClient) {
    
  }

  loginUser(loginBody:any) : Observable<LoginToken>{
    
    return this.http.post<LoginToken>(this.apiUrl+"/login",loginBody);
  }


  registerUser(registerBody:any):Observable<LoginToken>{
    
    return this.http.post<LoginToken>(this.apiUrl+"/register",registerBody);
  }
}
