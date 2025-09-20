import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Request } from '../Models/request';
import { Product } from '../Models/Product';
import { Toast, ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {

  @Input() isCart:boolean=false
  @Input() request :Request=new Request();
  @Input() product:Product= new Product();
  @Input() RedButtonString="";
  @Input() BlueButtonString="";
  @Output() RedButtonClick:EventEmitter<Number>=new EventEmitter();
  @Output() BlueButtonClick:EventEmitter<Number>=new EventEmitter();
  @Output() SendQuantity:EventEmitter<Number>=new EventEmitter();
  @Output() OnValueChange:EventEmitter<any>=new EventEmitter();

  private value:number=0;

  public qtyinput=1;

  constructor(private toastrServe:ToastrService) { }

  ngOnInit(): void {
    this.value=this.qtyinput=this.request.count==0?1:this.request.count;
  }

  onBlueClick(){
    this.SendQuantity.emit(this.qtyinput);
    this.BlueButtonClick.emit(this.request.requestId+this.product.id);
  }
  
  onRedClick(){
    this.SendQuantity.emit(this.qtyinput);
    this.RedButtonClick.emit(this.request.requestId+this.product.id)
  }

  onBulringInputField(){
    if(!this.isCart){
      return;
    }
    if(this.qtyinput<=0){
      this.qtyinput=this.value;
      this.toastrServe.error("quantity should be greater than 0");
    }
  }


}
