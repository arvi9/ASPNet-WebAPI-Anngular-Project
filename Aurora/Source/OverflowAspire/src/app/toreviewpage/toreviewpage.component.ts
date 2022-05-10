import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { Article } from 'Models/Article';

@Component({
  selector: 'app-toreviewpage',
  templateUrl: './toreviewpage.component.html',
  styleUrls: ['./toreviewpage.component.css']
})
export class ToreviewpageComponent implements OnInit {
  @Input() Usersrc : string="https://localhost:7197/Article/GetArticlesByArticleStatusId?ArticleStatusID=2";
  
  totalLength :any;
  page : number= 1;
 
  constructor(private http: HttpClient){}
 
  ngOnInit(): void {
    this.http
    .get<any>(this.Usersrc)
    .subscribe((data)=>{
      this.data =data;
      this.totalLength=data.length;
      console.log(data);
    });
  }
  public data: Article[] = [
 

 
  ];
 
  
}
