import { Component, OnInit,Input } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { application } from 'Models/Application';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { data } from 'jquery';
import { NgxSpinnerService } from 'ngx-spinner';
import { Toaster } from 'ngx-toast-notifications';
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
  IsVerified:string=''
  constructor(private spinnerService: NgxSpinnerService,private http: HttpClient,private route:Router,private toaster: Toaster) { }
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
        this.IsReviewer=data.isReviewer,
        this.IsVerified=data.IsVerified
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
        if(this.IsVerified=="false"){
          this.toaster.open({text: 'User is in Under Verification',position: 'top-center',type: 'primary'})
          this.route.navigateByUrl("");
        }
        else{
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
