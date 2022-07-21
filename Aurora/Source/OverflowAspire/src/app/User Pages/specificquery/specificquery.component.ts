import { Component, OnInit } from '@angular/core';
import { Query } from 'Models/Query';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { Toaster } from 'ngx-toast-notifications';
import { ConnectionService } from 'src/app/Services/connection.service';


@Component({
  selector: 'app-specificquery',
  templateUrl: './specificquery.component.html',
  styleUrls: ['./specificquery.component.css']
})

export class SpecificqueryComponent implements OnInit {
  issolved = false;
  error: any
  queryDetails: any = this.route.params.subscribe(params => {
    this.queryId = params['queryId'];
  });
  queryId: number = this.queryDetails;
  public data: Query = new Query();
  Query: any = {
    CommentId: 0,
    comment: '',
    datetime: new Date(),
    code: "",
    userId: 1,
    queryId: 0,
    createdBy: 10,
    createdOn: new Date(),
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
      this.GetQuery()
    });
  }

  GetQuery(): void {
    this.connection.GetQuery(this.queryId)
      .subscribe({
        next: (data: Query) => {
          this.data = data;
          this.issolved = data.isSolved;
        },
        error: (error: any) => this.error = error.error.message,
      });
  }

  //Add comment to that query.
  PostComment() {
    this.Query.queryId = this.queryId;
    this.connection.PostQueryComment(this.Query)
      .subscribe({
        next: () => {
        },
        error: (error: any) => this.error = error.error.message,
      });
    this.toaster.open({ text: 'Comment Posted successfully', position: 'top-center', type: 'success' })
    this.ngOnInit();
    setTimeout(
      () => {
        location.reload(); // the code to execute after the timeout
      },
      1000// the time to sleep to delay for
    );
  }
}
