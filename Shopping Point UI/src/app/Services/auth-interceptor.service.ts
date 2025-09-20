import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, map } from 'rxjs';
import { NavprofilesharedService } from './navprofileshared.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptorService implements HttpInterceptor{

  constructor(private toastr:ToastrService,private router:Router,private navbarservice:NavprofilesharedService) {}

  
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let userString=localStorage.getItem("user");
    if(!userString)return next.handle(req);
    let user=JSON.parse(userString);
    req=req.clone({
      setHeaders:{
        Authorization:"bearer "+user.token
      }
    }
    )
    return next.handle(req)
    // .pipe(catchError((event:Observable<HttpEvent<any>>)=>{
    //   if(event instanceof HttpErrorResponse){
    //     if(event.status==500){
    //       if(event.error.message!=null)
    //         this.toastr.error(event.error.message)
    //       this.router.navigate(["/servererror"])
    //     }
    //     if(event.status==401){
    //       this.navbarservice.SendlogoutInstruction();
    //       this.router.navigate(["/"])
    //       this.toastr.error("Token Expired login again");
          
    //     }
    //   }
    //  return event; 
    // }),(event: Observable<HttpEvent<any>>)=>{
    //   return event;
    // });
  }
}
