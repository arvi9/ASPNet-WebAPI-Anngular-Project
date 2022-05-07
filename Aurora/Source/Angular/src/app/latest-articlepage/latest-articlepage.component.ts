import { Component, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Article } from 'Models/Article';
@Component({
  selector: 'app-latest-articlepage',
  templateUrl: './latest-articlepage.component.html',
  styleUrls: ['./latest-articlepage.component.css']
})
export class LatestArticlepageComponent implements OnInit {


  @Input() artsrc: string ="https://localhost:7197/Article/GetLatestArticles";
  totalLength: any;
  page: number = 1;
 

  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    this.http
      .get<any>(this.artsrc)
      .subscribe((data) => {
        this.data = data;
        this.totalLength = data.length;
        console.log(data)
       
      });
}public data: Article[] = [

 
];

}