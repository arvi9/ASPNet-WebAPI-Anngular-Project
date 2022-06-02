import { Component, OnInit } from '@angular/core';
import { application } from 'Models/Application';

@Component({
  selector: 'app-trending-queriespage',
  templateUrl: './trending-queriespage.component.html',
  styleUrls: ['./trending-queriespage.component.css']
})
export class TrendingQueriespageComponent implements OnInit {
  url: string = `${application.URL}/Query/GetTrendingQueries`;
  constructor() { }

  ngOnInit(): void {
    
  }



}
