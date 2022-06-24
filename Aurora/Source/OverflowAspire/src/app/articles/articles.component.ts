import { Component, Input,OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Article } from 'Models/Article';
import { application } from 'Models/Application';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.css']
})
export class ArticlesComponent implements OnInit {
url:string=`${application.URL}/Article/GetArticlesByArticleStatusId?ArticleStatusID=4`;
  @Input() artsrc: string =`${application.URL}/Article/GetAll`;
  totalLength: any;
  page: number = 1;
  public data: Article[] = [
  ]
 

  constructor(private http: HttpClient,private route:Router) { }
  ngOnInit(): void {
    if(AuthService.GetData("token")==null) this.route.navigateByUrl("")
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    this.http
      .get<any>(this.artsrc,{headers:headers})
      .subscribe({next:(data) => {
        this.data = data;
        this.totalLength = data.length;
        console.log(data)
       
      }});
      
}

}