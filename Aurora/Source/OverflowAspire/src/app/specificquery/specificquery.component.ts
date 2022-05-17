
import { Component, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Query } from 'Models/Query';
import{QueryComment} from 'Models/Query';

import { ActivatedRoute, Router } from '@angular/router';


declare type myarray = Array<{ content: string, coding: string, name: string }>
@Component({
  selector: 'app-specificquery',
  templateUrl: './specificquery.component.html',
  styleUrls: ['./specificquery.component.css']
})
export class SpecificqueryComponent implements OnInit {
  queryId: number = 0
 
  Query: any = {
   CommentId:0,
   comment: '',
   datetime:Date.now,
   userId: 1,
   queryId:1,
   createdBy:1,
   createdOn:Date.now,
   
    
  }
  
  constructor(private route: ActivatedRoute, private http: HttpClient) { }
   
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.queryId = params['queryId'];
    console.log(this.queryId)
    this.http
      .get<any>(`https://localhost:7197/Query/GetQuery?QueryId=${this.queryId}`)
      .subscribe((data) => {
        this.data = data;
        console.log(data);
      });
    });
  }
  
   


 public data:Query =new Query();



 
 displayStyle = "none";
 openpopup() {
   this.displayStyle = "block";
 }
 
 closePopup() {
   this.displayStyle = "none";
 }
 PostComment(){  
  const headers = { 'content-type': 'application/json' }
  console.log(this.Query)
    this.http.post<any>('https://localhost:7197/Query/CreateComment', this.Query, { headers: headers })
      .subscribe((data) => {

        console.log(data)

      });
 }
 
    
}