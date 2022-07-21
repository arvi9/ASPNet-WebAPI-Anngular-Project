import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Article } from 'Models/Article';
import { User } from 'Models/User';
import { NgxSpinnerService } from 'ngx-spinner';
import { Toaster } from 'ngx-toast-notifications';
import { AuthService } from 'src/app/Services/auth.service';
import { ConnectionService } from 'src/app/Services/connection.service';

@Component({
  selector: 'app-toreviewpage',
  templateUrl: './toreviewpage.component.html',
  styleUrls: ['./toreviewpage.component.css']
})

export class ToreviewpageComponent implements OnInit {
  public data: Article[] = [];
  public data1: Article[] = [];
  userid: any;
  isSpinner = true;
  error:any
  constructor(private connection: ConnectionService, private route: Router, private toaster: Toaster, private spinner: NgxSpinnerService) { }

  ngOnInit(): void {

    if (AuthService.GetData("token") == null) {
      this.toaster.open({ text: 'Your Session has been Expired', position: 'top-center', type: 'warning' })
      this.route.navigateByUrl("")
    }
    if (!AuthService.IsReviewer()) {
      this.route.navigateByUrl("")
    }
    this.spinner.show();
    // Get to review articles.
    this.connection.GetToReviewArticles()
      .subscribe({
        next: (data: Article[]) => {
          this.data1 = data;
          // Get under review articles.
          this.connection.GetUnderReviewArticles()
            .subscribe({
              next: (data: Article[]) => {
                this.data = this.data1.concat(data)
              },
              error: (error:any) => this.error = error.error.message,
              complete: () => {
                this.isSpinner = false;
                this.spinner.hide();
              }
            });
        }
      });
      this.connection.GetCurrentApplicationUser()
      .subscribe({
        next: (data: User) => {
          this.userid = data.userId;
        },
        error: (error:any) => this.error = error.error.message,
      });
  }
}
