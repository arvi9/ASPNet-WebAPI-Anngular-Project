import { Component,Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Article } from 'Models/Article';

@Component({
  selector: 'app-toreviewspecificpage',
  templateUrl: './toreviewspecificpage.component.html',
  styleUrls: ['./toreviewspecificpage.component.css']
})
export class ToreviewspecificpageComponent implements OnInit {

  @Input() Articlesrc : string="https://localhost:7197/Article/GetArticleById?ArticleId=11";
  
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