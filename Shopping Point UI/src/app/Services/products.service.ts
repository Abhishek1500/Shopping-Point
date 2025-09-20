import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../Models/Product';
import { CategorySummary } from '../Models/category-summary';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  apiUrl:string="https://localhost:7018/api/"
  headers=new Headers();
  constructor(private http:HttpClient) {
    
  }

  getAllProducts() : Observable<Product[]>{
    return this.http.get<Product[]>(this.apiUrl+"products");
  }


  deleteProduct(id:number):Observable<void>{
    return this.http.delete<void>(this.apiUrl+"product/"+id);
  }

  addNewProduct(product :any):Observable<Product>{
   
    return this.http.post<Product>(this.apiUrl+"product",product);
  }


  updateProduct(Id :any,body:any):Observable<Product>{
    return this.http.put<Product>(this.apiUrl+"product/"+Id,body);
  }

  getAllCAtegories() : Observable<CategorySummary[]>{
    return this.http.get<CategorySummary[]>(this.apiUrl+"categories/Summary");
  }

  addNewCategory(category:any):Observable<any>{
    return this.http.post<any>(this.apiUrl+"category",category);
  }

  deleteCategory(id:number): Observable<void>{
    return this.http.delete<void>(this.apiUrl+"category/"+id);
  }

  updateCategory(id:number,categoryName:string): Observable<any>{
    return this.http.put<any>(this.apiUrl+"category/"+id,{categoryName});
  }

}
