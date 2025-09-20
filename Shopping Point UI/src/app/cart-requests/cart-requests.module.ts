import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyRequestsComponent } from './my-requests/my-requests.component';
import { MyCartComponent } from './my-cart/my-cart.component';
import { RequestsComponent } from './requests/requests.component';
import { MyRequestHistoryComponent } from './my-request-history/my-request-history.component';
import { TableModule } from '../table/table.module';
import { FormsModule } from '@angular/forms'; 
import { CartRequestsRoutingModule } from './cart-requests-routing.module';
import { ProductCardModule } from '../product-card/product-card.module';



@NgModule({
  declarations: [
    MyRequestsComponent,
    MyCartComponent,
    RequestsComponent,
    MyRequestHistoryComponent
  ],
  imports: [
    CommonModule,
    TableModule,
    FormsModule,
    CartRequestsRoutingModule,
    ProductCardModule
  ],
  exports:[
    MyRequestsComponent,
    MyCartComponent,
    RequestsComponent,
    MyRequestHistoryComponent
  ]
})
export class CartRequestsModule { }
