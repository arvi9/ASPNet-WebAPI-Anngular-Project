import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
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
  IsLoading: boolean = false;
  error=""
  raisequery = this.fb.group({
    title:['',[Validators.required,Validators.minLength(5),Validators.minLength(100)]],
    content:['',[Validators.minLength(20),Validators.minLength(900000000)]],
    code:['',[Validators.minLength(1),Validators.minLength(900000000)]]
  })
  constructor(private fb:FormBuilder, private connection: ConnectionService, private routing: Router,private toaster:Toaster) { }
 
  //User can create query.
  RaiseQuery() {
    this.IsLoading = true;
    const raisequery = {
    queryId: 0,
    title:this.raisequery.value['title'],
    content:this.raisequery.value['content'],
    code: this.raisequery.value['code'],
    isSolved: false,
    isActive: true,
    createdOn: new Date(),
    updatedBy: 0,
    queryComments: null,
    user: null
    }
    this.connection.CreateQuery(raisequery)
      .subscribe({
        next: (data) => {
        },
         error: (error) => {
          this.error = error.error.message;
          this.IsLoading=false;
        },
        complete: () => {
          this.toaster.open({ text: 'Query Raised Successfully', position: 'top-center', type: 'success' })
          this.routing.navigateByUrl("MyQueries");
        }
      });

  }

  ngOnInit(): void {
    if (AuthService.GetData("token") == null) {
      this.toaster.open({ text: 'Your Session has been Expired', position: 'top-center', type: 'warning' })
      this.routing.navigateByUrl("")
    }
  }
  
}