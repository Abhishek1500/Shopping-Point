import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductAndCategoryComponent } from './productsManage/product-and-category.component';
import { UserproductComponent } from './userproduct/userproduct.component';
import { PageNotfoundComponent } from '../page-notfound/page-notfound.component';
import { AdminIdentityGuardService } from '../Services/admin-identity-guard.service';
import { StudentIdentityGuardService } from '../Services/student-identity-guard.service';

const routes: Routes = [
    {path:"adminview",component:ProductAndCategoryComponent,canActivate:[AdminIdentityGuardService]},
    {path:"userview",component:UserproductComponent,canActivate:[StudentIdentityGuardService]},
  {path:"**",component:PageNotfoundComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class productRoutingModule { }
