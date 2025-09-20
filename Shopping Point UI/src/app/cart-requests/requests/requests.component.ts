import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { CategorySummary } from 'src/app/Models/category-summary';
import { CartRequestService } from 'src/app/Services/cartrequest.service';
import { ProductsService } from 'src/app/Services/products.service';
import { PendinRequest } from 'src/app/Models/pendingrequests';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.css']
})
export class RequestsComponent implements OnInit {

  parentfunction(requestId:any){
    this.requestId=requestId;
    this.madeuparea();
    this.openModal();
  }

  public loading=false;

  public araeMaintainer={
    id:0,
    userEmail: "",
    userName:"",
    productName: "",
    productCompany: "",
    imageurl: "",
    price: 0,
    categoryName: "",
    status: "",
    count:0,
    lastUpdated:"",
    remark:""
  }

  private requestId=0;
  private allcartItem:PendinRequest[]=[];
  public allCategory:CategorySummary[]=[];
  public categories:string[]=[];
  public tableFormatCart:(string|Number|Date)[][]=[];

  
  constructor(private cartService:CartRequestService,
    private productService:ProductsService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
    
    this.loading=true;
    this.cartService.getRequests().subscribe({
      next:data=>{
        this.allcartItem=data;
        this.tableFormatCart=this.convertTotableFormat(data);
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

  openModal(){
    document.getElementById("openModalButton")?.click();
  }

  madeuparea(){
    let a=this.allcartItem.find(x=>x.id==this.requestId)
    if(a==null){
      return;
    }
    this.araeMaintainer.id=Number(a.id)
    this.araeMaintainer.productName=a.productName,
    this.araeMaintainer.productCompany=a.productCompany,
    this.araeMaintainer.userEmail=a.userEmail;
    this.araeMaintainer.userName=a.userName;
    this.araeMaintainer.imageurl=a.imageurl,
    this.araeMaintainer.price= Number(a.price),
    this.araeMaintainer.categoryName=a.categoryName,
    this.araeMaintainer.status= a.status,
    this.araeMaintainer.count= Number(a.count),
    this.araeMaintainer.lastUpdated= (""+a.lastUpdated).split("T")[0];
  }


  getAllRequest(){
    this.cartService.getRequests()
  }


  convertTotableFormat(arr:PendinRequest[]){
    let temp:(string|number)[][]=[]
    arr.forEach(el=>{
      temp.push([el.id,el.imageurl,el.productName,el.productCompany,el.userEmail,el.userName,el.count,el.categoryName]);
    })
    return temp;
  }

  approve(){
    this.loading=true
    this.cartService.repondToRequest({newStatus:"approved"},this.araeMaintainer.id).subscribe({
      next:data=>{
        let index=this.allcartItem.findIndex(x=>x.id===this.araeMaintainer.id)
        this.allcartItem.splice(index,1);
        this.tableFormatCart=this.convertTotableFormat(this.allcartItem);
        this.toastr.success("request approved successfully")
        this.loading=false
      },
      error:err=>{
        this.toastr.error(err.error.message)
        this.loading=false
      }
    })
  }

  reject(){
    this.loading=true
    this.cartService.repondToRequest({newStatus:"rejected",remark:this.araeMaintainer.remark},this.araeMaintainer.id).subscribe({
      next:data=>{
        let index=this.allcartItem.findIndex(x=>x.id===this.araeMaintainer.id)
        this.allcartItem.splice(index,1);
        this.tableFormatCart=this.convertTotableFormat(this.allcartItem);
        this.toastr.success("request Rejected successfully")
        this.loading=false
      },
      error:err=>{
        this.toastr.error(err.error.message)
        this.loading=false
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

}
