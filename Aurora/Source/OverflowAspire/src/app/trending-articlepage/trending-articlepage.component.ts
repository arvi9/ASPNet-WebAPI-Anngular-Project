import { Component, OnInit } from '@angular/core';
import { application } from 'Models/Application';

@Component({
  selector: 'app-trending-articlepage',
  templateUrl: './trending-articlepage.component.html',
  styleUrls: ['./trending-articlepage.component.css']
})
export class TrendingArticlepageComponent implements OnInit {
  url: string = `${application.URL}/Article/GetTrendingArticles?Range=0`;
  constructor() { }

  ngOnInit(): void {
  }

}
