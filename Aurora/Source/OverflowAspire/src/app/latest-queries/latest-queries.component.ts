import { Component, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Query } from 'Models/Query';
import { application } from 'Models/Application';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-latest-queries',
  templateUrl: './latest-queries.component.html',
  styleUrls: ['./latest-queries.component.css']
})
export class LatestQueriesComponent implements OnInit {

   url: string = `${application.URL}/Query/GetLatestQueries`;
   constructor(private http: HttpClient,private route:Router) { }
   ngOnInit(): void {
     if(AuthService.GetData("token")==null) this.route.navigateByUrl("")
     
   }  
    public data: Query[] = [];
 }