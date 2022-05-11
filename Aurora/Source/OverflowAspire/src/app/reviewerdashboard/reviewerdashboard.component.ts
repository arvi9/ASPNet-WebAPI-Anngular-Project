import { HttpClient } from '@angular/common/http';
import { Component, OnInit,Input } from '@angular/core';
import { application } from 'Models/Application';
import { Dashboard } from 'Models/Dashboard';

@Component({
  selector: 'app-reviewerdashboard',
  templateUrl: './reviewerdashboard.component.html',
  styleUrls: ['./reviewerdashboard.component.css']
})
export class ReviewerdashboardComponent implements OnInit {
  @Input() Usersrc : string=`${application.URL}/Dashboard/GetReviewerDashboard?ReviewerId=1`;
  
  
 
  constructor(private http: HttpClient){}
 
  ngOnInit(): void {
    this.http
    .get<any>(this.Usersrc)
    .subscribe((data)=>{
      this.data =data;

      console.log(data);
    });
  }
  public data: Dashboard=new Dashboard();

  
}
