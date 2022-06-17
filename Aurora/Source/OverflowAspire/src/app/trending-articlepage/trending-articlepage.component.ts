import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { application } from 'Models/Application';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-trending-articlepage',
  templateUrl: './trending-articlepage.component.html',
  styleUrls: ['./trending-articlepage.component.css']
})
export class TrendingArticlepageComponent implements OnInit {
  url: string = `${application.URL}/Article/GetTrendingArticles?Range=0`;
  
  constructor(private http: HttpClient,private route:Router) { }
  ngOnInit(): void {
    if(AuthService.GetData("token")==null) this.route.navigateByUrl("")
  }

}
