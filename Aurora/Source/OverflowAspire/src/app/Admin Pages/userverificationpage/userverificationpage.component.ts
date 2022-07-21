import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'Models/User';
import { AuthService } from 'src/app/Services/auth.service';
import { Toaster } from 'ngx-toast-notifications';
import { ConnectionService } from 'src/app/Services/connection.service';

@Component({
  selector: 'app-userverificationpage',
  templateUrl: './userverificationpage.component.html',
  styleUrls: ['./userverificationpage.component.css']
})

export class UserverificationpageComponent implements OnInit {
  public data: User[] = [];
  error:any;
  constructor(private routing: Router, private toaster: Toaster, private connection: ConnectionService) { }

  //Get all the Users.
  ngOnInit(): void {
    if (AuthService.GetData("token") == null) {
      this.toaster.open({ text: 'Your Session has been Expired', position: 'top-center', type: 'warning' })
      this.routing.navigateByUrl("")
    }
    if (!AuthService.IsAdmin()) {
      this.routing.navigateByUrl("")
    }
    this.connection.GetUsers()
      .subscribe({
        next: (data: User[]) => {
          this.data = data;
          $(function () {
            $("#emptable").DataTable();
          });
        },
        error: (error:any) => this.error = error.error.message,
      });
  }

  //Here the admin can accept user.
  AcceptUser(userId: number) {
    this.connection.ApproveUser(userId)
      .subscribe({
        next: (data: any) => {
        },
        error: (error:any) => this.error = error.error.message,
        complete: () => {
          this.toaster.open({ text: 'User Verified successfully', position: 'top-center', type: 'success' })
          this.routing.navigateByUrl("/Employee");
        }
      });
      this.closePopup();
  }


  //Here the admin can reject user.
  RejectUser(userId: number) {
    this.connection.RemoveUser(userId)
      .subscribe({
        next: (data: any) => {
        },
        complete: () => {
          this.toaster.open({ text: 'User removed successfully', position: 'top-center', type: 'danger' })
          this.routing.navigateByUrl("/Employee");
        }
      });
  }
  displayStyle = "none";
  
  openPopup() {
    this.displayStyle = "block";
  }
  closePopup() {
    this.displayStyle = "none";
  }
}


