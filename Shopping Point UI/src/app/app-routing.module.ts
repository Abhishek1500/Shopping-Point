import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductCardComponent } from './product-card/product-card.component';
import { PageNotfoundComponent } from './page-notfound/page-notfound.component';
import { UserLoginGuardService } from './Services/user-login-guard.service';
import { ServerErrorComponent } from './server-error/server-error.component';

const routes: Routes = [
  {path:"",loadChildren:()=>import('./login/login.module').then(m => m.LoginModule)},
  {path:"products",loadChildren:()=>import('./products/products.module').then(m=>m.ProductsModule),canActivate:[UserLoginGuardService]},
  {path:"requests",loadChildren:() => import('../app/cart-requests/cart-requests.module').then(m => m.CartRequestsModule),canActivate:[UserLoginGuardService]},
  {path:"profile",loadChildren:()=>import("../app/user-profile/user-profile.module").then(m=>m.UserProfileModule),canActivate:[UserLoginGuardService]},
  {path:"servererror",component:ServerErrorComponent},
  {path:"**",component:PageNotfoundComponent}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
