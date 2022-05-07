import { Component, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Query } from 'Models/Query';

@Component({
  selector: 'app-latest-queries',
  templateUrl: './latest-queries.component.html',
  styleUrls: ['./latest-queries.component.css']
})
export class LatestQueriesComponent implements OnInit {

  @Input() Querysrc: string ="https://localhost:7197/Query/GetLatestQueries";
   totalLength: any;
   page: number = 1;
   
 
   constructor(private http: HttpClient) { }
   ngOnInit(): void {
     this.http
       .get<any>(this.Querysrc)
       .subscribe((data) => {
         this.data = data;
         this.totalLength = data.length;
        
       });
   }
 
 
 
   
    public data: Query[] = [
     
 
   ];
 
  
 
 
 }