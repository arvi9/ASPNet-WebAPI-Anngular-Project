import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-userverificationpage',
  templateUrl: './userverificationpage.component.html',
  styleUrls: ['./userverificationpage.component.css']
})
export class UserverificationpageComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  public data: any[] = [{Name:'Mani',DateOfBirth:'02-12-2009',ACENumber:'ace5656',Role:'SSE',Email:'mani@gmail.com'},
  {Name:'Mani',DateOfBirth:'02-12-2009',ACENumber:'ace5656',Role:'SSE',Email:'mani@gmail.com'},
  {Name:'Mani',DateOfBirth:'02-12-2009',ACENumber:'ace5656',Role:'SSE',Email:'mani@gmail.com'},
  {Name:'Mani',DateOfBirth:'02-12-2009',ACENumber:'ace5656',Role:'SSE',Email:'mani@gmail.com'},
  
  
  
  ];
}
