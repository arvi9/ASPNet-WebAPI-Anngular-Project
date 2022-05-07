import { Component, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Query } from 'Models/Query';

@Component({
  selector: 'app-latest-queries',
  templateUrl: './latest-queries.component.html',
  styleUrls: ['./latest-queries.component.css']
})
export class LatestQueriesComponent implements OnInit {

  
   ngOnInit(): void {
     
   }
 
 
 
   
    public data: Query[] = [
     
 
   ];
 
  
 
 
 }