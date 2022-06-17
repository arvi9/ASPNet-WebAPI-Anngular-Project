import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { application } from 'Models/Application';
import { AuthService } from '../auth.service';


@Component({
  selector: 'app-queries',
  templateUrl: './queries.component.html',
  styleUrls: ['./queries.component.css']
})
export class QueriesComponent implements OnInit {

Url:string =`${application.URL}/Query/GetAll`
constructor(private http: HttpClient,private route:Router) { }
ngOnInit(): void {
  if(AuthService.GetData("token")==null) this.route.navigateByUrl("")
   this.ngOnInit
 }
}
