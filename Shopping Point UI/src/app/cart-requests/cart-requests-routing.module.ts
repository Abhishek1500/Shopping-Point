import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyCartComponent } from './my-cart/my-cart.component';
import { MyRequestsComponent } from './my-requests/my-requests.component';
import { MyRequestHistoryComponent } from './my-request-history/my-request-history.component';
import { RequestsComponent } from './requests/requests.component';
import { PageNotfoundComponent } from '../page-notfound/page-notfound.component';
import { StudentIdentityGuardService } from '../Services/student-identity-guard.service';
import { AdminIdentityGuardService } from '../Services/admin-identity-guard.service';


const routes: Routes = [
  {path:"Mycart",component:MyCartComponent,canActivate:[StudentIdentityGuardService]},
  {path:"myrequests",component:MyRequestsComponent,canActivate:[StudentIdentityGuardService]},
  {path:"requesthistory",component:MyRequestHistoryComponent,canActivate:[StudentIdentityGuardService]},
  {path:"requests",component:RequestsComponent,canActivate:[AdminIdentityGuardService]},
  {path:"**",component:PageNotfoundComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CartRequestsRoutingModule { }
