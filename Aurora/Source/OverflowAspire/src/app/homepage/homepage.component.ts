import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { application } from 'Models/Application';
import { Article } from 'Models/Article';
import { Query } from 'Models/Query';
import { AuthService } from '../auth.service';

export class HomePage{
  trendingArticles:Article[]=[];
  latestArticles:Article[]=[];
  trendingQueries:Query[]=[];
  latestQueries:Query[]=[];
  }

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {

  public UrlString:string=`${application.URL}/Dashboard/GetHomePage`
  
  constructor(private http: HttpClient,private route:Router) { }
  ngOnInit(): void {
    if(AuthService.GetData("token")==null) this.route.navigateByUrl("")
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    this.http
      .get<any>(this.UrlString,{headers:headers})
      .subscribe({next:(data) => {
        this.data.latestArticles=data.latestArticles.slice(0,3)
        this.data.trendingArticles=data.trendingArticles.slice(0,3)
        this.data.trendingQueries=data.trendingQueries.slice(0,3)
        this.data.latestQueries=data.latestQueries.slice(0,3)
      
      console.log(data)
       
      }});
    
    }
      public data: HomePage = new HomePage();

  
    
}
