import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CartRequestService } from 'src/app/Services/cartrequest.service';
import { ProductsService } from 'src/app/Services/products.service';
import { Request } from 'src/app/Models/request';
import { CategorySummary } from 'src/app/Models/category-summary';
import { forkJoin } from 'rxjs';


@Component({
  selector: 'app-my-requests',
  templateUrl: './my-requests.component.html',
  styleUrls: ['./my-requests.component.css']
})
export class MyRequestsComponent implements OnInit {


  private allcartItem:Request[]=[];
  public allCategory:CategorySummary[]=[];
  public categories:string[]=[];
  public tableFormatCart:(string|Number|Date|null)[][]=[];
  public cartPrice=0;
  public loading=false;

  constructor(private cartService:CartRequestService,
    private productService:ProductsService,
    private toastr:ToastrService,) { }

  ngOnInit(): void {

    
    this.loading=true;
    this.cartService.getMyRequests().subscribe({
      next:data=>{
        this.allcartItem=data;
        this.tableFormatCart=this.convertTotableFormat(this.allcartItem);
        this.loading=false;
      },
      error:err=>{
        this.toastr.error(err.error.message)
        this.loading=false;
        return;
      }
    })
    this.loading=true;
    this.productService.getAllCAtegories().subscribe({
      next:data=>{
        this.allCategory=data;
        this.categories=this.convertCategoryToTableFormat(this.allCategory);
        this.loading=false;

      },
      error:err=>{
        this.toastr.error(err.error.message)
        this.loading=false;
        return;
      }
    })
  }

  convertTotableFormat(arr:Request[]){
    let temp:(string|number|Date|null)[][]=[]
    this.cartPrice=0;
    const options: Intl.DateTimeFormatOptions = {
    year: 'numeric',
    month: 'long',
    day: 'numeric'
    };
    arr.forEach(el=>{
      temp.push([el.requestId,el.imageurl,el.productName,el.productCompany,el.price,el.count,(el.lastUpdate+"").split("T")[0],el.categoryName]);
      this.cartPrice+=el.price
    })
    return temp;
  }


  convertCategoryToTableFormat(arr:CategorySummary[]){
    let cat:string[]=[]
    
    arr.forEach(element => {
      cat.push(element.categoryName)
    });
    return cat;
  }

}
