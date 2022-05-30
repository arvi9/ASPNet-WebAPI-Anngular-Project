import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit,Input } from '@angular/core';
import { application } from 'Models/Application';
import { Dashboard } from 'Models/Dashboard';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-reviewerdashboard',
  templateUrl: './reviewerdashboard.component.html',
  styleUrls: ['./reviewerdashboard.component.css']
})
export class ReviewerdashboardComponent implements OnInit {
  @Input() Usersrc : string=`${application.URL}/Dashboard/GetReviewerDashboard?ReviewerId=1`;
  
  
 
  constructor(private http: HttpClient){}
 
  ngOnInit(): void {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    this.http
    .get<any>(this.Usersrc,{headers:headers})
    .subscribe((data)=>{
      this.data =data;

      console.log(data);
    });
  }
  public data: Dashboard=new Dashboard();

  
}
