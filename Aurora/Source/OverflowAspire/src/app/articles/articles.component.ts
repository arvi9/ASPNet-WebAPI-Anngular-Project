import { Component, Input,OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Article } from 'Models/Article';
import { application } from 'Models/Application';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.css']
})
export class ArticlesComponent implements OnInit {

  @Input() artsrc: string =`${application.URL}/Article/GetAll`;
  totalLength: any;
  page: number = 1;
  public data: Article[] = [

 
  ]
 

  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    this.http
      .get<any>(this.artsrc)
      .subscribe((data) => {
        this.data = data;
        this.totalLength = data.length;
        console.log(data)
       
      });
      
}

}