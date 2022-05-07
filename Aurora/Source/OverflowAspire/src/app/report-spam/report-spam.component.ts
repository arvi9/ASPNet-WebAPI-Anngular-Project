import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { QueryService } from '../query.service';
import{Query} from '../query'
import { HttpClient, HttpClientModule } from '@angular/common/http';
@Component({
  selector: 'app-report-spam',
  templateUrl: './report-spam.component.html', 
  styleUrls: ['../specificquery/specificquery.component.css'],
  providers:[QueryService]
})
export class ReportSpamComponent implements OnInit { 

constructor(private service:QueryService){
 
}
reportspam : Query={

  querytitle:'',
  query:''
  
    }
spamreport(){    
  console.log(this.reportspam.query)
    }
  ngOnInit(): void {

  }
  
  querypara=this.service.getquery();
  querycode=this.service.getcode();
  
Reporthead:string="Reason for Report";
}
