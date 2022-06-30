import { Component, Input, OnInit } from '@angular/core';
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

  constructor(private routing: Router, private route: ActivatedRoute, private connection: ConnectionService, private toaster: Toaster) { }
  
  reportspam: any = {
    spamId: 0,
    reason: '',
    queryId: 3,
    userId: 0,
    verifyStatusID: 3,
  }


  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.routing.navigateByUrl("")
    this.route.params.subscribe(params => {
      this.queryId = params['queryId'];
      this.connection.GetQuery(this.queryId)
        .subscribe((data: any) => {
          this.data = data;
        });
    });
  }


  spamreport() {
    this.reportspam.queryId = this.queryId;
    this.connection.ReportSpam(this.reportspam)
      .subscribe({
        next: (data) => {
        }
      });
    this.toaster.open({ text: 'Reported spam successfully', position: 'top-center', type: 'success' })
    this.routing.navigateByUrl("/Home");
  }

}
