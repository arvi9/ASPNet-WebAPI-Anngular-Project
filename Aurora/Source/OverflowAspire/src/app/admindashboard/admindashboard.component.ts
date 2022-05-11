import { HttpClient } from '@angular/common/http';
import { Component, OnInit,Input } from '@angular/core';
import { Dashboard } from 'Models/Dashboard';
import { application } from 'Models/Application'

@Component({
  selector: 'app-admindashboard',
  templateUrl: './admindashboard.component.html',
  styleUrls: ['./admindashboard.component.css']
})
export class AdmindashboardComponent implements OnInit {

  @Input() Usersrc : string=`${application.URL}/Dashboard/GetAdminDashboard`;
  
  
 
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
