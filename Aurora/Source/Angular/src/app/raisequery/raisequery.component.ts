import { Component, OnInit } from '@angular/core';
import{Query} from '../query'
@Component({
  selector: 'app-raisequery',
  templateUrl: './raisequery.component.html',
  styleUrls: ['../specificquery/specificquery.component.css']
})
export class RaisequeryComponent implements OnInit {

  constructor() { }
  raisequery : Query={

    querytitle:'',
    query:''
    
      }
  RaiseQuery(){    
    console.log(this.raisequery.query)
      }
  ngOnInit(): void {
  }

}
