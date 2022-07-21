import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Article } from 'Models/Article';
import { Query } from 'Models/Query';
import { NgxSpinnerService } from 'ngx-spinner';
import { Toaster } from 'ngx-toast-notifications';
import { AuthService } from 'src/app/Services/auth.service';
import { ConnectionService } from 'src/app/Services/connection.service';

export class HomePage {
  trendingArticles: Article[] = [];
  latestArticles: Article[] = [];
  trendingQueries: Query[] = [];
  latestQueries: Query[] = [];
}

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})

// Get homepage and show latest articles,trending articles,trending queries,latest queries.
export class HomepageComponent implements OnInit {
  error:any
  isSpinner = true;
  @Input() ShowStatus: boolean = true;
  public data: HomePage = new HomePage();
  constructor(private connection: ConnectionService, private route: Router, private toaster: Toaster,private spinner: NgxSpinnerService) { }

  ngOnInit(): void {

    if (AuthService.GetData("token") == null) {
      this.toaster.open({ text: 'Your Session has been Expired', position: 'top-center', type: 'warning' })
      this.route.navigateByUrl("")
    }
    this.spinner.show();
    this.connection.GetHomePage()
      .subscribe({
        next: (data: HomePage) => {
          this.data.latestArticles = data.latestArticles
          this.data.trendingArticles = data.trendingArticles
          this.data.trendingQueries = data.trendingQueries
          this.data.latestQueries = data.latestQueries
        },
        error: (error:any) => this.error = error.error.message,
        complete: () => {
          this.isSpinner = false;
          this.spinner.hide();
        }
      });
  }
}
