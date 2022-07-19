import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Toaster } from 'ngx-toast-notifications';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-trending-articlepage',
  templateUrl: './trending-articlepage.component.html',
  styleUrls: ['./trending-articlepage.component.css']
})

export class TrendingArticlepageComponent implements OnInit {
  url: string = 'trendingArticles';
  constructor(private http: HttpClient, private route: Router,private toaster: Toaster) { }

  //Show trending Articles.
  ngOnInit(): void {
    
 if (AuthService.GetData("token") == null) {
this.toaster.open({ text: 'Your Session has been Expired', position: 'top-center', type: 'warning' })
      this.route.navigateByUrl("")
    }
  }

}
