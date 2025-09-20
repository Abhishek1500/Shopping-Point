import { Component, OnInit } from '@angular/core';
import { UserService } from '../Services/user.service';
import { Route, Router } from '@angular/router';
import { Toast, ToastrService } from 'ngx-toastr';
import { NavprofilesharedService } from '../Services/navprofileshared.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  loading=false;
  mindate="";

  newUser={
    id:0,
    name:"",
    dob:"",
    gender:"",
    email:"",
    profile:"",
    image:""
  }
  presentdata={
    id:0,
    name:"",
    dob:"",
    gender:"",
    email:"",
    profile:"",
    image:"",
  };

  temp:any;
  password={
    currentpass:"",
    newpass:""
  }

  constructor(private userService:UserService,
    private router:Router,
    private toastr:ToastrService,
    private navbarSubscrbe:NavprofilesharedService) { }

  ngOnInit(): void {

    this.mindate=new Date().toISOString().split("T")[0];
    let user=this.userService.getLogedInUser();
    if(user==null){
      this.router.navigate([""]);
      return;
    }

    this.userService.getUserById(user.id).subscribe({
      next:data=>{
        this.newUser.id=this.presentdata.id=data.id;
        this.newUser.email=this.presentdata.email=data.email;
        this.newUser.gender=this.presentdata.gender=data.gender;
        this.newUser.name=this.presentdata.name=data.name;
        this.newUser.image=this.presentdata.image=data.photoUrl;
        this.newUser.dob=this.presentdata.dob=(data.dob+"").split("T")[0];
        this.newUser.profile=this.presentdata.profile=data.isAdmin?"Admin":"User";
      },
      error:err=>{
        this.toastr.error(err.error.message)
      }
    })
  }

  changeHappen(){
    return (this.newUser.dob!=this.presentdata.dob||
    this.newUser.name.trim().toUpperCase()!=this.presentdata.name.toUpperCase().trim()||
    this.newUser.gender!=this.presentdata.gender||this.presentdata.image!=this.newUser.image)
  }

  changepassword(comp:any,comp1:any){
    this.loading=true;
    this.userService.changePassword(this.password.currentpass,this.password.newpass).subscribe({
      next:data=>{
        this.toastr.success("password updated successfully");
        this.loading=false;
      },
      error:err=>{
        this.toastr.error(err.error.message);
        this.loading=false;
      }
    })
    this.password.currentpass=this.password.newpass="";
    comp.control.reset();
    comp1.control.reset();
  }


  onFileSelected(event: any): void {
    const file: File = event.target.files[0];
    if (file&&(file.type==='image/jpeg'||file.type==='image/png')) {
      const reader = new FileReader();
    reader.onload = (e:any) => {
      const base64Data = reader.result as string ;
      if(this.newUser.image!=base64Data)
        this.newUser.image=base64Data;
      else
      this.toastr.error("image is same as present image");
    };
    reader.readAsDataURL(file);
    }else{
      this.toastr.error("please file should be jpeg or png format")
    }
  }

  save(){
    this.loading=true;
    this.userService.updateProfile(this.newUser.id,{
      name: this.newUser.name,
      dob: this.newUser.dob,
      gender: this.newUser.gender,
      photoUrl: this.newUser.image}).subscribe({
        next:data=>{
          this.newUser.id=this.presentdata.id=data.id;
          this.newUser.email=this.presentdata.email=data.email;
          this.newUser.gender=this.presentdata.gender=data.gender;
          this.newUser.name=this.presentdata.name=data.name;
          this.newUser.dob=this.presentdata.dob=(data.dob+"").split("T")[0];
          this.newUser.profile=this.presentdata.profile=data.isAdmin?"Admin":"User";
          this.newUser.image=this.presentdata.image=data.photoUrl;
          this.toastr.success("profile updated");
          this.navbarSubscrbe.setNewUserDetailInStorage({name:this.newUser.name,imageUrl:this.newUser.image})
          this.loading=false;
        },
        error:err=>{
          this.toastr.error("unable to update profile");
          this.newUser.id=this.presentdata.id;
          this.newUser.email=this.presentdata.email;
          this.newUser.gender=this.presentdata.gender;
          this.newUser.name=this.presentdata.name;
          this.newUser.dob=this.presentdata.dob;
          this.newUser.profile=this.presentdata.profile;
          this.newUser.image=this.presentdata.image;
          this.loading=false;
        }
  })
  }

}
