import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';


@Component({
  selector: 'app-queries',
  templateUrl: './queries.component.html',  
  styleUrls: ['./queries.component.css']
})
 
export class QueriesComponent implements OnInit {
Url:string ="allQueries"
constructor(private route:Router) { }

//Get all queries.
ngOnInit(): void {
  if(AuthService.GetData("token")==null) this.route.navigateByUrl("")
 }
}
