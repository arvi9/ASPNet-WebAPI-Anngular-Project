import { Template } from '@angular/compiler/src/render3/r3_ast';
import { Component, OnInit,Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Query } from 'Models/Query';

@Component({
  selector: 'app-query-card',
  templateUrl: './query-card.component.html',
  styleUrls: ['./query-card.component.css']


})
export class QueryCardComponent implements OnInit {

 @Input() Querysrc: string ="";
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


