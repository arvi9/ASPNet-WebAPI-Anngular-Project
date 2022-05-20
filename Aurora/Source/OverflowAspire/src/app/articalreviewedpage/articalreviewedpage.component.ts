import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { Article } from 'Models/Article';
import { application } from 'Models/Application'

@Component({
  selector: 'app-articalreviewedpage',
  templateUrl: './articalreviewedpage.component.html',
  styleUrls: ['./articalreviewedpage.component.css']
})
export class ArticalreviewedpageComponent implements OnInit {
  @Input() Usersrc : string=`${application.URL}/Article/GetArticlesByArticleStatusId?ArticleStatusID=4`;
  
  totalLength :any;
  page : number= 1;
  
  constructor(private http: HttpClient){}
 
  ngOnInit(): void {
    this.http
    .get<any>(this.Usersrc)
    .subscribe((data)=>{
      this.data =data;
      this.totalLength=data.length;
      console.log(data);
    });
  }
  public data: Article[] = [
 

 
  ];
}
