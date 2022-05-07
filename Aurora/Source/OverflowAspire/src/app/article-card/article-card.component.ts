import { Component, OnInit, Input } from '@angular/core';
import { Article } from 'Models/Article';
import { HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-article-card',
  templateUrl: './article-card.component.html',
  styleUrls: ['./article-card.component.css']
})
export class ArticleCardComponent implements OnInit {
  @Input() artsrc: string ="";
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
}
public data: Article[] = [

 
];

}
