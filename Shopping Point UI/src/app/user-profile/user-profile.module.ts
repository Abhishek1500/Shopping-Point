import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserProfileComponent } from './user-profile.component';
import { FormsModule } from '@angular/forms';
import { TableModule } from '../table/table.module';
import { UserProfileRoutingModule } from './user-routing-routing.module';



@NgModule({
  declarations: [
    UserProfileComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    TableModule,
    UserProfileRoutingModule
  ],
  exports:[
    UserProfileComponent
  ]
})
export class UserProfileModule { }
