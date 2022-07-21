import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';
import { ConnectionService } from 'src/app/Services/connection.service';
import { Subject } from 'rxjs';
import { Toaster } from 'ngx-toast-notifications';

@Component({
  selector: 'app-spamreportpage',
  templateUrl: './spamreportpage.component.html',
  styleUrls: ['./spamreportpage.component.css']
})

export class SpamreportpageComponent implements OnInit {
  public data: any[] = []
  error:any
  constructor(private connection: ConnectionService, private route: Router, private toaster: Toaster) { }

  //Get reported spams.
  ngOnInit(): void {

    if (AuthService.GetData("token") == null) {
      this.toaster.open({ text: 'Your Session has been Expired', position: 'top-center', type: 'warning' })
      this.route.navigateByUrl("")
    }
    if (!AuthService.IsAdmin()) {
      this.route.navigateByUrl("")
    }
    this.connection.GetSpams()
      .subscribe({
        next: (data: any[]) => {
          this.data = data;
          $(function () {
            $("#emptable").DataTable();
          });
        },
        error: (error:any) => this.error = error.error.message,
      });
  }
}
