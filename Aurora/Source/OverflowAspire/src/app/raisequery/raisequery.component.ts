import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Query } from '../query'
import { application } from 'Models/Application';
import { Router } from '@angular/router';
@Component({
  selector: 'app-raisequery',
  templateUrl: './raisequery.component.html',
  styleUrls: ['../specificquery/specificquery.component.css']
})
export class RaisequeryComponent implements OnInit {

  constructor(private http: HttpClient,private routing : Router) { }
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
    const headers = { 'content-type': 'application/json' }
    console.log(this.query)
    this.http.post<any>(`${application.URL}/Query/CreateQuery`, this.query, { headers: headers })
      .subscribe((data) => {

        console.log(data)

      });
      this.routing.navigateByUrl("/Home");
  }

  ngOnInit(): void {
   

    
  }}

