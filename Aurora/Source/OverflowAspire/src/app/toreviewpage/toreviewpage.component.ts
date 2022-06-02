import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { application } from 'Models/Application';
import { Article } from 'Models/Article';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-toreviewpage',
  templateUrl: './toreviewpage.component.html',
  styleUrls: ['./toreviewpage.component.css']
})
export class ToreviewpageComponent implements OnInit {
  @Input() Usersrc : string=`${application.URL}/Article/GetArticlesByArticleStatusId?ArticleStatusID=2`;
  
  totalLength :any;
  page : number= 1;
 
  constructor(private http: HttpClient){}
 
  ngOnInit(): void {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    this.http
    .get<any>(this.Usersrc,{headers:headers})
    .subscribe((data)=>{
      this.data =data;
      this.totalLength=data.length;
      console.log(data);
    });
   
  }
  public data: Article[] = [
 

 
  ];
 
  
}
