import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoginRegisterService } from 'src/app/Services/login-register.service';
import { SharedService } from 'src/app/Services/shared.service';


@Component({
  selector: 'app-userregister',
  templateUrl: './userregister.component.html',
  styleUrls: ['./userregister.component.css']
})
export class UserregisterComponent implements OnInit {

  mindate="";

  loading=false;
  newUser={
    email: "",
    password: "",
    name: "",
    dob: "",
    gender: "",
  }
  constructor(private loginService:LoginRegisterService,
    private router:Router,
    private sharedService:SharedService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
    this.loading=false;
    this.mindate=new Date().toISOString().split('T')[0];
  }

  registerUser():void{
    this.loading=true;
    this.loginService.registerUser({
      email: this.newUser.email,
    password: this.newUser.password,
    name: this.newUser.name,
    dob: this.newUser.dob,
    gender: this.newUser.gender,
    photoUrl:"default"
    }).subscribe({
      next:data=>{
        localStorage.setItem("user",JSON.stringify(data));
        this.sharedService.triggerReload();
        this.toastr.success("You have Successfully Registered");
        this.router.navigate(["/products/userview"])
        this.loading=false;
      },
      error:err=>{
        this.toastr.error(err.error.message);
        this.loading=false;
      }
    })
  }

}
