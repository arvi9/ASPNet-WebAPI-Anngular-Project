import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Toaster } from 'ngx-toast-notifications';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-trending-queriespage',
  templateUrl: './trending-queriespage.component.html',
  styleUrls: ['./trending-queriespage.component.css']
})


export class TrendingQueriespageComponent implements OnInit {
  url: string = "trendingQueries";
  constructor(private route: Router, private toaster: Toaster) { }

  //Show trending articles.
  ngOnInit(): void {

    if (AuthService.GetData("token") == null) {
      this.toaster.open({ text: 'Your Session has been Expired', position: 'top-center', type: 'warning' })
      this.route.navigateByUrl("")
    }
  }
}



