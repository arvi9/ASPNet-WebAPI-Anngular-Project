import { Component, OnInit,Input } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { application } from 'Models/Application';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  IsAdmin:boolean=false;
  IsReviewer:boolean=false;
  constructor(private http: HttpClient,private route:Router) { }
  user:any={

   Email: '',
    Password: '',

  }


  ngOnInit(): void {
  }

  onSubmit(){
    const headers = { 'content-type': 'application/json'}

    console.log(this.user)
    this.http.post<any>(`${application.URL}/Token/AuthToken`,this.user,{headers:headers})
      .subscribe((data) => {

        this.IsAdmin=data.isAdmin,
        this.IsReviewer=data.isReviewer
        AuthService.SetDateWithExpiry("token",data.token,data.expiryInMinutes)
        AuthService.SetDateWithExpiry("Admin",data.isAdmin,data.expiryInMinutes)
        AuthService.SetDateWithExpiry("Reviewer",data.isReviewer,data.expiryInMinutes)

        console.log(AuthService.GetData("token"))
        console.log(AuthService.GetData("Admin"))
        console.log(AuthService.GetData("Reviewer"))

        if(this.IsAdmin){
          this.route.navigateByUrl("/AdminDashboard");  //navigation
        }else{
          this.route.navigateByUrl("/Home");
        }
        console.log(data)

      });
  }
}
