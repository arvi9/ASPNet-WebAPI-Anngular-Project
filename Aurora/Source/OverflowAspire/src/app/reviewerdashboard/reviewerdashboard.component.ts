import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit,Input } from '@angular/core';
import { application } from 'Models/Application';
import { Dashboard } from 'Models/Dashboard';
import { AuthService } from '../auth.service';
import {Chart} from 'chart.js';
@Component({
  selector: 'app-reviewerdashboard',
  templateUrl: './reviewerdashboard.component.html',
  styleUrls: ['./reviewerdashboard.component.css']
})
export class ReviewerdashboardComponent implements OnInit {
  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    this.http
      .get<any>(`${application.URL}/Dashboard/GetReviewerDashboard?ReviewerId=1`,{headers:headers})
      .subscribe((data) => {
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
  });
  
   
  }
  public piedata: Dashboard=new Dashboard();
  
  }
  