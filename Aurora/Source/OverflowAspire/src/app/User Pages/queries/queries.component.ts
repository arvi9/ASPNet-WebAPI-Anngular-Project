import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Toaster } from 'ngx-toast-notifications';
import { AuthService } from 'src/app/Services/auth.service';


@Component({
  selector: 'app-queries',
  templateUrl: './queries.component.html',  
  styleUrls: ['./queries.component.css']
})
 
export class QueriesComponent implements OnInit {
Url:string ="allQueries"
constructor(private routing: Router,private toaster:Toaster) { }

//Get all queries.
ngOnInit(): void {
  if (AuthService.GetData("token") == null) {
    this.toaster.open({ text: 'Your Session has been Expired', position: 'top-center', type: 'warning' })
    this.routing.navigateByUrl("")
  }
 }
}
