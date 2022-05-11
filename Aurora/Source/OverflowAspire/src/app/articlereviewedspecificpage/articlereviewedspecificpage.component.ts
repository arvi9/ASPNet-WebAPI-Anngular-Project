import { Component,Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Article } from 'Models/Article';
import { application } from 'Models/Application'

@Component({
  selector: 'app-articlereviewedspecificpage',
  templateUrl: './articlereviewedspecificpage.component.html',
  styleUrls: ['./articlereviewedspecificpage.component.css']
})
export class ArticlereviewedspecificpageComponent implements OnInit {

  @Input() Articlesrc : string=`${application.URL}/Article/GetArticleById?ArticleId=11`;
  
  totalLength :any;
  page : number= 1;
 
  constructor(private http: HttpClient){}
 
  ngOnInit(): void {
    this.http
    .get<any>(this.Articlesrc)
    .subscribe((data)=>{
      this.data =data;
      this.totalLength=data.length;
      console.log(data);
    });
  }
  public data:Article=new Article();
}