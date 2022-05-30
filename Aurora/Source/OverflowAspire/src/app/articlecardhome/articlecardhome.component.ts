import { Component, OnInit,Input } from '@angular/core';
import { Article } from 'Models/Article';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { application } from 'Models/Application';
import { AuthService } from '../auth.service';


@Component({
  selector: 'app-articlecardhome',
  templateUrl: './articlecardhome.component.html',
  styleUrls: ['./articlecardhome.component.css']
})
export class ArticlecardhomeComponent implements OnInit {

 
  @Input() artsrc: string ="";
  

 

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
        
        console.log(data)
       
      });
}
public data: Article[] = [

 
];

}
