import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { Article } from 'Models/Article';
import { application } from 'Models/Application'
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-articalreviewedpage',
  templateUrl: './articalreviewedpage.component.html',
  styleUrls: ['./articalreviewedpage.component.css']
})
export class ArticalreviewedpageComponent implements OnInit {
  @Input() Usersrc : string=`${application.URL}/Article/GetArticlesByArticleStatusId?ArticleStatusID=4`;
  
  totalLength :any;
  page : number= 1;
  
  constructor(private http: HttpClient,private route:Router) { }
  ngOnInit(): void {
    if(AuthService.GetData("token")==null) this.route.navigateByUrl("")
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    this.http
    .get<any>(this.Usersrc,{headers:headers})
    .subscribe({next:(data)=>{
      this.data =data;
      this.totalLength=data.length;
      console.log(data);
    }});
  }
  public data: Article[] = [
 

 
  ];
}
