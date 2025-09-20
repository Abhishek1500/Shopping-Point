import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoginRegisterService } from 'src/app/Services/login-register.service';
import { SharedService } from 'src/app/Services/shared.service';

@Component({
  selector: 'app-userlogin',
  templateUrl: './userlogin.component.html',
  styleUrls: ['./userlogin.component.css']
})

export class UserloginComponent implements OnInit {
  loading=false;
  useremail="";
  password="";
  constructor(private loginRegisterService:LoginRegisterService,
    private router:Router,private toastr:ToastrService,
    private sharedService:SharedService) { }

  ngOnInit(): void {
    this.loading=false;
  }

  loginUser(){
    this.loading=true;
    this.loginRegisterService.loginUser({
      email:this.useremail,
      password:this.password
    }).subscribe({
      next: data=>{
        localStorage.setItem("user",JSON.stringify(data));
        this.sharedService.triggerReload();
        this.toastr.success("You have Successfully Loged in");
        this.loading=false;
        if(data.isAdmin)
        this.router.navigate(['/products/adminview'])
        else
        this.router.navigate(['/products/userview'])
        this.loading=false;
      },
      error:err=>{
        if(err.status==500){
          this.toastr.error("Unable to connect to Server")
        }
        else{
          let e=err.error.message;
          this.toastr.error(err.error.message);
        }
        this.loading=false;
      }
    })
  }

}
