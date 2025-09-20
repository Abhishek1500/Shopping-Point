import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductAndCategoryComponent } from './product-and-category.component';
import { ProductsService } from 'src/app/Services/products.service';
import { Observable, observable, of, throwError } from 'rxjs';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { By } from '@angular/platform-browser';

fdescribe('ProductAndCategoryComponent', () => {
  let component: ProductAndCategoryComponent;
  let fixture: ComponentFixture<ProductAndCategoryComponent>;
  let productService:ProductsService;
  let productServiceSpy:jasmine.Spy;
  let toastrService: ToastrService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductAndCategoryComponent ],
      imports:[HttpClientModule,ToastrModule.forRoot(),FormsModule],
      providers:[ProductsService,ToastrService]
    })
    .compileComponents();

    


    fixture = TestBed.createComponent(ProductAndCategoryComponent);
    component = fixture.componentInstance;
    productService=TestBed.inject(ProductsService);
    toastrService=TestBed.inject(ToastrService);

   
    fixture.detectChanges();
  });

  it('Product Category component create', () => {
    fixture.detectChanges();
    expect(component).toBeTruthy();
  });


  it('ProductService is called on initialization',()=>{

     productServiceSpy=spyOn(productService,'getAllCAtegories').and.returnValue(of([{
      id:1,
      categoryName:"cat1",
      products:[]
    },
    {
      id:2,
      categoryName:"cat2",
      products:[]
    }
  ]))
    component.ngOnInit();
    expect(productServiceSpy).toHaveBeenCalled();
  })


  it("productServiceGivingData",()=>{

    productServiceSpy=spyOn(productService,'getAllCAtegories').and.returnValue(of([{
      id:1,
      categoryName:"cat1",
      products:[]
    },
    {
      id:2,
      categoryName:"cat2",
      products:[]
    }
  ]))
    
    productService.getAllCAtegories().subscribe(data=>{
      expect(data.length).toBeGreaterThan(0);
    })
  })


  it('testing the adding button of product if it calls the function or not',()=>{
    fixture.detectChanges();
    productServiceSpy=spyOn(productService,'addNewProduct').and.returnValue(of({
    id:0,
    productName:"",
    productCompany:"",
    dateOfManuFacture:new Date,
    quantity:0,
    description:"",
    imageurl:"",
    price:0,
    categoryName:"",
    }))
    const addButton=document.getElementById('addbutton');
    console.log(addButton);
    component.addProduct();
    expect(productServiceSpy).toHaveBeenCalled();
  })
});
