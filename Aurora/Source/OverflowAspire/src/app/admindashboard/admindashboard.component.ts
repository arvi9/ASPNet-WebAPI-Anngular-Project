import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit,Input } from '@angular/core';
import { Dashboard } from 'Models/Dashboard';
import { application } from 'Models/Application'
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import {Chart} from 'chart.js';
@Component({
  selector: 'app-admindashboard',
  templateUrl: './admindashboard.component.html',
  styleUrls: ['./admindashboard.component.css']
})
export class AdmindashboardComponent implements OnInit {
  constructor(private http: HttpClient,private route:Router) { }
  ngOnInit(): void {
    if(AuthService.GetData("token")==null) this.route.navigateByUrl("")
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    this.http
      .get<any>(`${application.URL}/Dashboard/GetAdminDashboard`,{headers:headers})
      .subscribe((data) => {
        this.piedata = data;
        console.log(data);
        var names=['Number of Articles', 'Number of Queries'];
        var details=[];
        details.push(data.totalNumberOfArticles);
        details.push(data.totalNumberofQueries);
      
     console.log(data.totalNumberOfArticles)
      
  
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
  