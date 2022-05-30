import { Component,Input, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Article } from 'Models/Article';
import { application } from 'Models/Application';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-toreviewspecificpage',
  templateUrl: './toreviewspecificpage.component.html',
  styleUrls: ['./toreviewspecificpage.component.css']
})
export class ToreviewspecificpageComponent implements OnInit {
  articleId: number = 0

  
  totalLength :any;
  page : number= 1;
 
  constructor(private route: ActivatedRoute, private http: HttpClient,private routing:Router) { }
 
  ngOnInit(): void {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    this.route.params.subscribe(params => {
      this.articleId = params['articleId'];
    console.log(this.articleId)
    this.http
      .get<any>(`${application.URL}/Article/GetArticleById?ArticleId=${this.articleId}`,{headers:headers})
      .subscribe((data) => {
        this.data = data;
        console.log(data);
      });
    });
  }
  public data:Article=new Article();

  PublishArticle(articleId:number){
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    console.log("ge")
    this.http
    .patch(`${application.URL}/Article/ChangeArticleStatus?ArticleId=${articleId}&ArticleStatusID=4`,Object,{headers:headers})  
    .subscribe((data)=>{
      console.log(data);
    });
    this.routing.navigateByUrl("/ToReview");
   
  }

  RejectArticle(articleId:number){
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    console.log("go")
    this.http
    .patch(`${application.URL}/Article/ChangeArticleStatus?ArticleId=${articleId}&ArticleStatusID=1`,Object,{headers:headers})
    .subscribe((data)=>{
      console.log(data);
    });
    this.routing.navigateByUrl("/ToReview");
  }
}