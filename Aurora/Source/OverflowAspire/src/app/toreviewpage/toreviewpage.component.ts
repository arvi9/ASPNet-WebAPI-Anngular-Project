import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-toreviewpage',
  templateUrl: './toreviewpage.component.html',
  styleUrls: ['./toreviewpage.component.css']
})
export class ToreviewpageComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  public data: any[] = [{ArticleID:'1',AuthorName:'jin',ArticleTitle:'Rising Sun',Status:'Click Here To Review',ReviewerName:'-'},
  {ArticleID:'2',AuthorName:'Ranjith',ArticleTitle:'Happiness',Status:'Click Here To Review',ReviewerName:'-'},
  {ArticleID:'3',AuthorName:'Irfan',ArticleTitle:'Master of the Universe',Status:'Under Review',ReviewerName:'Ranjith'},
  {ArticleID:'4',AuthorName:'Mohammed',ArticleTitle:'The art of the Steal',Status:'Click Here To Review',ReviewerName:'-'},
  {ArticleID:'5',AuthorName:'Kumar',ArticleTitle:'Music is an art',Status:'Under Review',ReviewerName:'Irfan'},
  
  

 
  ];
}
