import { HttpClient } from '@angular/common/http';
import { Component, OnInit,Input } from '@angular/core';

import { application } from 'Models/Application';
import { User } from 'Models/User';

@Component({
  selector: 'app-userverificationpage',
  templateUrl: './userverificationpage.component.html',
  styleUrls: ['./userverificationpage.component.css']
})
export class UserverificationpageComponent implements OnInit {

  @Input() Usersrc : string=`${application.URL}/User/GetUsersByVerifyStatusId?VerifyStatusID=3`;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http
    .get<any>(this.Usersrc)
    .subscribe((data)=>{
      this.data =data;
      console.log(data);
    });
  }
  public data: User[] = [
  
  
  ];

  AcceptUser(userId:number){
    console.log("ge")
    this.http
    .patch(`https://localhost:7197/User/ChangeUserVerifyStatus?UserId=${userId}&IsVerified=true`,Object)  
    .subscribe((data)=>{
      console.log(data);
    });
  }


  RejectUser(userId:number){
    console.log("go")
    this.http
    .delete(`https://localhost:7197/User/RemoveUser?UserId=${userId}`)  
    .subscribe((data)=>{
      console.log(data);
    });
   
  }
}
