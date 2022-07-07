import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Article } from 'Models/Article';
import { AuthService } from 'src/app/Services/auth.service';
import { ConnectionService } from 'src/app/Services/connection.service';

@Component({
  selector: 'app-toreviewpage',
  templateUrl: './toreviewpage.component.html',
  styleUrls: ['./toreviewpage.component.css']
})
export class ToreviewpageComponent implements OnInit {
  constructor(private connection: ConnectionService, private route: Router) { }
  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.route.navigateByUrl("")
    if (!AuthService.GetData("Reviewer")) {
      this.route.navigateByUrl("")
    }
    // Get to review articles.
    this.connection.GetToReviewArticles()
      .subscribe({
        next: (data: Article[]) => {
          this.data1 = data;

        // Get under review articles.
          this.connection.GetUnderReviewArticles()
          .subscribe({
            next: (data: Article[]) => {
              this.data=this.data1.concat(data)
              console.log(this.data)
            }
          });
        }
      });
  }

  

  public data: Article[] = [];
  public data1: Article[] = [];
}
