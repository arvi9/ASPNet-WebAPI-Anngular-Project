import { Component, OnInit } from '@angular/core';
import { Dashboard } from 'Models/Dashboard';
import { AuthService } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';
import { Chart } from 'chart.js';
import { ConnectionService } from 'src/app/Services/connection.service';

@Component({
  selector: 'app-admindashboard',
  templateUrl: './admindashboard.component.html',
  styleUrls: ['./admindashboard.component.css']
})

export class AdmindashboardComponent implements OnInit {
  piedata: any = {
    users: 0,
    articles: 0,
    queries: 0
  }
  constructor(private connection: ConnectionService, private route: Router) { }

  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.route.navigateByUrl("")
    if (!AuthService.IsAdmin()) {
      this.route.navigateByUrl("")
    }

    this.connection.GetAdminDashboard().subscribe(
      {
        next: (data: any) => {
          this.piedata = data;
          var names = ['Number of Articles', 'Number of Queries'];
          var details = [];
          details.push(data.articles.totalNumberOfArticles);
          details.push(data.queries.totalNumberOfQueries);
          new Chart('piechart', {
            type: 'pie',
            data: {
              labels: names,
              datasets: [{ data: details }]
            },
            options: {
              plugins: {
                legend: {
                  position: 'top',
                  display: true,
                },
              },
            }
          }
          );
        }
      }
    );
  }
}
