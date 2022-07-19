import { Component, OnInit } from '@angular/core';
import { User } from 'Models/User';
import { AuthService } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';
import { Toaster } from 'ngx-toast-notifications';
import { ConnectionService } from 'src/app/Services/connection.service';


@Component({
  selector: 'app-employee-page',
  templateUrl: './employee-page.component.html',
  styleUrls: ['./employee-page.component.css']
})

export class EmployeePageComponent implements OnInit {
  public data: User[] = []
  constructor(private connection: ConnectionService, private route: Router, private toaster: Toaster) { }

  //Get the Employee page and shows the employee data
  ngOnInit(): void {
    
 if (AuthService.GetData("token") == null) {
this.toaster.open({ text: 'Your Session has been Expired', position: 'top-center', type: 'warning' })
      this.route.navigateByUrl("")
    }
    if (!AuthService.IsAdmin()) {
      this.route.navigateByUrl("")
    }
    this.connection.GetEmployeePage().subscribe((data: User[]) => {
      this.data = data;
      $(function(){
        $("#emptable").DataTable();
       });
    })
  }

  //Here the admin can disable the user.
  DisableUser(userId: number) {
    this.connection.DisableUser(userId)
      .subscribe({
        next: () => { },
        complete: () => {
          this.toaster.open({ text: 'User has Disabled', position: 'top-center', type: 'danger' })
        }
      });
    setTimeout(
      () => {
        location.reload(); 
      },
      1000
    );
  }


  //Method for marking and unmarking as reviewer
  onCheckboxChange(userId: any) {
    var res = this.data.find(item => item.userId == userId)?.isReviewer
    //Here the admin can mark a user as reviewer.
    if (res == true) {
      this.connection.MarkAsReviewer(userId)
        .subscribe({
          next: () => { },
          complete: () => {
            this.toaster.open({ text: 'User marked as Reviewer', position: 'top-center', type: 'success' })
          }
        });
    }
    //Here the admin can unmark a user as reviewer.
    else {
      this.connection.UnmarkAsReviewer(userId)
        .subscribe({
          next: () => { },
          complete: () => {
            this.toaster.open({ text: 'User unmarked as Reviewer', position: 'top-center', type: 'secondary' })
          }
        });
    }
    this.closePopup()
  }

  displayStyle = "none";
  
  openPopup() {
    this.displayStyle = "block";
  }
  closePopup() {
    this.displayStyle = "none";
  }
}
