import { Component, Input, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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
  queryId: number = 0;
  constructor(private routing: Router, private route: ActivatedRoute, private http: HttpClient, private connection: ConnectionService, private toaster: Toaster) { }

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

  public data: any[] = []


  onAccept() {
    this.connection.ApproveSpam(this.queryId)
      .subscribe({
        next: (data: any) => {
        }
      });
    this.connection.RemoveQuery(this.queryId)
      .subscribe({
        next: (data: any) => {
        }
      });
    this.toaster.open({ text: 'Query removed successfully', position: 'top-center', type: 'danger' })
    this.routing.navigateByUrl("/SpamReport");
  }


  onReject() {
    this.connection.RejectSpam(this.queryId)
      .subscribe({
        next: (data: any) => {
        }
      });
    this.toaster.open({ text: 'spam removed successfully', position: 'top-center', type: 'danger' })
    this.routing.navigateByUrl("/SpamReport");
  }
}
