import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-articalreviewedpage',
  templateUrl: './articalreviewedpage.component.html',
  styleUrls: ['./articalreviewedpage.component.css']
})
export class ArticalreviewedpageComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  public data: any[] = [{ArticleID:'A1',AuthorName:'Sandy',ArticleName:'Nature',PublishedDate:'02-09-2008'},
  {ArticleID:'A1',AuthorName:'irfan',ArticleName:'Nature',PublishedDate:'02-09-2008'},
  {ArticleID:'A1',AuthorName:'Sandy',ArticleName:'Nature',PublishedDate:'02-09-2008'},
  {ArticleID:'A1',AuthorName:'ranjith',ArticleName:'Nature',PublishedDate:'02-09-2008'},
  {ArticleID:'A1',AuthorName:'Sandy',ArticleName:'Nature',PublishedDate:'02-09-2008'},
 

 
  ];
}
