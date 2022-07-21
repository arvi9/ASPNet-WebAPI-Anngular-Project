import { Component, OnInit } from '@angular/core';
import { QueryService } from 'src/app/Services/query.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { Toaster } from 'ngx-toast-notifications';
import { ConnectionService } from 'src/app/Services/connection.service';

@Component({
  selector: 'app-report-spam',
  templateUrl: './report-spam.component.html',
  styleUrls: ['../specificquery/specificquery.component.css'],
  providers: [QueryService]
})

export class ReportSpamComponent implements OnInit {
  data: any;
  queryId: number = 0
  error = ""
  reportspam: any = {
    spamId: 0,
    reason: '',
    queryId: 3,
    userId: 0,
    verifyStatusID: 3,
  }

  constructor(private routing: Router, private route: ActivatedRoute, private connection: ConnectionService, private toaster: Toaster) { }

  //Get query by its id.
  ngOnInit(): void {
    if (AuthService.GetData("token") == null) {
      this.toaster.open({ text: 'Your Session has been Expired', position: 'top-center', type: 'warning' })
      this.routing.navigateByUrl("")
    }
    this.route.params.subscribe(params => {
      this.queryId = params['queryId'];
      this.connection.GetQuery(this.queryId)
        .subscribe((data: any) => {
          this.data = data;
        });
    });
  }

  //Report query as spam.
  spamreport() {
    this.reportspam.queryId = this.queryId;
    this.connection.ReportSpam(this.reportspam)
      .subscribe({
        next: (data) => {
        },
        error: (error) => {
          this.error = error.error.message;
        },
        complete: () => {
          this.toaster.open({ text: 'Reported spam successfully', position: 'top-center', type: 'success' })
          this.routing.navigateByUrl("/Queries");
        }
      }); 
  }

}
