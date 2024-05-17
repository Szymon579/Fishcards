import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NaviComponent } from './navi/navi.component';
import { CardsComponent } from './cards/cards.component';
import { CollectionsComponent } from './collections/collections.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgFor } from '@angular/common';
import { TopBarComponent } from './top-bar/top-bar.component';
import { LoginComponent } from './login/login.component';
import { AuthInterceptor } from './auth.interceptor';
import { CollectionShareComponent } from './collection-share/collection-share.component';

@NgModule({
  declarations: [
    AppComponent,
    NaviComponent,
    CardsComponent,
    CollectionsComponent,
    TopBarComponent,
    LoginComponent,
    CollectionShareComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgFor,
    HttpClientModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
