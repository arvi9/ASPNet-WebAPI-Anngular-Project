import { Component, OnInit } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { User } from 'Models/User';
import { AuthService } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';
import { Toaster } from 'ngx-toast-notifications';
import { ConnectionService } from 'src/app/Services/connection.service';
import { Subject } from 'rxjs';


@Component({
  selector: 'app-employee-page',
  templateUrl: './employee-page.component.html',
  styleUrls: ['./employee-page.component.css']
})
export class EmployeePageComponent implements OnInit {
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject();
  
  constructor(private http: HttpClient, private connection: ConnectionService, private route: Router, private toaster: Toaster) { }
 
  // Get the Employee page and shows the employee data
  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.route.navigateByUrl("")
    if (!AuthService.GetData("Admin")) {
      this.route.navigateByUrl("")
    }
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      processing: true,
      "retrieve": true,
    };
    this.connection.GetEmployeePage().subscribe((data: User[]) => {
      this.data = data;
    })
  }

 // Here the admin can disable the user.
  DisableUser(userId: number) {
    this.connection.DisableUser(userId)
    .subscribe({
      next: () => {},
      complete: () => {
        this.toaster.open({ text: 'User has Disabled', position: 'top-center', type: 'danger' })
      }
    }); 
    this.ngOnInit();  
  }

     // Here the admin can mark a user as reviewer.
  onCheckboxChange(userId: any) {
    var res = this.data.find(item => item.userId == userId)?.isReviewer
    if (res == true) {
      this.connection.MarkAsReviewer(userId)
      .subscribe({
        next: () => {},
        complete: () => {
          this.toaster.open({ text: 'User marked as Reviewer', position: 'top-center', type: 'success' })
        }
      });
      
    }
    //Here the admin can unmark a user as reviewer.
    else {
      this.connection.UnmarkAsReviewer(userId)
      .subscribe({
        next: () => {},
        complete: () => {
          this.toaster.open({ text: 'User unmarked as Reviewer', position: 'top-center', type: 'secondary' })  
        }
      });
      }
  }


  public data: User[] = []

}
