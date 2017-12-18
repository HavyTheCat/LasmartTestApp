import { Component } from '@angular/core';
import { DataService } from "./shared/dataService"
import { Router } from "@angular/router"


@Component({
  selector: 'app-root',
  templateUrl: "app.component.html",
  styles: []
})
export class AppComponent {
    constructor(private data: DataService, private router: Router) { }
    
    title = 'app';
   

    onClick() {
       
    }
}
