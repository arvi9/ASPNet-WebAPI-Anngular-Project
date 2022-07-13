import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'Models/User';
import { AuthService } from 'src/app/Services/auth.service';
import { Toaster } from 'ngx-toast-notifications';
import { ConnectionService } from 'src/app/Services/connection.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-userverificationpage',
  templateUrl: './userverificationpage.component.html',
  styleUrls: ['./userverificationpage.component.css']
})

export class UserverificationpageComponent implements OnInit {
  public data: User[] = [];
  constructor(private routing: Router, private toaster: Toaster, private connection: ConnectionService) { }

  //Get all the Users.
  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.routing.navigateByUrl("")
    if (!AuthService.IsAdmin()) {
      this.routing.navigateByUrl("")
    }
    this.connection.GetUsers()
      .subscribe({
        next: (data: User[]) => {
          this.data = data;
          $(function(){
            $("#emptable").DataTable();
           });
        }
      });
  }

  //Here the admin can accept user.
  AcceptUser(userId: number) {
    this.connection.ApproveUser(userId)
      .subscribe({
        next: (data: any) => {
        }
      });
    this.toaster.open({ text: 'User Verified successfully', position: 'top-center', type: 'success' })
    this.routing.navigateByUrl("/Employee");
  }


  //Here the admin can reject user.
  RejectUser(userId: number) {
    this.connection.RemoveUser(userId)
      .subscribe({
        next: (data: any) => {
        }
      });
    this.toaster.open({ text: 'User removed successfully', position: 'top-center', type: 'danger' })
    this.routing.navigateByUrl("/Employee");
  }
}


