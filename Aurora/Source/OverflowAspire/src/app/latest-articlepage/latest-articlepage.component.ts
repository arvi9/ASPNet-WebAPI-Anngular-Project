import { Component, Input, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Article } from 'Models/Article';
import { AuthService } from '../auth.service';
import { application } from 'Models/Application';
import { Router } from '@angular/router';
@Component({
  selector: 'app-latest-articlepage',
  templateUrl: './latest-articlepage.component.html',
  styleUrls: ['./latest-articlepage.component.css']
})
export class LatestArticlepageComponent implements OnInit {

  url: string = `${application.URL}/Article/GetLatestArticles`;

  constructor(private http: HttpClient,private route:Router) { }
  ngOnInit(): void {
    if(AuthService.GetData("token")==null) this.route.navigateByUrl("")


  } public data: Article[] = [


  ];

}