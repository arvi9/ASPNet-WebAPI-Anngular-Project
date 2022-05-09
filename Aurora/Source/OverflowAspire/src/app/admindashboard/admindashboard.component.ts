import { HttpClient } from '@angular/common/http';
import { Component, OnInit,Input } from '@angular/core';
import { Dashboard } from 'Models/Dashboard';

@Component({
  selector: 'app-admindashboard',
  templateUrl: './admindashboard.component.html',
  styleUrls: ['./admindashboard.component.css']
})
export class AdmindashboardComponent implements OnInit {

  @Input() Usersrc : string="https://localhost:7197/Dashboard/GetAdminDashboard";
  
  
 
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
