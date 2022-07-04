import { Component, OnInit, Input } from '@angular/core';
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
  constructor(private connection: ConnectionService, private route: Router) { }

  ngOnInit(): void {
    if (!AuthService.GetData("Admin")) {
      this.route.navigateByUrl("")
    }
    this.connection.GetAdminDashboard()
      .subscribe(
        {
          next: (data: Dashboard) => {
            this.piedata = data;
            var names = ['Number of Articles', 'Number of Queries'];
            var details = [];
            details.push(data.totalNumberOfArticles);
            details.push(data.totalNumberofQueries);
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