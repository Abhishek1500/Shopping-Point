import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Request } from '../Models/request';
import { PendinRequest } from '../Models/pendingrequests';

@Injectable({
  providedIn: 'root'
})
export class CartRequestService {

  apiUrl:string="https://localhost:7102/api/"
  constructor(private http:HttpClient) {
    
  }

  requestProduct(productId:any,count:any) : Observable<void>{
    return this.http.post<void>(this.apiUrl+"order/request",{productId,count});
  }

  getMyRequests():Observable<Request[]>{
    return this.http.get<Request[]>(this.apiUrl+"order/myrequest");
  }

  getMyRequestHistory():Observable<Request[]>{
    return this.http.get<Request[]>(this.apiUrl+"order/myrequesthistory");
  }
  
  getRequests():Observable<PendinRequest[]>{
    return this.http.get<PendinRequest[]>(this.apiUrl+"order/requests");
  }

  addToCart(productId:any,count:any) : Observable<void>{
    return this.http.post<void>(this.apiUrl+"cart/item",{productId,count});
  }

  requestTheCartItem(id:number){
    return this.http.put<void>(this.apiUrl+"cart/item/"+id+"/request",{});
  }

  deleteCartItem(id:number){
    return this.http.delete<void>(this.apiUrl+"cart/item/"+id);
  }

  getMyCart():Observable<Request[]>{
    return this.http.get<Request[]>(this.apiUrl+"cart");
  }
  

  repondToRequest(newStatus:any,reqId:number):Observable<void>{
    return this.http.put<void>(this.apiUrl+"order/request/"+reqId+"/respond",newStatus)
  }


  updateCartItemAtm(newAmt:number,reqId:number): Observable<void>{
    return this.http.put<void>(this.apiUrl+"cart/item/"+reqId+"newAmt",{count:newAmt});
  }

}
