import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CategorySummary } from 'src/app/Models/category-summary';
import { CartRequestService } from 'src/app/Services/cartrequest.service';
import { ProductsService } from 'src/app/Services/products.service';
import { Request } from 'src/app/Models/request';

@Component({
  selector: 'app-my-cart',
  templateUrl: './my-cart.component.html',
  styleUrls: ['./my-cart.component.css']
})
export class MyCartComponent implements OnInit {

 pageinfo={
    totalEntity:0,
    currentPage:1,
    perpageentity:4,
    pagepossibe:0
  }
  public cartPrice=0;

  filteredPaginatedData:Request[]=[];

  loading :boolean=false;

  filteringinfo={
    serched:"",
    filter:"",
    category:""
  }

  qty:number=0;

  mainArray:Request[]=[];
  categoryArray:string[]=[];
  SerchableArrayIndex:number[]=[0,1]
  TitleArray:string[]=["Product Name","Company"]

  
  constructor(private cartService:CartRequestService,
    private productService:ProductsService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
    this.loading=true;
    this.productService.getAllCAtegories().subscribe({
      next:data=>{
        this.categoryArray=this.convertCategoryToTableFormat(data);
        this.loading=false;
      },
      error:err=>{
        this.toastr.error(err.error.message)
        this.loading=false;
        return;
      }
    })

    
    this.loading=true;
    this.cartService.getMyCart().subscribe({
      next:data=>{
        this.mainArray=data;
        this.mainArray.forEach(x=>{this.cartPrice+=x.count*x.price});
        this.paginateNow(this.pageinfo.currentPage)
      
        this.loading=false;
      },
      error:err=>{
        this.toastr.error(err.error.message)
        this.loading=false;
        return;
      }
    })
  }

  openModal(){
    document.getElementById("openModalButton")?.click();
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



  convertTotableFormat(arr:Request[]){
    let temp:(string|number)[][]=[]
    this.cartPrice=0;
    arr.forEach(el=>{
      temp.push([el.requestId,el.imageurl,el.productName,el.productCompany,el.categoryName]);
      this.cartPrice+=el.price*el.count
    })
    return temp;
  }

  RedButtonClick(id:any){
    this.cartService.deleteCartItem(id).subscribe({
      next:data=>{
        let index=this.mainArray.findIndex(x=>x.requestId===id)
        this.cartPrice-=this.mainArray[index].count*this.mainArray[index].price;
        this.mainArray.splice(index,1);
        this.mainArray;
        this.paginateNow(this.pageinfo.currentPage);
        this.toastr.success("cart item deleted successfully")
      },
      error:err=>{
        this.toastr.error(err.error.message)
      }
    })
  }

  BlueButtonClick(id:any){
    
    this.cartService.requestTheCartItem(id).subscribe({
      next:data=>{
        let index=this.mainArray.findIndex(x=>x.requestId===id)
        this.mainArray.splice(index,1);
        this.paginateNow(this.pageinfo.currentPage);
        this.toastr.success("cart item requested successfully")
      },
      error:err=>{
        this.toastr.error(err.error.message)
      }
    })
  }

  convertCategoryToTableFormat(arr:CategorySummary[]){
    let cat:string[]=[]
    arr.forEach(element => {
      cat.push(element.categoryName)
    });
    return cat;
  }


  cartAmtChange(value:any){
    this.loading=true;
    this.cartService.updateCartItemAtm(value.count,value.id).subscribe({
      next: data=> {
        this.loading=false;
        this.toastr.success("qty updated successfully");
      },
      error:err=>{
        this.loading=false;
        this.toastr.error(err.error.message);
      }
    })
  }


}
