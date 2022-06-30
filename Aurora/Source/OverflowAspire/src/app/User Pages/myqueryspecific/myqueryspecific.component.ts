import { Component, OnInit } from '@angular/core';
import { Query } from 'Models/Query';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { Toaster } from 'ngx-toast-notifications';
import { ConnectionService } from 'src/app/Services/connection.service';

declare type myarray = Array<{ content: string, coding: string, name: string }>

@Component({
  selector: 'app-myqueryspecific',
  templateUrl: './myqueryspecific.component.html',
  styleUrls: ['./myqueryspecific.component.css']
})
export class MyqueryspecificComponent implements OnInit {
  issolved: any
  queryDetails: any = this.route.params.subscribe(params => {
    this.queryId = params['queryId'];
  });
  queryId: number = this.queryDetails;

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

  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.routing.navigateByUrl("")
    this.route.params.subscribe(params => {
      this.queryId = params['queryId'];
      this.connection.GetQuery(this.queryId)
        .subscribe({
          next: (data: Query) => {
            this.data = data;
            this.issolved = data.isSolved;
          }
        });
    });
  }

  public data: Query = new Query();

  MarkQueryAsSolved(QueryId: number) {
    this.connection.MarkQueryAsSolved(QueryId)
      .subscribe({
        next: (data: any) => {
        }
      });
    this.toaster.open({ text: 'Query Marked As Solved', position: 'top-center', type: 'success' })
    this.routing.navigateByUrl("/MyQueries");
  }

}
