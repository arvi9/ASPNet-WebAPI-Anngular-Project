import { Component, Input, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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
  @Input()  ShowStatus:boolean=true;
  constructor(private route: ActivatedRoute, private http: HttpClient,private routes:Router,private connection:ConnectionService) { }
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
  if(AuthService.GetData("token")==null) this.routes.navigateByUrl("")
  this.route.params.subscribe(params => {
  this.articleId = params['articleId'];
  this.connection.GetArticle(this.articleId)
    .subscribe({next:(data: Article) => {
      this.data = data;
    }});
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
