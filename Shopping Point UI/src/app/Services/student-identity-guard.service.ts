import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { UserService } from './user.service';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class StudentIdentityGuardService implements CanActivate{

  constructor(private router:Router,private userService:UserService,private toastr:ToastrService) { }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    let user=this.userService.getLogedInUser();
    if(user===null){
      this.router.navigate([""]);
      return false;
    }
    if(user.isAdmin){
      this.toastr.error("Login as User")
      this.router.navigate([""]);
      return false;
    }
    return true;
  }
}