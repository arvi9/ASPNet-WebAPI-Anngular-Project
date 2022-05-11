import { Component, OnInit } from '@angular/core';
import { application } from 'Models/Application';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {

  public UrlString:string=application.URL
  constructor() { }

  ngOnInit(): void {
  }

}
