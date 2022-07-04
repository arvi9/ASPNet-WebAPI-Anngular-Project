import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { NgxSpinnerService } from 'ngx-spinner';
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
  constructor(private spinnerService: NgxSpinnerService, private connection: ConnectionService, private route: Router, private toaster: Toaster) { }
  user: any = {
    Email: '',
    Password: '',
  }
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
          AuthService.SetDataWithExpiry("Admin", data.isAdmin, data.expiryInMinutes)
          AuthService.SetDataWithExpiry("Reviewer", data.isReviewer, data.expiryInMinutes)
          if (this.IsAdmin) {
            this.route.navigateByUrl("/AdminDashboard");
          }
          else {
            if (this.IsVerified == "NotVerified") {
              this.toaster.open({ text: 'User is in Under Verification', position: 'top-center', type: 'primary' })
              this.route.navigateByUrl("");
            }
            else {
              this.route.navigateByUrl("/Home");
            }
          }
        },

        error: (error) => {
          this.IsLoading = false;
          this.showErrorMessage = true;
        }
      });





  }

}
