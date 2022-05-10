import { Component, OnInit } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import{Query} from '../query'

@Component({
  selector: 'app-raisequery',
  templateUrl: './raisequery.component.html',
  styleUrls: ['../specificquery/specificquery.component.css']
})
export class RaisequeryComponent implements OnInit {

  constructor(private http: HttpClient) { }
  raisequery : Query={

    querytitle:'',
    query:'',
    
      }
   RaiseQuery(){    
    console.log(this.raisequery)
     
    
       }
  ngOnInit(): void {
    const headers = { 'content-type': 'application/json'}  
   
    console.log(this.raisequery)
    this.http.post<any>('https://localhost:7197/Query/CreateQuery',this.raisequery,{headers:headers})
      .subscribe((data) => {
       
        console.log(data)
       
      });
  }
}
