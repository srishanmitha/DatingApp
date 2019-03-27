import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { ValueComponent } from './value/value.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import {  ErrorInterceptorProvider} from './services/error.interceptor';
import { AlertifyService } from './services/alertify.service';
import { BsDropdownModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { MemberListComponent } from './member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthService } from './services/auth.service';
import { appRoutes } from './routes';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      ValueComponent,
      HomeComponent,
      RegisterComponent,
      MemberListComponent,
      ListsComponent,
      MessagesComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      RouterModule.forRoot(appRoutes)
   ],
   providers: [
      AppComponent,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
