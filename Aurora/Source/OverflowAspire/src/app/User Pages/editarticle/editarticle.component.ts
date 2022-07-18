import { Component, Input, OnInit } from '@angular/core';
import { Article } from 'Models/Article';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { ConnectionService } from 'src/app/Services/connection.service';
import { Toaster } from 'ngx-toast-notifications';

@Component({
  selector: 'app-editarticle',
  templateUrl: './editarticle.component.html',
  styleUrls: ['./editarticle.component.css']
})
export class EditarticleComponent implements OnInit {
  articleId: number = 0
  @Input() ShowStatus: boolean = true;
  public data: Article = new Article();
  likeCount = 0;
  isLiked = false;
  isReadMore = true;
  iReadMore = true
  error=""
  article: any = {
    articleCommentId: 0,
    comment: '',
    userId: 1,
    createdBy: 1,
    articleId: 2,
    updatedBy: 0,
    updatedOn: '',
    reviewerId:0,
    publishedDate:new Date()
  }
  like: any = {
    likeId: 0,
    articleId: 1,
    userId: 2,
  }

  constructor(private route: ActivatedRoute,  private routes: Router, private connection: ConnectionService,private toaster: Toaster) { }

  //Get article by article id.
  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.routes.navigateByUrl("")
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

  DeleteArticle(articleId: number){
    this.connection.DeleteArticle(articleId)
    .subscribe({
      next: () => {
      },
      error: (error:any)=> {
        this.error = error.error.message;
      },
      complete:()=>{
        this.toaster.open({ text: 'Article Deleted successfully', position: 'top-center', type: 'danger' })
        this.routes.navigateByUrl("/MyArticles");
      }
    });
  }
  

  showText() {
    this.isReadMore = !this.isReadMore
  }
  Text() {
    this.iReadMore = !this.iReadMore
  }
}
