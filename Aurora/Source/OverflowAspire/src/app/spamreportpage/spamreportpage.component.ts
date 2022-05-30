import { Component, Input,OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { application } from 'Models/Application';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-spamreportpage',
  templateUrl: './spamreportpage.component.html',
  styleUrls: ['./spamreportpage.component.css']
})
export class SpamreportpageComponent implements OnInit {

  @Input() QuerySrc : string=`${application.URL}/Query/GetListOfSpams`;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    this.http
    .get<any>(this.QuerySrc,{headers:headers})
    .subscribe((data)=>{
      this.data =data;
      console.log(data);
    });
  }

  public data: any[] = []
 
 
    
  

}
