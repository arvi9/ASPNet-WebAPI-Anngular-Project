import { Component, Input, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Article } from 'Models/Article';
import { User } from 'Models/User';
import { data } from 'jquery';
import { application } from 'Models/Application';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-editarticle',
  templateUrl: './editarticle.component.html',
  styleUrls: ['./editarticle.component.css']
})
export class EditarticleComponent implements OnInit {

  articleId: number = 0
  //@Input() Articlesrc: string = `${application.URL}/Article/GetArticleById/:articleId`;



  constructor(private route: ActivatedRoute, private http: HttpClient) { }
  article: any = {
    articleCommentId: 0,
    comment: '',
    datetime: Date.now,
    userId: 1,
    createdBy: 1,
    articleId: 2,
    createdOn: Date.now,
    updatedBy: 0,
    updatedOn: '',


  }
  like: any = {
    likeId:0,
    articleId:1,
    userId:2,

}


ngOnInit(): void {
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
    .subscribe((data) => {
      this.data = data;
      console.log(data);
    });
  });
}
  public data: Article = new Article();
  createdOn: string = this.data.date.toDateString()
  likeCount = 0;
  isLiked = false;



  isReadMore = false

  showText() {
    this.isReadMore = !this.isReadMore
  }
  iReadMore = true

  Text() {
    this.iReadMore = !this.iReadMore
  }


}
