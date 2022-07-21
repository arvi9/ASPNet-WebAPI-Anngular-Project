import { Component, OnInit } from '@angular/core';
import { Query } from 'Models/Query';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { Toaster } from 'ngx-toast-notifications';
import { ConnectionService } from 'src/app/Services/connection.service';

@Component({
  selector: 'app-myqueryspecific',
  templateUrl: './myqueryspecific.component.html',
  styleUrls: ['./myqueryspecific.component.css']
})

export class MyqueryspecificComponent implements OnInit {
  issolved: any
  public data: Query = new Query();
  queryDetails: any = this.route.params.subscribe(params => {
    this.queryId = params['queryId'];
  });
  queryId: number = this.queryDetails;
  error: any
  Query: any = {
    CommentId: 0,
    comment: '',
    datetime: Date.now,
    code: "",
    userId: 1,
    queryId: 0,
    createdBy: 10,
    createdOn: Date.now,
  }

  constructor(private routing: Router, private route: ActivatedRoute, private connection: ConnectionService, private toaster: Toaster) { }
  //Get specific query by its id.
  ngOnInit(): void {
    if (AuthService.GetData("token") == null) {
      this.toaster.open({ text: 'Your Session has been Expired', position: 'top-center', type: 'warning' })
      this.routing.navigateByUrl("")
    }
    this.route.params.subscribe(params => {
      this.queryId = params['queryId'];
      this.connection.GetQuery(this.queryId)
        .subscribe({
          next: (data: Query) => {
            this.data = data;
            this.issolved = data.isSolved;
          },
          error: (error: any) => this.error = error.error.message,
        });
    });
  }

  MarkQueryAsSolved(QueryId: number) {
    this.connection.MarkQueryAsSolved(QueryId)
      .subscribe({
        next: (data: any) => {
        },
        error: (error: any) => this.error = error.error.message,
      });
    this.toaster.open({ text: 'Query Marked As Solved', position: 'top-center', type: 'success' })
    this.routing.navigateByUrl("/MyQueries");
  }

}
