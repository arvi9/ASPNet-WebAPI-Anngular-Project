import { Component, OnInit, Input } from '@angular/core';
import { Article } from 'Models/Article';
import { HttpClient } from '@angular/common/http';
import { data } from 'jquery';


@Component({
  selector: 'app-article-card',
  templateUrl: './article-card.component.html',
  styleUrls: ['./article-card.component.css']
})
export class ArticleCardComponent implements OnInit {
  @Input() artsrc: string = "";
  totalLength: any;
  page: number = 1;
  searchTitle = "";
  searchAuthor = "";
  FromDate="";
  ToDate="";

  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    this.http
      .get<any>(this.artsrc)
      .subscribe((data) => {
        this.data = data;
        this.filteredData = data;
        this.totalLength = data.length;
        console.log(data)

      });
  }
  public data: Article[] = [

  ];

  public filteredData: Article[] = [];
  samplefun(searchTitle: string, searchAuthor: string,FromDate:String,ToDate:String) {
    console.log(searchAuthor)
    console.log(searchTitle)
    if (searchTitle.length == 0) this.data = this.filteredData


    if (searchTitle.length != 0 && searchAuthor == '') {
      this.data = this.filteredData.filter(item => item.title.toLowerCase().includes(searchTitle.toLowerCase()));
    }
    else if (searchTitle == '' && searchAuthor.length != 0) {
      this.data = this.filteredData.filter(item => item.authorName.toLowerCase().includes(searchAuthor.toLowerCase()));
    }
    
    else if (searchTitle.length != 0 && searchAuthor.length != 0) {
      this.data = this.filteredData.filter(item =>{return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && item.authorName.toLowerCase().includes(searchAuthor.toLowerCase())});

    }
  }
}
