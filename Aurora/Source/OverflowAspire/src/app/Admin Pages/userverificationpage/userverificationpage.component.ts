import { Component, OnInit} from '@angular/core';
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
  dtOptions: any = {};
  dtTrigger: Subject<any> = new Subject();
  constructor(private routing: Router, private toaster: Toaster, private connection: ConnectionService) { }

  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.routing.navigateByUrl("")
    if (!AuthService.GetData("Admin")) {
      this.routing.navigateByUrl("")
    }
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      processing: true
    };
    this.connection.GetUsers()
      .subscribe({
        next: (data: User[]) => {
          this.data = data;
        }
      });
  }


  AcceptUser(userId: number) {
    this.connection.ApproveUser(userId)
      .subscribe({
        next: (data: any) => {
        }
      });
    this.toaster.open({ text: 'User Verified successfully', position: 'top-center', type: 'success' })
    this.routing.navigateByUrl("/Employee");

  }


  RejectUser(userId: number) {
    this.connection.RemoveUser(userId)
      .subscribe({
        next: (data: any) => {
        }
      });
    this.toaster.open({ text: 'User removed successfully', position: 'top-center', type: 'danger' })
    this.routing.navigateByUrl("/Employee");
  }

  public data: User[] = [];
}


