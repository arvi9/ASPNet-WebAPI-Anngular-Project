import { Component, Input,OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { application } from 'Models/Application';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-spamreportpage',
  templateUrl: './spamreportpage.component.html',
  styleUrls: ['./spamreportpage.component.css']
})
export class SpamreportpageComponent implements OnInit {

  @Input() QuerySrc : string=`${application.URL}/Query/GetListOfSpams`;
 
  constructor(private http: HttpClient,private route:Router) { }
  ngOnInit(): void {
    if(AuthService.GetData("token")==null) this.route.navigateByUrl("")
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    this.http
    .get<any>(this.QuerySrc,{headers:headers})
    .subscribe({next:(data)=>{
      this.data =data;
      console.log(data);
    }});
  }

  public data: any[] = []
 
 
    
  

}
