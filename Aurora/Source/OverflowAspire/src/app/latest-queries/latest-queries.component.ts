import { Component, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Query } from 'Models/Query';
import { application } from 'Models/Application';

@Component({
  selector: 'app-latest-queries',
  templateUrl: './latest-queries.component.html',
  styleUrls: ['./latest-queries.component.css']
})
export class LatestQueriesComponent implements OnInit {

   url: string = `${application.URL}/Query/GetLatestQueries`;
   ngOnInit(): void {
     
   }
 
 
 
   
    public data: Query[] = [
     
 
   ];
 
  
 
 
 }