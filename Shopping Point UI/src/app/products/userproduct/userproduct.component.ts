import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Product } from 'src/app/Models/Product';
import { CategorySummary } from 'src/app/Models/category-summary';
import { CartRequestService } from 'src/app/Services/cartrequest.service';
import { ProductsService } from 'src/app/Services/products.service';

@Component({
  selector: 'app-userproduct',
  templateUrl: './userproduct.component.html',
  styleUrls: ['./userproduct.component.css']
})
export class UserproductComponent implements OnInit {


   pageinfo={
    totalEntity:0,
    currentPage:1,
    perpageentity:4,
    pagepossibe:0
  }

  filteredPaginatedData:Product[]=[];

  loading :boolean=false;

  filteringinfo={
    serched:"",
    filter:"",
    category:""
  }

  qty:number=0;

  mainArray:Product[]=[];
  categoryArray:string[]=[];
  SerchableArrayIndex:number[]=[0,1]
  TitleArray:string[]=["Product Name","Company"]



  constructor(private prodserve:ProductsService,
    private toastr:ToastrService,
    private cartService:CartRequestService) { }

  ngOnInit(): void {
    this.loading=true;
    this.prodserve.getAllProducts().subscribe({
      next:data=>{
        this.mainArray=data;
        this.paginateNow(this.pageinfo.currentPage)
        this.loading=false;
      },
      error:err=>{
        this.toastr.error(err.error.message);
        this.loading=false
      }
    })

    this.loading=true;

    this.prodserve.getAllCAtegories().subscribe({
      next:data=>{
        this.categoryArray=this.convertCategoryToTableFormat(data);
        this.loading=false
      },
      error:err=>{
        this.toastr.error(err.error.message)
        this.loading=false;
        return;
      }
    })




  }


    paginateNow(currpage:number){
    this.filter()
    this.pageinfo.totalEntity=this.filteredPaginatedData.length;
    this.pageinfo.perpageentity=this.pageinfo.perpageentity;
    this.pageinfo.pagepossibe=Math.ceil(this.pageinfo.totalEntity/this.pageinfo.perpageentity)
    if(currpage>this.pageinfo.pagepossibe){
      currpage=this.pageinfo.pagepossibe;
    }
    this.pageinfo.currentPage=currpage;
    
    this.filteredPaginatedData=this.filteredPaginatedData.slice((this.pageinfo.currentPage-1)*(this.pageinfo.perpageentity),
    (this.pageinfo.currentPage-1)*(this.pageinfo.perpageentity)+this.pageinfo.perpageentity)
    if(this.pageinfo.currentPage==0)this.pageinfo.currentPage=1;
  }


  filter(){
    if(this.filteringinfo.category===""){
      this.filteredPaginatedData=this.mainArray;
    }else{
      this.filteredPaginatedData=this.mainArray.filter(x=>x.categoryName===this.filteringinfo.category);
    }
    if(this.filteringinfo.serched===""||this.filteringinfo.filter===""){
      return;
    }
    switch(this.filteringinfo.filter){
     case "0": {this.filteredPaginatedData=this.filteredPaginatedData.filter(x=>x.productName.toUpperCase().startsWith(this.filteringinfo.serched.trim().toUpperCase()));break;}
     
     case "1": {this.filteredPaginatedData=this.filteredPaginatedData.filter(x=>x.productCompany.toUpperCase().startsWith(this.filteringinfo.serched.trim().toUpperCase()));break;}
    }
  }


  private convertCategoryToTableFormat(arr:CategorySummary[]){
    let cat:string[]=[];
    arr.forEach(e=>{
      cat.push(e.categoryName)
    });
    return cat;
  }


  qtyget(data:any){
    this.qty=data;
  }

  BlueButtonClick(data :any){
    this.loading=true;
    this.cartService.addToCart(data,this.qty).subscribe({
      next:()=>{
        this.toastr.success("Added To Cart Successfully Suceessfully");
        this.loading=false;
      },
      error:err=>{
        this.toastr.error(err.error.message);
        this.loading=false;
      }
    })

  }
  
  RedButtonClick(data :any){
    this.loading=true;
    this.cartService.requestProduct(data,this.qty).subscribe({
      next:()=>{
        this.toastr.success("Request Sent Suceessfully");
        this.loading=false;
      },
      error:err=>{
        this.toastr.error(err.error.message);
        this.loading=false;
      }
    })
  }

}
