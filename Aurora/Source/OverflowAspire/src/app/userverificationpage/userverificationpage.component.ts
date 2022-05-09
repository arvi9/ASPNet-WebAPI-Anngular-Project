import { HttpClient } from '@angular/common/http';
import { Component, OnInit,Input } from '@angular/core';
import { User } from 'Models/User';

@Component({
  selector: 'app-userverificationpage',
  templateUrl: './userverificationpage.component.html',
  styleUrls: ['./userverificationpage.component.css']
})
export class UserverificationpageComponent implements OnInit {

  @Input() Usersrc : string="https://localhost:7197/User/GetUsersByVerifyStatusId?VerifyStatusID=3";
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http
    .get<any>(this.Usersrc)
    .subscribe((data)=>{
      this.data =data;
      console.log(data);
    });
  }
  public data: User[] = [
  
  
  ];
}
