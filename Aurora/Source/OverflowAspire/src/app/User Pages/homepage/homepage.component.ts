import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Article } from 'Models/Article';
import { Query } from 'Models/Query';
import { AuthService } from 'src/app/Services/auth.service';
import { ConnectionService } from 'src/app/Services/connection.service';

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
// Get homepage and show latest articles,trending articles,trending queries,latest queries.
export class HomepageComponent implements OnInit {
  @Input() ShowStatus: boolean = true;
  constructor(private connection:ConnectionService,private route:Router) { }
  ngOnInit(): void {
    if(AuthService.GetData("token")==null) this.route.navigateByUrl("")
    this.connection.GetHomePage()
      .subscribe({next:(data: HomePage) => {
        this.data.latestArticles=data.latestArticles.slice(0,3)
        this.data.trendingArticles=data.trendingArticles.slice(0,3)
        this.data.trendingQueries=data.trendingQueries.slice(0,3)
        this.data.latestQueries=data.latestQueries.slice(0,3)
      }});
    }
      public data: HomePage = new HomePage();
    
}
