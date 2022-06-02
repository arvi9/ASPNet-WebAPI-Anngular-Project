import { Component, Input, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Article } from 'Models/Article';
import { AuthService } from '../auth.service';
import { application } from 'Models/Application';
@Component({
  selector: 'app-latest-articlepage',
  templateUrl: './latest-articlepage.component.html',
  styleUrls: ['./latest-articlepage.component.css']
})
export class LatestArticlepageComponent implements OnInit {

  url: string = `${application.URL}/Article/GetLatestArticles`;


  constructor(private http: HttpClient) { }
  ngOnInit(): void {


  } public data: Article[] = [


  ];

}