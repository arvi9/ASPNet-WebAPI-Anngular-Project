import { Component, OnInit } from '@angular/core';
import { Article } from 'Models/Article';
import { AuthService } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';
import { ConnectionService } from 'src/app/Services/connection.service';
import { Toaster } from 'ngx-toast-notifications';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-articalreviewedpage',
  templateUrl: './articalreviewedpage.component.html',
  styleUrls: ['./articalreviewedpage.component.css']
})

export class ArticalreviewedpageComponent implements OnInit {
  constructor(private route: Router, private connection: ConnectionService, private toaster: Toaster,private spinner: NgxSpinnerService) { }
  public data: Article[] = [];
  isSpinner = true;

  // Reviewer can get reviewed articles.
  ngOnInit(): void {
    this.spinner.show();
    if (AuthService.GetData("token") == null) {
      this.toaster.open({ text: 'Your Session has been Expired', position: 'top-center', type: 'warning' })
      this.route.navigateByUrl("")
    }
    if (!AuthService.IsReviewer()) {
      this.route.navigateByUrl("")
    }
    this.connection.GetReviewedArticles()
      .subscribe({
        next: (data: Article[]) => {
          this.data = data;
        },
        complete: () => {
          this.isSpinner = false;
          this.spinner.hide();
        }
      });
  }
}
