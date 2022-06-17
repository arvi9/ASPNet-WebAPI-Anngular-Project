import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { application } from 'Models/Application';
import { User } from 'Models/User';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  role=false;
  @Input() Usersrc : string=`${application.URL}/User/GetUser`;
  
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    this.http
    .get<any>(this.Usersrc,{headers:headers})
    .subscribe((data)=>{
      this.user =data;
      this.role=data.isReviewer;
    });

   }
  LogOut(){
    AuthService.Logout();
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
