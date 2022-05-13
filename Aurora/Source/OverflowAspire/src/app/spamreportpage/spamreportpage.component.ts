import { Component, Input,OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { application } from 'Models/Application';

@Component({
  selector: 'app-spamreportpage',
  templateUrl: './spamreportpage.component.html',
  styleUrls: ['./spamreportpage.component.css']
})
export class SpamreportpageComponent implements OnInit {

  @Input() QuerySrc : string=`${application.URL}/Query/GetListOfSpams`;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http
    .get<any>(this.QuerySrc)
    .subscribe((data)=>{
      this.data =data;
      console.log(data);
    });
  }

  public data: any[] = []
 
 
    
  

}
