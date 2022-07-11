import { Component, OnInit } from '@angular/core';
import { User } from 'Models/User';
import { AuthService } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';
import { ConnectionService } from 'src/app/Services/connection.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})

export class ProfileComponent implements OnInit {
  public user: User = {
    userId: 0,
    fullName: '',
    genderId: 1,
    aceNumber: '',
    employeeId: '',
    email: '',
    department: '',
    designation: '',
    password: '',
    dateOfBirth: '',
    verifyStatusID: 0,
    isReviewer: false,
    userRoleId: 0,
    designationId: 0
  }
  constructor(private connection: ConnectionService, private route: Router) { }

  //Get user profile.
  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.route.navigateByUrl("")
    this.connection.GetCurrentApplicationUser()
      .subscribe({
        next: (data: User) => {
          this.user = data;
        }
      });
  }
}
