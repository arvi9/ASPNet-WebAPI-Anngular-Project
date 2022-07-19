import { Component, OnInit, Input } from '@angular/core';
import { Article } from 'Models/Article';
import { AuthService } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';
import { ConnectionService } from 'src/app/Services/connection.service';


@Component({
  selector: 'app-article-card',
  templateUrl: './article-card.component.html',
  styleUrls: ['./article-card.component.css']
})
export class ArticleCardComponent implements OnInit {
  @Input() artsrc: string = '';
  @Input() ShowStatus: boolean = true;
  totalLength: any;
  page: number = 1;
  searchTitle = "";
  searchAuthor = "";
  FromDate = new Date("0001-01-01");
  ToDate = new Date("0001-01-01");
  public data: Article[] = [];
  public filteredData: Article[] = [];

  constructor(private connection: ConnectionService, private route: Router) { }

  //Get articles
  ngOnInit(): void {
    //Get all the articles
    if (this.artsrc == "allArticles") {
      this.connection.GetAllArticles()
        .subscribe({
          next: (data) => {
            this.data = data;
            this.filteredData = data;
            this.totalLength = data.length;
          }
        });
    }

    //Get latest articles.
    else if (this.artsrc == "latestArticles") {
      this.connection.GetLatestArticles()
        .subscribe({
          next: (data) => {
            this.data = data;
            this.filteredData = data;
            this.totalLength = data.length;
          }
        });
    }
    //Get private articles
    else if (this.artsrc == "PrivateArticles") {
      this.connection.GetPrivateArticles()
        .subscribe({
          next: (data: any[]) => {
            this.data = data
            this.filteredData = data;
            this.totalLength = data.length;
          }
        });
    }

    // Get Trending articles.
    else if (this.artsrc == "trendingArticles")
    {
      this.connection.GetTrendingArticles()
        .subscribe({
          next: (data) => {
            this.data = data;
            this.filteredData = data;
            this.totalLength = data.length;
          }
        });
    }
  }

  //Filter articles by Title,Author,Fromdate and Todate.
  samplefun(searchTitle: string, searchAuthor: string, FromDate: any, ToDate: any) {
    if (searchTitle.length == 0 && searchAuthor.length == 0 && FromDate == new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) this.data = this.filteredData

    //1.Search by title
    if (searchTitle.length != 0 && searchAuthor == '' && FromDate == new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => item.title.toLowerCase().includes(searchTitle.toLowerCase()));
    }
    //2.Search by Author
    else if (searchTitle == '' && searchAuthor.length != 0 && FromDate == new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => item.authorName.toLowerCase().includes(searchAuthor.toLowerCase()));
    }
    //3.Search by FromDate
    else if (searchTitle == '' && searchAuthor == '' && FromDate != new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => new Date(item.date) >= new Date(FromDate));
    }
    //4.Search by ToDate
    else if (searchTitle == '' && searchAuthor == '' && FromDate == new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => new Date(item.date) <= new Date(ToDate));
    }
    //5.search by title and author
    else if (searchTitle.length != 0 && searchAuthor.length != 0 && FromDate == new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && item.authorName.toLowerCase().includes(searchAuthor.toLowerCase()) });
    }
    //6.search by title and fromdate
    else if (searchTitle.length != 0 && searchAuthor == '' && FromDate != new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && new Date(item.date) >= new Date(FromDate) });
    }
    //7.search by title and Todate
    else if (searchTitle.length != 0 && searchAuthor == '' && FromDate == new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && new Date(item.date) <= new Date(ToDate) });
    }
    //8.search by author and fromdate
    else if (searchTitle == '' && searchAuthor.length != 0 && FromDate != new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return item.authorName.toLowerCase().includes(searchAuthor.toLowerCase()) && new Date(item.date) >= new Date(FromDate) });
    }
    //9.search by author and todate
    else if (searchTitle == '' && searchAuthor.length != 0 && FromDate == new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return item.authorName.toLowerCase().includes(searchAuthor.toLowerCase()) && new Date(item.date) <= new Date(ToDate) });
    }
    //10.search by fromdate and todate
    else if (searchTitle == '' && searchAuthor == '' && FromDate != new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return new Date(item.date) >= new Date(FromDate) && new Date(item.date) <= new Date(ToDate) });
    }
    //11.search by author,title and fromdate
    else if (searchTitle.length != 0 && searchAuthor.length != 0 && FromDate != new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && item.authorName.toLowerCase().includes(searchAuthor.toLowerCase()) && new Date(item.date) >= new Date(FromDate) });
    }
    //12.search by author,title and Todate
    else if (searchTitle.length != 0 && searchAuthor.length != 0 && FromDate == new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && item.authorName.toLowerCase().includes(searchAuthor.toLowerCase()) && new Date(item.date) <= new Date(ToDate) });
    }
    //13.search by author,Fromdate and Todate
    else if (searchTitle == '' && searchAuthor.length != 0 && FromDate != new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return item.authorName.toLowerCase().includes(searchAuthor.toLowerCase()) && new Date(item.date) >= new Date(FromDate) && new Date(item.date) <= new Date(ToDate) });
    }
    //14.search by Title,Fromdate and Todate
    else if (searchTitle.length != 0 && searchAuthor == '' && FromDate != new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && new Date(item.date) >= new Date(FromDate) && new Date(item.date) <= new Date(ToDate) });
    }
    //15.search by Title,Author,Fromdate and Todate
    else if (searchTitle.length != 0 && searchAuthor.length != 0 && FromDate != new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && item.authorName.toLowerCase().includes(searchAuthor.toLowerCase()) && new Date(item.date) >= new Date(FromDate) && new Date(item.date) <= new Date(ToDate) });
    }
    this.searchTitle = '';
    this.searchAuthor = '';
    this.FromDate = new Date("0001-01-01");
    this.ToDate = new Date("0001-01-01");
  }
}
