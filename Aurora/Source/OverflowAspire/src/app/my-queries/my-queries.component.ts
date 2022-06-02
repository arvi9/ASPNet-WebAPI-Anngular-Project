import { Component,Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Query } from 'Models/Query';
import { application } from 'Models/Application';

@Component({
  selector: 'app-my-queries',
  templateUrl: './my-queries.component.html',
  styleUrls: ['./my-queries.component.css']
})
export class MyQueriesComponent implements OnInit {

 
  url: string = `${application.URL}/Query/GetQueriesByUserId?UserId=2`;

 
  ngOnInit(): void {
    
      };
}

