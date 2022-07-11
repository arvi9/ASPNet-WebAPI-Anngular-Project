import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-trending-queriespage',
  templateUrl: './trending-queriespage.component.html',
  styleUrls: ['./trending-queriespage.component.css']
})


export class TrendingQueriespageComponent implements OnInit {
  url: string = "trendingQueries";
  constructor(private route: Router) { }

  //Show trending articles.
  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.route.navigateByUrl("")
  }
}



