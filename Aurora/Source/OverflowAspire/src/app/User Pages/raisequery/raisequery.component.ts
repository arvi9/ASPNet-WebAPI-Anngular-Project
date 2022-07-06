import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Toaster } from 'ngx-toast-notifications';
import { AuthService } from 'src/app/Services/auth.service';
import { ConnectionService } from 'src/app/Services/connection.service';

@Component({
  selector: 'app-raisequery',
  templateUrl: './raisequery.component.html',
  styleUrls: ['../specificquery/specificquery.component.css']
})
export class RaisequeryComponent implements OnInit {

  constructor(private connection: ConnectionService, private routing: Router, private toaster: Toaster) { }
  IsLoading: boolean = false;
  query: any = {
    queryId: 0,
    title: '',
    content: '',
    code: '',
    isSolved: false,
    isActive: true,
    createdBy: 1,
    createdOn: Date.now,
    updatedBy: 0,
    updatedOn: null,
    queryComments: null,
    user: null
  }

  RaiseQuery() {
    this.IsLoading = true;
    this.connection.CreateQuery(this.query)
      .subscribe({
        next: (data) => {
          this.toaster.open({ text: 'Query Added Successfully', position: 'top-center', type: 'secondary' })
          this.routing.navigateByUrl("MyQueries");
        }
      });
   
  }


  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.routing.navigateByUrl("")
  }
  
}

