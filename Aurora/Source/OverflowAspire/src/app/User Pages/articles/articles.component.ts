import { Component,OnInit } from '@angular/core';
import { Article } from 'Models/Article';
import { AuthService } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.css']
})


export class ArticlesComponent implements OnInit {
  url: string = "allArticles";
  public data: Article[] = []

  constructor(private route: Router) { }

  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.route.navigateByUrl("")
  }
}