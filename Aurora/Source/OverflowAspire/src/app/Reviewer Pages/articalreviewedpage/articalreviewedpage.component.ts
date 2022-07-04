import { Component, Input, OnInit } from '@angular/core';
import { Article } from 'Models/Article';
import { AuthService } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';
import { ConnectionService } from 'src/app/Services/connection.service';

@Component({
  selector: 'app-articalreviewedpage',
  templateUrl: './articalreviewedpage.component.html',
  styleUrls: ['./articalreviewedpage.component.css']
})
export class ArticalreviewedpageComponent implements OnInit {

  constructor(private route: Router, private connection: ConnectionService) { }
  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.route.navigateByUrl("")
    if (!AuthService.GetData("Reviewer")) {
      this.route.navigateByUrl("")
    }
    this.connection.GetReviewedArticles()
      .subscribe({
        next: (data: Article[]) => {
          this.data = data;
        }
      });
  }

  public data: Article[] = [];
}