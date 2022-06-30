import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'Models/User';
import { AuthService } from 'src/app/Services/auth.service';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  role=AuthService.GetData("Reviewer")

  constructor(private route:Router) { }

  ngOnInit(): void {
   }
  LogOut(){
    AuthService.Logout();
    this.route.navigateByUrl("")
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
