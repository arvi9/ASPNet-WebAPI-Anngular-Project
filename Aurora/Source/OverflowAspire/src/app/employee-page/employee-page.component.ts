import { Component, OnInit,Input } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { User } from 'Models/User';
import { application } from 'Models/Application';
import { AuthService } from '../auth.service';
@Component({
  selector: 'app-employee-page',
  templateUrl: './employee-page.component.html',
  styleUrls: ['./employee-page.component.css']
})
export class EmployeePageComponent implements OnInit {

  @Input() Usersrc : string=`${application.URL}/User/GetUsersByUserRoleId?RoleId=2`;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    this.http
    .get<any>(this.Usersrc,{headers:headers})
    .subscribe((data)=>{
      this.data =data;
      console.log(data);
    });
  }
  DisableUser(userId:number){
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    console.log("ge")
    this.http
    .patch(`https://localhost:7197/User/ChangeUserVerifyStatus?UserId=${userId}&IsVerified=false`,Object,{headers:headers})  
    .subscribe((data)=>{
      console.log(data);
    });
    
  }
  public data: User[] = []
}
