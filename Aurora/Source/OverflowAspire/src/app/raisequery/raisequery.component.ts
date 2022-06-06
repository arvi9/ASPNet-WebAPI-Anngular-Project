import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Query } from '../query'
import { application } from 'Models/Application';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
@Component({
  selector: 'app-raisequery',
  templateUrl: './raisequery.component.html',
  styleUrls: ['../specificquery/specificquery.component.css']
})
export class RaisequeryComponent implements OnInit {

  constructor(private http: HttpClient,private routing : Router) { }
  IsLoading:boolean=false;
  query: any = {
    queryId:0,
    title:'',
    content:'',
    code:'',
    isSolved: false,
    isActive: true,
    createdBy: 1,
    createdOn: Date.now,
    updatedBy:0,
    updatedOn:null,
    queryComments:null,
    user:null


  }
  RaiseQuery() {
    this.IsLoading=true;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    console.log(this.query)
    this.http.post<any>(`${application.URL}/Query/CreateQuery`, this.query, { headers: headers })
      .subscribe((data) => {
        console.log(data);
      });
       this.routing.navigateByUrl("MyQueries");
    }

  ngOnInit(): void {



  }}

