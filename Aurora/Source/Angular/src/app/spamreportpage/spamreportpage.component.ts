import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-spamreportpage',
  templateUrl: './spamreportpage.component.html',
  styleUrls: ['./spamreportpage.component.css']
})
export class SpamreportpageComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  public data: any[] = [{QueryID:'Q1',QueryTitle:'Web API',Reports:'8'},
  {QueryID:'Q2',QueryTitle:'Web API',Reports:'8'},
  {QueryID:'Q3',QueryTitle:'Web API',Reports:'8'},
  {QueryID:'Q1',QueryTitle:'Web API',Reports:'8'},
  {QueryID:'Q4',QueryTitle:'Web API',Reports:'8'},
  {QueryID:'Q5',QueryTitle:'Web API',Reports:'8'},
  {QueryID:'Q9',QueryTitle:'Web API',Reports:'8'},
  

 
  ];

}
