import { Component, OnInit } from '@angular/core';
import { Article } from 'Models/Article';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { Toaster } from 'ngx-toast-notifications';
import { ConnectionService } from 'src/app/Services/connection.service';

@Component({
  selector: 'app-specificarticle',
  templateUrl: './specificarticle.component.html',
  styleUrls: ['./specificarticle.component.css']
})

export class SpecificarticleComponent implements OnInit {
  articleDetails: any = this.route.params.subscribe(params => {
    this.articleId = params['articleId'];
  });
  articleId: number = this.articleDetails;
  public data: any = new Article();
  likeCount = 0;
  isLiked = false;
  isReadMore = false
  iReadMore = true
  createdOn="";

  article: any = {
    articleCommentId: 0,
    comment: '',
    datetime: new Date(),
    userId: 1,
    createdBy: 1,
    articleId: 0,
    createdOn: new Date(),
    updatedBy: 0,
    updatedOn: '',
    publishedDate:''
  }

  like: any = {
    likeId: 0,
    articleId: this.article.articleId,
    userId: 10,
  }

  constructor(private routing: Router, private route: ActivatedRoute, private connection: ConnectionService, private toaster: Toaster) { }

  //Get Specific article by its id.
  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.routing.navigateByUrl("")
    this.route.params.subscribe(params => {
      this.articleId = params['articleId'];
      this.connection.GetArticle(this.articleId)
        .subscribe({
          next: (data: any) => {
            this.data = data;
            console.log(data)
          }
        });
    });
  }
  
  

  //Add like to article.
  likeTheButton = () => {
    this.like.articleId = this.articleId;
    this.connection.AddLike(this.like)
      .subscribe({
        next: (data) => {
          this.data.likes = data.likesCount
        }
      });
  }

  // Add comment to the article.
  PostComment() {
    this.article.articleId = this.articleId;
    console.log(this.article)
    this.connection.PostArticleComment(this.article)
      .subscribe({
        next: (data) => {
        }
      });
    this.toaster.open({ text: 'Comment Posted successfully', position: 'top-center', type: 'success' })
    setTimeout(
      () => {
        location.reload(); // the code to execute after the timeout
      },
      1000// the time to sleep to delay for
    );
  }

  showText() {
    this.isReadMore = !this.isReadMore
  }
  Text() {
    this.iReadMore = !this.iReadMore
  }
}
