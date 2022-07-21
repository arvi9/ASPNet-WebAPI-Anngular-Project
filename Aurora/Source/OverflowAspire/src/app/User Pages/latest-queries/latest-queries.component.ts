import { Component,OnInit } from '@angular/core';
import { AuthService } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';
import { Toaster } from 'ngx-toast-notifications';

@Component({
  selector: 'app-latest-queries',
  templateUrl: './latest-queries.component.html',
  styleUrls: ['./latest-queries.component.css']
})

//Show latest queries page.
export class LatestQueriesComponent implements OnInit {
   url: string = "latestQueries";
   constructor(private routes: Router, private toaster: Toaster,) { }
   
   ngOnInit(): void {
    if (AuthService.GetData("token") == null) {
      this.toaster.open({ text: 'Your Session has been Expired', position: 'top-center', type: 'warning' })
      this.routes.navigateByUrl("")
    }
   }  
 }