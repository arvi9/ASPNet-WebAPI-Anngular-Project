import { Component,Input, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Article } from 'Models/Article';
import { application } from 'Models/Application'
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-articlereviewedspecificpage',
  templateUrl: './articlereviewedspecificpage.component.html',
  styleUrls: ['./articlereviewedspecificpage.component.css']
})
export class ArticlereviewedspecificpageComponent implements OnInit {
  articleId: number = 0
  

 
  constructor(private route: ActivatedRoute, private http: HttpClient,private routes:Router) { }
  ngOnInit(): void {
    if(AuthService.GetData("token")==null) this.routes.navigateByUrl("")
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    this.route.params.subscribe(params => {
      this.articleId = params['articleId'];
    console.log(this.articleId)
    this.http
      .get<any>(`${application.URL}/Article/GetArticleById?ArticleId=${this.articleId}`,{headers:headers})
      .subscribe({next:(data) => {
        this.data = data;
        console.log(data);
      }});
    });
  }
  public data:Article=new Article();
}