import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductAndCategoryComponent } from './productsManage/product-and-category.component';
import { FormsModule } from '@angular/forms';
import { TableModule } from 'src/app/table/table.module';
import { productRoutingModule } from './product-routing.module';
import { UserproductComponent } from './userproduct/userproduct.component';
import { ProductCardModule } from '../product-card/product-card.module';



@NgModule({
  declarations: [
  ProductAndCategoryComponent,
  UserproductComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    TableModule,
    productRoutingModule,
    ProductCardModule
  ],
  exports:[
    ProductAndCategoryComponent
  ]
})
export class ProductsModule { }
