import { Component, OnInit, Input } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from 'Models/User';
import { application } from 'Models/Application';
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


  DisableUser(userId: number) {
    this.connection.DisableUser(userId)
      .subscribe((data: any) => {
      });
    this.toaster.open({ text: 'User has Disabled', position: 'top-center', type: 'danger' })
  }


  onCheckboxChange(userId: any) {
    var res = this.data.find(item => item.userId == userId)?.isReviewer
    if (res == true) {
      this.connection.MarkAsReviewer(userId)
        .subscribe((data: any) => {
        });
      this.toaster.open({ text: 'User marked as Reviewer', position: 'top-center', type: 'success' })
    }
    else {
      this.connection.UnmarkAsReviewer(userId)
        .subscribe((data: any) => {
        });
      this.toaster.open({ text: 'User unmarked as Reviewer', position: 'top-center', type: 'secondary' })
    }
  }

  public data: User[] = []
}
