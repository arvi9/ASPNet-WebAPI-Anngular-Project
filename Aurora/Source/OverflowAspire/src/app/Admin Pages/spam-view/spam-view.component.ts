import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { Toaster } from 'ngx-toast-notifications';
import { ConnectionService } from 'src/app/Services/connection.service';

@Component({
  selector: 'app-spam-view',
  templateUrl: './spam-view.component.html',
  styleUrls: ['./spam-view.component.css']
})

export class SpamViewComponent implements OnInit {
  error = ""
  queryId: number = 0;
  public data: any[] = []

  constructor(private routing: Router, private route: ActivatedRoute, private connection: ConnectionService, private toaster: Toaster) { }

  //Get spams by query id.
  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.routing.navigateByUrl("")
    if (!AuthService.GetData("Admin")) {
      this.routing.navigateByUrl("")
    }
    this.route.params.subscribe(params => {
      this.queryId = params['queryId'];
      this.connection.GetSpams()
        .subscribe((data: any[]) => {
          this.data = data;
          this.data = this.data.filter(item => item.query.queryId == this.queryId)
        });
    });
  }

  //Here the admin can approve spam by query id.
  onAccept() {
    this.connection.ApproveSpam(this.queryId)
      .subscribe({
        next: (data: any) => {
        },
      });
    //Here the admin can remove query by query id.
    this.connection.RemoveQuery(this.queryId)
      .subscribe({
        next: (data: any) => {
        },
      });
    this.toaster.open({ text: 'Query removed successfully', position: 'top-center', type: 'danger' })
    this.routing.navigateByUrl("/SpamReport");
  }

  //Here the admin can reject spam by query id.
  onReject() {
    this.connection.RejectSpam(this.queryId)
      .subscribe({
        next: (data: any) => {
        },
      });
    this.toaster.open({ text: 'spam removed successfully', position: 'top-center', type: 'danger' })
    this.routing.navigateByUrl("/SpamReport");
  }
}
