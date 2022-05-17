import { Component, OnInit } from '@angular/core';
import {Chart} from 'chart.js';
import { HttpClient } from '@angular/common/http';
import { Dashboard } from 'Models/Dashboard';
import { application } from 'Models/Application';
@Component({
  selector: 'app-admin-piechart',
  templateUrl: './admin-piechart.component.html',
  styleUrls: ['./admin-piechart.component.css']
})
export class AdminPiechartComponent implements OnInit {

  constructor(private http: HttpClient) { }
ngOnInit(): void {
  this.http
    .get<any>(`${application.URL}/Dashboard/GetAdminDashboard`)
    .subscribe((data) => {
      this.piedata = data;
      console.log(data);
      var names=['Number of Articles', 'Number of Queries'];
      var details=[];
      details.push(data.totalNumberOfArticles);
      details.push(data.totalNumberofQueries);
    
   
    

    new Chart('piechart',{
      
      type:'pie',
      data:{
      labels:names,
      datasets:[{
        data:details,

      
      }]
    },
    options: {
      plugins: {
        legend: {
          position: 'top',
          display:true,
        },
      },
      
    }
  });
});

 
}
public piedata: Dashboard[] = [];
}
