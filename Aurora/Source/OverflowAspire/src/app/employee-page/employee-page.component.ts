import { Component, OnInit,Input } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { User } from 'Models/User';
import { application } from 'Models/Application';
@Component({
  selector: 'app-employee-page',
  templateUrl: './employee-page.component.html',
  styleUrls: ['./employee-page.component.css']
})
export class EmployeePageComponent implements OnInit {

  @Input() Usersrc : string=`${application.URL}/User/GetUsersByUserRoleId?RoleId=2`;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http
    .get<any>(this.Usersrc)
    .subscribe((data)=>{
      this.data =data;
      console.log(data);
    });
  }
  checkEmployee(userId:any){
    console.warn("user id :"+userId)
    if(confirm("Are you sure?")==true){
    }
  }
  DisableUser(userId:number){
    console.warn("user id :"+userId)
    if(confirm("Are you sure?")==true){
    this.http
    .patch(`https://localhost:7197/User/ChangeUserVerifyStatus?UserId=${userId}&IsVerified=false`,Object)  
    .subscribe((data)=>{
      console.log(data);
    });
  }}
  public data: User[] = []
 
 
    
  

}
