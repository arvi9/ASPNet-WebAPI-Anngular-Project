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
   // Here the user can logout the application.
  LogOut(){
    AuthService.Logout();
    this.route.navigateByUrl("")
  }
 

  public user:User={
    userId: 0,
    fullName: '',
    genderId: 1,
    aceNumber: '',
    employeeId:'',
    email: '',
    department:'',
    designation:'',
    password: '',
    dateOfBirth: '',
    verifyStatusID: 0,
    isReviewer: false,
    userRoleId: 0,
    designationId: 0
  }
}
