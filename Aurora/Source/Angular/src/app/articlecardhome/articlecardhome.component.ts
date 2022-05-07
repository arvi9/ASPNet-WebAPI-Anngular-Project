import { Component, OnInit,Input } from '@angular/core';
import { Article } from 'Models/Article';
import { HttpClient} from '@angular/common/http';


@Component({
  selector: 'app-articlecardhome',
  templateUrl: './articlecardhome.component.html',
  styleUrls: ['./articlecardhome.component.css']
})
export class ArticlecardhomeComponent implements OnInit {


  constructor(private http: HttpClient) { }
  ngOnInit(): void {
   
}
public data:any[]=[{title:"hi",content:"hello"},{title:"hi",content:"hello"},{title:"hi",content:"hello"}];


;

}
