import { Component, Input, OnInit } from '@angular/core';
import { Article } from 'Models/Article';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { ConnectionService } from 'src/app/Services/connection.service';

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
  isReadMore = false
  iReadMore = true
  createdOn: string = this.data.date.toDateString()
  
  article: any = {
    articleCommentId: 0,
    comment: '',
    userId: 1,
    createdBy: 1,
    articleId: 2,
    updatedBy: 0,
    updatedOn: '',
    reviewerId:0
  }
  like: any = {
    likeId: 0,
    articleId: 1,
    userId: 2,
  }

  constructor(private route: ActivatedRoute,  private routes: Router, private connection: ConnectionService) { }

  //Get article by article id.
  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.routes.navigateByUrl("")
    this.route.params.subscribe(params => {
      this.articleId = params['articleId'];
      this.connection.GetArticle(this.articleId)
        .subscribe({
          next: (data: Article) => {
            this.data = data;
          }
        });
    });
  }

  

  showText() {
    this.isReadMore = !this.isReadMore
  }
  Text() {
    this.iReadMore = !this.iReadMore
  }
}
