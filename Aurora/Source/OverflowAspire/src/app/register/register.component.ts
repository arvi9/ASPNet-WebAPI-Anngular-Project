import { Component, OnInit } from '@angular/core';
import { User } from 'Models/User';

@Component({
  selector: 'app-register',
  templateUrl:'./register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

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
