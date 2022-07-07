import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';


@Component({
  selector: 'app-queries',
  templateUrl: './queries.component.html',
  styleUrls: ['./queries.component.css']
})

  //Get all queries.
export class QueriesComponent implements OnInit {

Url:string ="allQueries"
constructor(private http: HttpClient,private route:Router) { }
ngOnInit(): void {
  if(AuthService.GetData("token")==null) this.route.navigateByUrl("")
   this.ngOnInit
 }
}
