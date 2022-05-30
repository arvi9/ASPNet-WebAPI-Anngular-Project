import { Component, Input, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Article } from 'Models/Article';
import { User } from 'Models/User';
import { data } from 'jquery';
import { application } from 'Models/Application';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../auth.service';
@Component({
  selector: 'app-specificarticle',
  templateUrl: './specificarticle.component.html',
  styleUrls: ['./specificarticle.component.css']
})
export class SpecificarticleComponent implements OnInit {
  articleId: number = 0
  //@Input() Articlesrc: string = `${application.URL}/Article/GetArticleById/:articleId`;



  constructor(private routing:Router,private route: ActivatedRoute, private http: HttpClient) { }
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


  likeTheButton = () => {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    console.log(this.like)
    this.http.post<any>(`${application.URL}/Article/AddLikeToArticle`, this.like, { headers: headers })
      .subscribe((data) => {
          this.data.likes=data.likesCount
        console.log(data)

      });
     

  }
  isReadMore = true

  showText() {
    this.isReadMore = !this.isReadMore
  }
  iReadMore = true

  Text() {
    this.iReadMore = !this.iReadMore
  }

  PostComment() {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    console.log(this.article)
    this.http.post<any>(`${application.URL}/Article/CreateComment`, this.article, { headers: headers })
      .subscribe((data) => {

        console.log(data)

      });
      this.routing.navigateByUrl("/Home");
  }
}