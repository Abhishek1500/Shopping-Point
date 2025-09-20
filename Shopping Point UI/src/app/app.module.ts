import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ToastContainerModule, ToastrModule, ToastrService } from 'ngx-toastr';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthInterceptorService } from './Services/auth-interceptor.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NavbarComponent } from './navbar/navbar.component';
import { ProductCardModule } from './product-card/product-card.module';
import { PageNotfoundComponent } from './page-notfound/page-notfound.component';
import { ServerErrorComponent } from './server-error/server-error.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    PageNotfoundComponent,
    ServerErrorComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    NgbModule,
    ProductCardModule,
    ToastContainerModule
  ],
  providers: [
    {provide:HTTP_INTERCEPTORS,useClass:AuthInterceptorService,multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
