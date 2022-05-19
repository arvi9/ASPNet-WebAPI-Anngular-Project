import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { application } from 'Models/Application';
import { Article } from 'Models/Article';
import { Query } from 'Models/Query';

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
 
  
  
  

  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    this.http
      .get<any>(this.UrlString)
      .subscribe((data) => {
        this.data.latestArticles=data.latestArticles.slice(0,3)
        this.data.trendingArticles=data.trendingArticles.slice(0,3)
        this.data.trendingQueries=data.trendingQueries.slice(0,3)
        this.data.latestQueries=data.latestQueries.slice(0,3)
      
      console.log(data)
       
      });
    
    }
      public data: HomePage = new HomePage();

  
    
}
