import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";
import { HttpModule } from '@angular/http';


import { AppComponent } from './app.component';
import { Login } from "./login/login.component"

import { DataService } from "./shared/dataService"
import { RouterModule } from "@angular/router";
import { FormsModule } from "@angular/forms";
//import { DataTableModule, InputTextareaModule, PanelModule, DropdownModule } from 'primeng';


let routes = [
    { path: "login", component: Login }
];

@NgModule({
  declarations: [
      AppComponent,
      Login
  ],
  imports: [
      BrowserModule,
      HttpClientModule,
      HttpModule,
      FormsModule,
      RouterModule.forRoot(routes, {
          useHash: true,
          enableTracing: false
      })
  ],
  providers: [
      DataService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
