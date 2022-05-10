import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Query } from '../query'

@Component({
  selector: 'app-raisequery',
  templateUrl: './raisequery.component.html',
  styleUrls: ['../specificquery/specificquery.component.css']
})
export class RaisequeryComponent implements OnInit {

  constructor(private http: HttpClient) { }
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
    this.http.post<any>('https://localhost:7197/Query/CreateQuery', this.query, { headers: headers })
      .subscribe((data) => {

        console.log(data)

      });
  }

  ngOnInit(): void {
   

    
  }}

