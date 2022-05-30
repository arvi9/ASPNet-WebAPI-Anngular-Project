import { Component,Input, OnInit } from '@angular/core';
import { application } from 'Models/Application';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-spam-view',
  templateUrl: './spam-view.component.html',
  styleUrls: ['./spam-view.component.css']
})
export class SpamViewComponent implements OnInit {
queryId: number = 0


@Input() Querysrc : string = `${application.URL}/Query/GetListOfSpams`;
constructor(private http: HttpClient,private routing:Router) { }

ngOnInit(): void {
  const headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${AuthService.GetData("token")}`
  })
  console.log(AuthService.GetData("token"))
  this.http
  .get<any>(this.Querysrc,{headers:headers})
  .subscribe((data)=>{
    this.data =data;
    this.data=this.data.filter(item=> item.query.queryId==this.queryId)
    console.log(data);
  });
}

public data: any[] = []

 
onAccept(){
  this.routing.navigateByUrl("/SpamReport");
}
onReject(){
  this.routing.navigateByUrl("/SpamReport");
}
}