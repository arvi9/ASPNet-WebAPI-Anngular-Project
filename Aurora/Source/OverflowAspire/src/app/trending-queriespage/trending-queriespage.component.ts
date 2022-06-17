import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { application } from 'Models/Application';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-trending-queriespage',
  templateUrl: './trending-queriespage.component.html',
  styleUrls: ['./trending-queriespage.component.css']
})
export class TrendingQueriespageComponent implements OnInit {
  url: string = `${application.URL}/Query/GetTrendingQueries`;
  constructor(private http: HttpClient,private route:Router) { }
  ngOnInit(): void {
    if(AuthService.GetData("token")==null) this.route.navigateByUrl("")
  }
    
  }



