
import { Component, Input, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Query } from 'Models/Query';
import{QueryComment} from 'Models/Query';

import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { application } from 'Models/Application';


declare type myarray = Array<{ content: string, coding: string, name: string }>
@Component({
  selector: 'app-specificquery',
  templateUrl: './specificquery.component.html',
  styleUrls: ['./specificquery.component.css']
})
export class SpecificqueryComponent implements OnInit {
  queryDetails:any= this.route.params.subscribe(params => {
    this.queryId = params['queryIdId'];
    console.log(this.queryId)
  });
  queryId:number=this.queryDetails;
 
  Query: any = {
   CommentId:0,
   comment: '',
   datetime:Date.now,
   userId: 1,
   queryId:0,
   createdBy:10,
   createdOn:Date.now,
   
    
  }
  
  constructor(private routing:Router,private route: ActivatedRoute, private http: HttpClient) { }
   
  ngOnInit(): void {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    this.route.params.subscribe(params => {
      this.queryId = params['queryId'];
    console.log(this.queryId)

    this.http
      .get<any>(`${application.URL}/Query/GetQuery?QueryId=${this.queryId}`,{headers:headers})
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
  const headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${AuthService.GetData("token")}`
  })
  console.log(AuthService.GetData("token"))
  console.log(this.Query)
  console.log(this.queryId)
    this.Query.queryId=this.queryId;
    this.http.post<any>('https://localhost:7197/Query/CreateComment', this.Query, { headers: headers })
      .subscribe((data) => {

        console.log(data)

      });
      this.routing.navigateByUrl("/Home");
 }
 
    
}