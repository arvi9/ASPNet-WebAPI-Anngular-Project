import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { ConnectionService } from 'src/app/Services/connection.service';

@Component({
  selector: 'app-raisequery',
  templateUrl: './raisequery.component.html',
  styleUrls: ['../specificquery/specificquery.component.css']
})
export class RaisequeryComponent implements OnInit {
  IsLoading: boolean = false;
  query: any = {
    queryId: 0,
    title: '',
    content: '',
    code: '',
    isSolved: false,
    isActive: true,
    createdBy: 1,
    createdOn: new Date(),
    updatedBy: 0,
    updatedOn: new Date(),
    queryComments: null,
    user: null
  }

  constructor(private connection: ConnectionService, private routing: Router) { }
 
  //User can create query.
  RaiseQuery() {
    this.IsLoading = true;
    this.connection.CreateQuery(this.query)
      .subscribe({
        next: (data) => {
        }
      });
    this.routing.navigateByUrl("MyQueries");
  }

  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.routing.navigateByUrl("")
  }
  
}

