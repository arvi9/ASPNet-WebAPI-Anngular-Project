import { Component, OnInit } from '@angular/core';
import {Chart} from 'chart.js';
import { HttpClient } from '@angular/common/http';
import { Dashboard } from 'Models/Dashboard';
import { application } from 'Models/Application';
@Component({
  selector: 'app-reviewer-piechart',
  templateUrl: './reviewer-piechart.component.html',
  styleUrls: ['./reviewer-piechart.component.css']
})
export class ReviewerPiechartComponent implements OnInit {

  constructor(private http: HttpClient) { }
ngOnInit(): void {
  this.http
    .get<any>(`${application.URL}/Dashboard/GetReviewerDashboard?ReviewerId=1`)
    .subscribe({next:(data) => {
      this.piedata = data;
      console.log(data);
      var names=['Articles to be Reviewed', 'Articles Reviewed'];
      var details=[];
      details.push(data.articlesTobeReviewed);
      details.push(data.articlesReviewed);
    
   
    

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
}});

 
}
public piedata: Dashboard[] = [];

}
