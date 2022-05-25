import { Component,Input, OnInit } from '@angular/core';
import { application } from 'Models/Application';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

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
  this.http
  .get<any>(this.Querysrc)
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