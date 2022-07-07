import { Component,OnInit } from '@angular/core';
import { Query } from 'Models/Query';
import { AuthService } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-latest-queries',
  templateUrl: './latest-queries.component.html',
  styleUrls: ['./latest-queries.component.css']
})

//Show latest queries page.
export class LatestQueriesComponent implements OnInit {

   url: string = "latestQueries";
   constructor(private route:Router) { }
   ngOnInit(): void {
     if(AuthService.GetData("token")==null) this.route.navigateByUrl("")
     
   }  
    public data: Query[] = [];
 }