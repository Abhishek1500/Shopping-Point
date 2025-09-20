import { Component, OnInit , Input, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { ProductsService } from '../Services/products.service';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit,OnChanges {

  @Output() parentfunction:EventEmitter<any>=new EventEmitter()

  @Input() mainArray:any=[];
  @Input() shouldShow:any;
  @Input() TitleArray:any;
  @Input() categoryArray:any;
  @Input() TableTitle:any;
  @Input() SerchableArrayIndex:any;
  @Input() TextEmpty:any;

   pageinfo={
    totalEntity:0,
    currentPage:1,
    perpageentity:4,
    pagepossibe:0
  }

  filteredPaginatedData:any[]=[];

  loading :boolean=false;

  filteringinfo={
    serched:"",
    filter:"",
    category:""
  }



  constructor(private product:ProductsService) { }

  ngOnInit(): void {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(!this.categoryArray.includes(this.filteringinfo.category)){
      this.filteringinfo.category=""
    }
    this.paginateNow(this.pageinfo.currentPage)
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
      this.filteredPaginatedData=this.mainArray.filter((x:any)=>x[x.length-1]===this.filteringinfo.category);
    }
    if(this.filteringinfo.serched===""||this.filteringinfo.filter===""){
      return;
    }
    this.filteredPaginatedData=this.filteredPaginatedData.filter((x:any)=>x[Number(this.filteringinfo.filter)+1].toUpperCase().startsWith(this.filteringinfo.serched.trim().toUpperCase()));
  }



}
