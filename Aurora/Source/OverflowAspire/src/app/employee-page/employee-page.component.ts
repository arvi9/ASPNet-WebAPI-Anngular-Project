import { Component, OnInit,Input } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { User } from 'Models/User';
import { application } from 'Models/Application';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';



@Component({
  selector: 'app-employee-page',
  templateUrl: './employee-page.component.html',
  styleUrls: ['./employee-page.component.css']
})
export class EmployeePageComponent implements OnInit {
  @Input() Usersrc : string=`${application.URL}/User/GetUsersByUserRoleId?RoleId=2`;
  constructor(private http: HttpClient,private route:Router) { }


  ngOnInit(): void {
    if(AuthService.GetData("token")==null) this.route.navigateByUrl("")
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
  onCheckboxChange(userId:any){
      var res=this.data.find(item => item.userId==userId)?.isReviewer 
      console.log(res)   
      if(res==true){
        const headers = new HttpHeaders({
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${AuthService.GetData("token")}`
        })
        this.http
        .patch(`https://localhost:7197/User/UpdateUserByIsReviewer?UserId=${userId}&IsReviewer=true`,Object,{headers:headers})
        .subscribe((data)=>{
          console.log(data);
        });
       
       
      } 

      else{
        const headers = new HttpHeaders({
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${AuthService.GetData("token")}`
        })
        this.http
        .patch(`https://localhost:7197/User/UpdateUserByIsReviewer?UserId=${userId}&IsReviewer=false`,Object,{headers:headers})
        .subscribe((data)=>{
          console.log(data);
        });
      }
  }
  
  public data: User[] = []
}
