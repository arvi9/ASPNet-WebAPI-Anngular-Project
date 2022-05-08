import { Component, OnInit,Input } from '@angular/core';
import { Article } from 'Models/Article';
import { HttpClient} from '@angular/common/http';


@Component({
  selector: 'app-articlecardhome',
  templateUrl: './articlecardhome.component.html',
  styleUrls: ['./articlecardhome.component.css']
})
export class ArticlecardhomeComponent implements OnInit {


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
