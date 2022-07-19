import { Component, OnInit } from '@angular/core';
import { Dashboard } from 'Models/Dashboard';
import { AuthService } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';
import { Chart } from 'chart.js';
import { ConnectionService } from 'src/app/Services/connection.service';
import { Toaster } from 'ngx-toast-notifications';

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
  constructor(private connection: ConnectionService, private route: Router,private toaster: Toaster) { }

  ngOnInit(): void {
    
 if (AuthService.GetData("token") == null) {
this.toaster.open({ text: 'Your Session has been Expired', position: 'top-center', type: 'warning' })
      this.route.navigateByUrl("")
    }
    if (!AuthService.IsAdmin()) {
      this.route.navigateByUrl("")
    }

    this.connection.GetAdminDashboard().subscribe(
      {
        next: (data: any) => {
          this.piedata = data;
          var names = ['Number of articles', 'Number of queries'];
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
