import { Component,OnInit } from '@angular/core';
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
    if (!AuthService.IsAdmin()) this.route.navigateByUrl("error")
    this.connection.GetToReviewArticles()
      .subscribe({
        next: (data: Article[]) => {
          this.data = data;
        }
      });
  }

  button: string = 'To review'
  clicked = false;
  actionMethod() {
    if (this.clicked) {
      this.button = 'To Review'
    }
    else {
      this.button = 'Under Review'
    }
  }
  public data: Article[] = [];
}
