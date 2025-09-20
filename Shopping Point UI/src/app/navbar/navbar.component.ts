import { Component, OnDestroy, OnInit } from '@angular/core';
import { UserService } from '../Services/user.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { SharedService } from '../Services/shared.service';
import { ToastrService } from 'ngx-toastr';

import { LoginToken } from '../Models/login-token';
import { NavprofilesharedService } from '../Services/navprofileshared.service';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  login=false;
  isAdmin=false;
  profileImg="";
  private loadsubscription:Subscription;
  private navbarSubscription:Subscription;
  private logoutSubscription:Subscription;
  // apiUrl="http:/localhost:4200"
  constructor(private userService:UserService,
    private router:Router,
    private sharedService:SharedService,
    private toastr:ToastrService,
    private navprofilesharedserve:NavprofilesharedService) {

      this.navbarSubscription=this.navprofilesharedserve.dataStream$.subscribe(data=>{
      let user=this.userService.getLogedInUser()
        if(user===null){
          this.router.navigate([""]);
          return;
        }
        user.name=data.name;
        user.imageUrl=data.imageUrl;
        this.profileImg=data.imageUrl;
        localStorage.setItem("user",JSON.stringify(user));
    })


      this.loadsubscription=this.sharedService.reloadComponent.subscribe(()=>{
        this.login=true;
        let user=this.userService.getLogedInUser()
        if(user===null){
          router.navigate([""]);
          return;
        }
        this.isAdmin=user.isAdmin;
        this.profileImg=user.imageUrl
      })

      this.logoutSubscription=navprofilesharedserve.logoutStream$.subscribe(()=>{
        localStorage.clear();
        this.profileImg="";
        this.isAdmin=false;
        this.login=false;
      })



  }

  ngOnDestroy(){
    this.loadsubscription.unsubscribe();
    this.navbarSubscription.unsubscribe();
  }
  

  ngOnInit(): void {
    let user=this.userService.getLogedInUser();
    if(user===null){
      return;
    }
    this.login=true;
    this.profileImg=user.imageUrl
    this.isAdmin=user.isAdmin;
  }

  logout(){
    this.login=false;
    this.isAdmin=false;
    this.profileImg="";
    this.toastr.success("logged Out Successfully");
    localStorage.clear();
    this.router.navigate(["/"])
  }




}
