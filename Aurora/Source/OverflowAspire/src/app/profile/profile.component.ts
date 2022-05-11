import { Component,Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from 'Models/User';
import { application } from 'Models/Application';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  @Input() Usersrc : string=`${application.URL}/User/GetUser?UserId=1`;
  
  totalLength :any;
  page : number= 1;
 
  constructor(private http: HttpClient){}
 
  ngOnInit(): void {
    this.http
    .get<any>(this.Usersrc)
    .subscribe((data)=>{
      this.user =data;
      this.totalLength=data.length;
      console.log(data);
    });
    
  }
  public user:User={
    
    
    userId: 0,
    fullName: 'Pooja',
    genderId: 1,
    aceNumber: 'ACE0564',
    employeeId:'',
    email: 'pooja@gmail.com',
    department:'dotnet',
    designation:'',
    password: '',
    dateOfBirth: '05/05/2001',
    verifyStatusID: 0,
    isReviewer: false,
    userRoleId: 0,
    designationId: 0
  } 
 

  
}
