import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../Models/user';
import { LoginToken } from '../Models/login-token';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  apiUrl:string="https://localhost:7277/api/"
  headers=new Headers();
  constructor(private http:HttpClient) {
    //this.headers.append("Authorization","Bearer "+localStorage.getItem("token"));
  }

  getUserById(id:string): Observable<User>{
    return this.http.get<User>(this.apiUrl+"users/"+id);
  }

  getLogedInUser():LoginToken|null{
    let userString=localStorage.getItem("user");
    if(userString==null)return null;
    let user=JSON.parse(userString);
    return user;
  }

  updateProfile(id:number,updatedata:any): Observable<User>{
    return this.http.put<User>(this.apiUrl+"user",updatedata);
  }

  changePassword(currentPassword:string,newPassword:string): Observable<void>{
    return this.http.put<void>(this.apiUrl+"user/changepassword",{currentPassword,newPassword});
  }

}
