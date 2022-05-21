import { Component, OnInit, Input } from '@angular/core';
import { Article } from 'Models/Article';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-my-articles',
  templateUrl: './my-articles.component.html',
  styleUrls: ['./my-articles.component.css']
})
export class MyArticlesComponent implements OnInit {

  @Input()  ShowStatus:boolean=true;
 
  @Input() artsrc: string = "https://localhost:7197/Article/GetArticlesByUserId?UserId=1";
  totalLength: any;
  page: number = 1;
  searchTitle = "";
  searchAuthor = "";
  FromDate = new Date("0001-01-01");
  ToDate = new Date("0001-01-01");
userId:any=0;

  constructor(private route: ActivatedRoute,private http: HttpClient) { }
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.userId = params['UserId'];
    console.log(this.userId)
    this.http
      .get<any>(this.artsrc)
      .subscribe((data) => {
        this.data = data;
        this.filteredData = data;
        this.totalLength = data.length;
      });
    });
  }
  public data: Article[] = [

  ];

  public filteredData: Article[] = [];
  samplefun(searchTitle: string, FromDate: any, ToDate: any) {

    if (searchTitle.length == 0 &&  FromDate == new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) this.data = this.filteredData


    //1.Search by title
    if (searchTitle.length != 0 &&  FromDate == new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      console.log("title")
      this.data = this.filteredData.filter(item => item.title.toLowerCase().includes(searchTitle.toLowerCase()));
    }
    //3.Search by FromDate
    else if (searchTitle == '' &&  FromDate != new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      console.log("FromDate")
      this.data = this.filteredData.filter(item => new Date(item.date) >= new Date(FromDate));
    }
    //4.Search by ToDate
    else if (searchTitle == '' && FromDate == new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      console.log("ToDate")
      this.data = this.filteredData.filter(item => new Date(item.date) <= new Date(ToDate));
    }
   
    //6.search by title and fromdate
    else if (searchTitle.length != 0 &&  FromDate != new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      console.log("title&FromDate")
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && new Date(item.date) >= new Date(FromDate) });
    }
    //7.search by title and Todate
    else if (searchTitle.length != 0 &&  FromDate == new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      console.log("title&ToDate")
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && new Date(item.date) <= new Date(ToDate) });
    }
   
    //10.search by fromdate and todate
    else if (searchTitle == '' &&  FromDate != new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      console.log("fromdate&TsearchTitle == ''oDate")
      this.data = this.filteredData.filter(item => { return new Date(item.date) >= new Date(FromDate) && new Date(item.date) <= new Date(ToDate) });
    }
     //14.search by Title,Fromdate and Todate
     else if (searchTitle.length != 0 &&  FromDate != new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      console.log("title,fromdate,Todate")
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && new Date(item.date) >= new Date(FromDate)&&new Date(item.date) <= new Date(ToDate) });
    }
    //14.search by Title,Author,Fromdate and Todate
    else if (searchTitle.length != 0 && FromDate != new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      console.log("title,author,fromdate,Todate")
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase())&& new Date(item.date) >= new Date(FromDate)&&new Date(item.date) <= new Date(ToDate) });
    }

  }


 


}