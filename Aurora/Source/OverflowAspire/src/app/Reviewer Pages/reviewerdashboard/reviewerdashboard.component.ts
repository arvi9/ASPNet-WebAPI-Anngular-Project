import { Component, OnInit, Input } from '@angular/core';
import { Dashboard } from 'Models/Dashboard';
import { AuthService } from 'src/app/Services/auth.service';
import { Chart } from 'chart.js';
import { Router } from '@angular/router';
import { ConnectionService } from 'src/app/Services/connection.service';
import { Toaster } from 'ngx-toast-notifications';

@Component({
  selector: 'app-reviewerdashboard',
  templateUrl: './reviewerdashboard.component.html',
  styleUrls: ['./reviewerdashboard.component.css']
})

export class ReviewerdashboardComponent implements OnInit {
  piedata: any = {
    articleCounts: 0,
  }
  constructor(private route: Router, private connection: ConnectionService,private toaster: Toaster) { }
  articles:any;

  ngOnInit(): void {
    
 if (AuthService.GetData("token") == null) {
this.toaster.open({ text: 'Your Session has been Expired', position: 'top-center', type: 'warning' })
      this.route.navigateByUrl("")
    }
    if (!AuthService.IsReviewer()) {
      this.route.navigateByUrl("")
    }
    this.connection.GetReviewerDashboard()
      .subscribe({
        next: (data: any) => {
          this.piedata = data;
          this.articles=data.articleCounts.toBeReviewedArticles+data.articleCounts.underReviewArticles
          var names = ['Articles to be reviewed', 'Article published'];
          var details = [];
          details.push( this.articles);
          details.push(data.articleCounts.articlesPublished);
          new Chart('piechart', {
            type: 'pie',
            data: {
              labels: names,
              datasets: [{
                data: details,
              }]
            },
            options: {
              plugins: {
                legend: {
                  position: 'top',
                  display: true,
                },
              },
            }
          });
        }
      });
  }
  
}
