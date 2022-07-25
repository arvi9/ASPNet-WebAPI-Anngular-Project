import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { Toaster } from 'ngx-toast-notifications';
import { ConnectionService } from 'src/app/Services/connection.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  showErrorMessage = false;
  IsAdmin: boolean = false;
  IsReviewer: boolean = false;
  IsLoading: boolean = false;
  IsVerified: string = ''
  user: any = {
    Email: '',
    Password: '',
  }
  error = ""

  constructor(private connection: ConnectionService, private route: Router, private toaster: Toaster) { }

  ngOnInit(): void {
  }


  onSubmit() {
    this.IsLoading = true;
    this.showErrorMessage = false;
    this.connection.Login(this.user)
      .subscribe({
        next: (data) => {
          this.IsAdmin = data.isAdmin,
            this.IsReviewer = data.isReviewer,
            this.IsVerified = data.isVerified
          AuthService.SetDataWithExpiry("token", data.token, data.expiryInMinutes)
          AuthService.SetDataWithExpiry("Admin", data.isAdmin.toString(), data.expiryInMinutes)
          AuthService.SetDataWithExpiry("Reviewer", data.isReviewer.toString(), data.expiryInMinutes)
          this.connection.initializeTokenHeader(AuthService.GetData("token"))
          if (AuthService.IsAdmin()) {
            this.route.navigateByUrl("/AdminDashboard");
          }
          else {
            if (this.IsVerified == "NotVerified") {
              this.toaster.open({ text: 'User is in Under Verification', position: 'top-center', type: 'primary' })
              this.IsLoading = false;
              this.route.navigateByUrl("");
            }
            if (this.IsVerified == "Rejected") {
              this.toaster.open({ text: 'You have disabled by the admin!! Contact admin!', position: 'top-center', type: 'warning' })
              this.IsLoading = false;
              this.route.navigateByUrl("");
            }
            if (this.IsVerified == "Approved") {
              this.route.navigateByUrl("/Home");
            }
          }
        },
        error: (error) => {
          this.IsLoading = false;
          if (error.status == 0)
            this.error = "Oops! Server down please try again later";
          else if (error.status == 500)
            this.error = "Sorry some internal error occured please try again later";
          else
            this.showErrorMessage = true;
        },
        complete: () => {
          this.user.Email = ''
          this.user.Password = ''
        }
      });

  }
}
