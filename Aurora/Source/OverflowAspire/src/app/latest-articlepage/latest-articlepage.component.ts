import { Component, Input, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Article } from 'Models/Article';
import { AuthService } from '../auth.service';
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
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    this.http
      .get<any>(this.artsrc,{headers:headers})
      .subscribe((data) => {
        this.data = data;
        this.totalLength = data.length;
        console.log(data)
       
      });
}public data: Article[] = [

 
];

}