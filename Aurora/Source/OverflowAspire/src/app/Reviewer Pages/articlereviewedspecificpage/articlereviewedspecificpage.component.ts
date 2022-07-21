import { Component, OnInit } from '@angular/core';
import { Article } from 'Models/Article';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { ConnectionService } from 'src/app/Services/connection.service';
import { Toaster } from 'ngx-toast-notifications';

@Component({
  selector: 'app-articlereviewedspecificpage',
  templateUrl: './articlereviewedspecificpage.component.html',
  styleUrls: ['./articlereviewedspecificpage.component.css']
})

export class ArticlereviewedspecificpageComponent implements OnInit {
  articleId: number = 0
  public data: Article = new Article();
  error:any

  constructor(private route: ActivatedRoute, private routes: Router, private connection: ConnectionService,private toaster: Toaster,) { }

  //Get article by article id.
  ngOnInit(): void {
    if (AuthService.GetData("token") == null) {
      this.toaster.open({ text: 'Your Session has been Expired', position: 'top-center', type: 'warning' })
      this.routes.navigateByUrl("")
    }
    if (!AuthService.IsReviewer()) {
      this.routes.navigateByUrl("")
    }
    this.route.params.subscribe(params => {
      this.articleId = params['articleId'];
      this.connection.GetArticle(this.articleId)
        .subscribe({
          next: (data: Article) => {
            this.data = data;
          },
          error: (error:any) => this.error = error.error.message,
        });
    });
  }
}