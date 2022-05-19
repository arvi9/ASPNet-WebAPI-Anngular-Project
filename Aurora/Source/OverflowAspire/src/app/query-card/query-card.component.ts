import { Template } from '@angular/compiler/src/render3/r3_ast';
import { Component, OnInit,Input } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Query } from 'Models/Query';

import { AuthService } from '../auth.service';

@Component({
  selector: 'app-query-card',
  templateUrl: './query-card.component.html',
  styleUrls: ['./query-card.component.css']


})
export class QueryCardComponent implements OnInit {

 @Input() Querysrc: string ="";
  totalLength: any;
  page: number = 1;
  searchTitle=""

  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    this.http
      .get<any>(this.Querysrc, {headers: headers} )
      .subscribe((data) => {
        this.data = data;
        this.totalLength = data.length;

      });
  }




   public data: Query[] = [


  ];




}


