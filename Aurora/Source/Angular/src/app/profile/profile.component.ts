import { Component, OnInit } from '@angular/core';
import { User } from 'Models/User';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  constructor() { }
  user:User={
    userId: 0,
    fullName: 'Pooja',
    genderId: 1,
    aceNumber: 'ACE0564',
    emailAddress: 'pooja@gmail.com',
    password: '',
    dateOfBirth: '05/05/2001',
    verifyStatusID: 0,
    isReviewer: false,
    userRoleId: 0,
    designationId: 0
  }

  ngOnInit(): void {
  }

}
