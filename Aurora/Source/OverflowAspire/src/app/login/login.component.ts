import { Component, OnInit,Input } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { application } from 'Models/Application';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { data } from 'jquery';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
showErrorMessage=false;
  IsAdmin:boolean=false;
  IsReviewer:boolean=false;
  IsLoading:boolean=false;
  constructor(private spinnerService: NgxSpinnerService,private http: HttpClient,private route:Router) { }
  user:any={

   Email: '',
    Password: '',

  }


  ngOnInit(): void {
  }

  onSubmit(){

    this.IsLoading=true;

   this.showErrorMessage=false;
    const headers = { 'content-type': 'application/json'}

    console.log(this.user)
    this.http.post<any>(`${application.URL}/Token/AuthToken`,this.user,{headers:headers})
      .subscribe({
        next:(data) =>
      {

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

      },
      error:(error)=>{
      
        this.showErrorMessage=true;
      }
    });




       
  }

}
