import { Component, OnInit,Input } from '@angular/core';
import { User } from 'Models/User';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor() { }
  user:User={
    userId: 0,
    fullName: '',
    genderId: 0,
    aceNumber: '',
    emailAddress: '',
    password: '',
    dateOfBirth: '',
    verifyStatusID: 0,
    isReviewer: false,
    userRoleId: 0,
    designationId: 0
  }
   

  ngOnInit(): void {
  }
  onSubmit(){
    console.log(this.user)
  }
}
