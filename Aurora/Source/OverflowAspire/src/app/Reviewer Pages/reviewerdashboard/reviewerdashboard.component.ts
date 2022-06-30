import { Component, OnInit, Input } from '@angular/core';
import { Dashboard } from 'Models/Dashboard';
import { AuthService } from 'src/app/Services/auth.service';
import { Chart } from 'chart.js';
import { Router } from '@angular/router';
import { ConnectionService } from 'src/app/Services/connection.service';

@Component({
  selector: 'app-reviewerdashboard',
  templateUrl: './reviewerdashboard.component.html',
  styleUrls: ['./reviewerdashboard.component.css']
})
export class ReviewerdashboardComponent implements OnInit {
  constructor(private route: Router, private connection: ConnectionService) { }
  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.route.navigateByUrl("")
    if (!AuthService.GetData("Reviewer")) {
      this.route.navigateByUrl("")
    }
    this.connection.GetReviewerDashboard()
      .subscribe({
        next: (data: Dashboard) => {
          this.piedata = data;
          var names = ['Articles to be Reviewed', 'Articles Reviewed'];
          var details = [];
          details.push(data.articlesTobeReviewed);
          details.push(data.articlesReviewed);
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
  public piedata: Dashboard = new Dashboard();

}
