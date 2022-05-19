import { Component, OnInit } from '@angular/core';
import { application } from 'Models/Application';


@Component({
  selector: 'app-queries',
  templateUrl: './queries.component.html',
  styleUrls: ['./queries.component.css']
})
export class QueriesComponent implements OnInit {

Url:string =`${application.URL}/Query/GetAll`
  constructor() { }

 ngOnInit(): void {
   this.ngOnInit
 }
}
