import { HttpClient } from '@angular/common/http';
import { Component, OnInit,Input } from '@angular/core';
import { Dashboard } from 'Models/Dashboard';
import { application } from 'Models/Application'
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admindashboard',
  templateUrl: './admindashboard.component.html',
  styleUrls: ['./admindashboard.component.css']
})
export class AdmindashboardComponent implements OnInit {

  @Input() Usersrc : string=`${application.URL}/Dashboard/GetAdminDashboard`;



  constructor(private http: HttpClient,private route:Router){}

  ngOnInit(): void {



    if(!AuthService.IsAdmin()) this.route.navigateByUrl("")  //if u want u can navigate to 404 page


   this.http
    .get<any>(this.Usersrc)
    .subscribe((data)=>{
      this.data =data;

      console.log(data);
    });


    }

  public data: Dashboard=new Dashboard();

}
