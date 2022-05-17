import { Component,Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Article } from 'Models/Article';
import { application } from 'Models/Application'
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-articlereviewedspecificpage',
  templateUrl: './articlereviewedspecificpage.component.html',
  styleUrls: ['./articlereviewedspecificpage.component.css']
})
export class ArticlereviewedspecificpageComponent implements OnInit {
  articleId: number = 0
  

 
  constructor(private route: ActivatedRoute, private http: HttpClient) { }
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.articleId = params['articleId'];
    console.log(this.articleId)
    this.http
      .get<any>(`${application.URL}/Article/GetArticleById?ArticleId=${this.articleId}`)
      .subscribe((data) => {
        this.data = data;
        console.log(data);
      });
    });
  }
  public data:Article=new Article();
}