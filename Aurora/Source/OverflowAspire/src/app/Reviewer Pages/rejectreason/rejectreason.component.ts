import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Toaster } from 'ngx-toast-notifications';
import { AuthService } from 'src/app/Services/auth.service';
import { ConnectionService } from 'src/app/Services/connection.service';

@Component({
  selector: 'app-rejectreason',
  templateUrl: './rejectreason.component.html',
  styleUrls: ['./rejectreason.component.css']
})
export class RejectreasonComponent implements OnInit {
  articleid = 0;
  error = ""
  article: any = {
    title: '',
    content: '',
    articlestatusid: 1,
    Reason: "",
  }

  constructor(private routing: Router, private route: ActivatedRoute, private connection: ConnectionService, private toaster: Toaster) { }

  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.routing.navigateByUrl("")
    this.route.params.subscribe(params => {
      this.articleid = params['articleId'];
      this.connection.GetArticle(this.articleid)
        .subscribe((data: any) => {
          this.article = data;
          console.log(this.article)
        });
    });
  }

  UpdateReason() {
    this.article.articlestatusid = 1
    this.connection.UpdateArticle(this.article)
      .subscribe({
        next: (data) => {
        },
        error: (error) => {
          this.error = error.error.message;
        },
        complete: () => {
          this.toaster.open({ text: 'Article Rejected and reason added successfully', position: 'top-center', type: 'warning' })
          this.routing.navigateByUrl("/ToReview");
        }
      });
  }
}



