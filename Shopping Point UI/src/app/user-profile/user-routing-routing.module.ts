import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserProfileComponent } from './user-profile.component';
import { PageNotfoundComponent } from '../page-notfound/page-notfound.component';

const routes: Routes = [
  {path:"",component:UserProfileComponent},
  {path:"**",component:PageNotfoundComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserProfileRoutingModule { }
