import { Component, Input, OnInit } from '@angular/core';
import { Query } from 'Models/Query';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { Toaster } from 'ngx-toast-notifications';
import { ConnectionService } from 'src/app/Services/connection.service';

declare type myarray = Array<{ content: string, coding: string, name: string }>

@Component({
  selector: 'app-specificquery',
  templateUrl: './specificquery.component.html',
  styleUrls: ['./specificquery.component.css']
})
export class SpecificqueryComponent implements OnInit {
  queryDetails: any = this.route.params.subscribe(params => {
    this.queryId = params['queryId'];
  });
  queryId: number = this.queryDetails;

  Query: any = {
    CommentId: 0,
    comment: '',
    datetime:Date.now,
    code: "",
    userId: 1,
    queryId: 0,
    createdBy: 10,
    createdOn: Date.now,
  }

  constructor(private routing: Router, private route: ActivatedRoute, private connection: ConnectionService,private toaster: Toaster) { }

  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.routing.navigateByUrl("")
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
        }
      });
  }


  public data: Query = new Query();

  PostComment() {
    this.Query.queryId = this.queryId;
    this.connection.PostQueryComment(this.Query)
      .subscribe({
        next: () => {
        }
      });
    console.log(this.Query)
    this.toaster.open({ text: 'Comment Posted successfully', position: 'top-center', type: 'success' })
    this.ngOnInit();
  }
}
