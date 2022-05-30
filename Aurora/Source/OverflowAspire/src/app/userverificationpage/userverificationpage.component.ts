import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit,Input } from '@angular/core';
import { Router } from '@angular/router';

import { application } from 'Models/Application';
import { User } from 'Models/User';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-userverificationpage',
  templateUrl: './userverificationpage.component.html',
  styleUrls: ['./userverificationpage.component.css']
})
export class UserverificationpageComponent implements OnInit {

  @Input() Usersrc : string=`${application.URL}/User/GetUsersByVerifyStatusId?VerifyStatusID=3`;
  constructor(private http: HttpClient,private routing:Router) { }

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
  public data: User[] = [
  
  
  ];
 
  AcceptUser(userId:number){
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    console.log("ge")
    this.http
    .patch(`https://localhost:7197/User/ChangeUserVerifyStatus?UserId=${userId}&IsVerified=true`,Object,{headers:headers})  
    .subscribe((data)=>{
      console.log(data);
    });
    this.routing.navigateByUrl("/Employee");
  }


  RejectUser(userId:number){
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    console.log("go")
    this.http
    .delete(`https://localhost:7197/User/RemoveUser?UserId=${userId}`,{headers:headers})  
    .subscribe((data)=>{
      console.log(data);
    });
    this.routing.navigateByUrl("/Employee");
  }
}
