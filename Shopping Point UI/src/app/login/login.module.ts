import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserloginComponent } from './userlogin/userlogin.component';
import { AppRoutingModule } from '../app-routing.module';
import { UserregisterComponent } from './userregister/userregister.component';
import { FormsModule } from '@angular/forms';
import { LoginRoutingModule } from './login-routing.module';



@NgModule({
  declarations: [
    UserloginComponent,
    UserregisterComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    LoginRoutingModule
  ],
  exports:[
    UserloginComponent,
    UserregisterComponent
  ]
})
export class LoginModule { }
