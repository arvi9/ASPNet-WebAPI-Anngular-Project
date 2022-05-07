import { Component, OnInit } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { User } from 'Models/User';
@Component({
  selector: 'app-employee-page',
  templateUrl: './employee-page.component.html',
  styleUrls: ['./employee-page.component.css']
})
export class EmployeePageComponent implements OnInit {


  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    // this.http
    // .get<any>(this.)
    // .subscribe((data) => {
    // this.data = data;

  }
  public data: any[] = [{name:'mani',email:'mani@gmail.com',aceno:'ace5656'},
  {name:'mani',email:'mani@gmail.com',aceno:'yuui5656'},
  {name:'mani',email:'mani@gmail.com',aceno:'ace5656'},
  {name:'mani',email:'mani@gmail.com',aceno:'ace5656'},
  {name:'mani',email:'mani@gmail.com',aceno:'ace5656'},
  {name:'mani',email:'mani@gmail.com',aceno:'ace5656'},
  {name:'gowtham',email:'mani@gmail.com',aceno:'ace5656'},
  {name:'mani',email:'mani@gmail.com',aceno:'yuui5656'},
  {name:'mani',email:'mani@gmail.com',aceno:'ace5656'},
  {name:'mani',email:'mani@gmail.com',aceno:'ace5656'},
  {name:'mani',email:'mani@gmail.com',aceno:'ace5656'},
  {name:'mani',email:'mani@gmail.com',aceno:'ace5656'},
  {name:'gowtham',email:'mani@gmail.com',aceno:'ace5656'}

 
  ];
    
  

}
