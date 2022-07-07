import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-trending-queriespage',
  templateUrl: './trending-queriespage.component.html',
  styleUrls: ['./trending-queriespage.component.css']
})

  //Show trending articles.
export class TrendingQueriespageComponent implements OnInit {
  url: string = "trendingQueries";
  constructor(private http: HttpClient,private route:Router) { }
  ngOnInit(): void {
    if(AuthService.GetData("token")==null) this.route.navigateByUrl("")
  }
    
  }



