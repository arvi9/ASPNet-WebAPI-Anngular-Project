import { Component, Input, OnInit } from '@angular/core';
import { QueryService } from '../query.service';
import{Query} from '../query'
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { application } from 'Models/Application';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-report-spam',
  templateUrl: './report-spam.component.html', 
  styleUrls: ['../specificquery/specificquery.component.css'],
  providers:[QueryService]
})
export class ReportSpamComponent implements OnInit { 
 @Input() Querysrc: string = "https://localhost:7197/Query/GetQuery?QueryId=1";
  data: any;
  queryId: number = 0

 
  constructor(private route: ActivatedRoute, private http: HttpClient) { }
  reportspam: any = {
    spamId: 0,
    reason: '',
    queryId:3,
    userId: 1,
    verifyStatusID: 3,
    
  
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.queryId = params['queryId'];
    console.log(this.queryId)
    this.http
      .get<any>(`https://localhost:7197/Query/GetQuery?QueryId=${this.queryId}`)
      .subscribe((data) => {
        this.data = data;
        console.log(data);
      });
    });
  }
  spamreport(){
    const headers = { 'content-type': 'application/json' }
    console.log(this.reportspam)
    this.http.post<any>(`${application.URL}/Query/AddSpam`, this.reportspam, { headers: headers })
      .subscribe((data) => {

        console.log(data)

      });
  }
  
}
