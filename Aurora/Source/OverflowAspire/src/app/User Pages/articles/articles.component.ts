import { Component, Input,OnInit } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Article } from 'Models/Article';
import { AuthService } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.css']
})

 
export class ArticlesComponent implements OnInit {
  url:string="allArticles";

  totalLength: any;
  page: number = 1;
  public data: Article[] = []

  constructor(private http: HttpClient,private route:Router) { }

  ngOnInit(): void {
    if(AuthService.GetData("token")==null) this.route.navigateByUrl("")     
}

}