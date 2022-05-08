import { Component, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Article } from 'Models/Article';
import { data } from 'jquery';
@Component({
  selector: 'app-specificarticle',
  templateUrl: './specificarticle.component.html',
  styleUrls: ['./specificarticle.component.css']
})
export class SpecificarticleComponent implements OnInit {
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
 createdOn:string = this.data.createdOn.toDateString()
  likeCount = 0;
  isLiked = false;

  likeTheButton = () => {
    if (this.isLiked)
      this.likeCount--;
    else
      this.likeCount++;

    this.isLiked = !this.isLiked
  }
  isReadMore = true

  showText() {
    this.isReadMore = !this.isReadMore
  }
  iReadMore = true

  Text() {
    this.iReadMore = !this.iReadMore
  }
}