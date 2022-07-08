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

  constructor(private routing: Router, private route: ActivatedRoute, private connection: ConnectionService, private toaster: Toaster) { }

  article: any = {
    articleCommentId: 0,
    comment: '',
    datetime: Date.now,
    userId: 1,
    createdBy: 1,
    articleId: 0,
    createdOn: Date.now,
    updatedBy: 0,
    updatedOn: '',
  }

  like: any = {
    likeId: 0,
    articleId: this.article.articleId,
    userId: 10,

  }

  //Get Specific article by its id.

  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.routing.navigateByUrl("")
    this.route.params.subscribe(params => {
      this.articleId = params['articleId'];
      this.connection.GetArticle(this.articleId)
      .subscribe({
        next: (data: Article) => {
          this.data = data;
          console.log(data)
        }
      });
    });
  }

 


  public data: Article = new Article();
  createdOn: string = this.data.date.toDateString()
  likeCount = 0;
  isLiked = false;

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

  isReadMore = false
  showText() {
    this.isReadMore = !this.isReadMore
  }
  iReadMore = true
  Text() {
    this.iReadMore = !this.iReadMore
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
   this.ngOnInit();
  }
}
