import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { FormBuilder,Validators } from '@angular/forms'
import { formatDate } from '@angular/common';
import { ConnectionService } from 'src/app/Services/connection.service';
import { Toaster } from 'ngx-toast-notifications';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

//User can register his details.
export class RegisterComponent implements OnInit {
  IsLoading: boolean = false;
  designationDetails: any;
  departmentDetails: any;
  GenderDetails: any;
  error: string = ""
  maxDate:any;
  Designationlist: any[] = []
  Designationlist1: any[] = []
  todayAsString: any;
  year: number = 0;
  validateGendererror = true;

  user = this.fb.group({
    fullName: ['', [Validators.required,Validators.minLength(4), Validators.maxLength(26), Validators.pattern("^(?!.*([ ])\\1)(?!.*([A-Za-z])\\2{2})\\w[a-zA-Z\\s]*$")]],
    gender: ['', [Validators.required]],
    aceNumber: ['', [Validators.required, Validators.pattern("^ACE[0-9]{4,6}$"),Validators.pattern("^(?!.*ACE000000|ACE0000|ACE00000).*$")]],
    departmentValidate: ['', [Validators.required]],
    emailAddress: ['', [Validators.required, Validators.pattern("([a-zA-Z0-9-_\.]{5,22})@(aspiresys.com)")]],
    DesignationValidate: ['', [Validators.required]],
    dateOfBirth: ['', [Validators.required]],
    password: ['', [Validators.required, Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")]],
  })

  

  constructor(private fb: FormBuilder, private connection: ConnectionService, private router: Router, private toaster: Toaster) { }
 
  validateGender(value: any) {
    if (value === 'default') {
      this.validateGendererror = true;
    }
    else {
      this.validateGendererror = false;
    }
  }

  ngOnInit(): void {
    this.dateValidation();
    this.connection.GetDesignations()
      .subscribe({
        next: (data) => {
          this.designationDetails = data;
        },
        error: (error:any) => this.error = error.error.message,
      });
    this.connection.GetDepartments()
      .subscribe({
        next: (data) => {
          this.departmentDetails = data;
          this.connection.GetGenders()
            .subscribe({
              next: (data) => {
                this.GenderDetails = data;
              },
              error: (error:any) => this.error = error.error.message,
            });
        },
        error: (error:any) => this.error = error.error.message,
      });
  }
  dateValidation() {
    var date: any = new Date();

    var toDate: any = date.getDate();
    if (toDate < 10) {
      toDate = "0" + toDate;
    }
    var month = date.getMonth() + 1;
    if (month < 10) {
      month = '0' + month;
    }
    var year = date.getFullYear(); 
    this.maxDate = year + "-" + month + "-" + toDate;
    console.log(this.maxDate);
    return true;
  }
  FilterDesignation() {
    this.Designationlist = [];
    for (let item of this.designationDetails) {
      if (item.departmentId == this.user.value['departmentValidate']) {
        this.Designationlist.push(item)
      }
    }
  }

  validateDateOfBirth() {
      this.year = parseInt(this.user.value['dateOfBirth']);
      if (((new Date().getFullYear() - this.year) <= 18))
       {
        return true;
      }
      if((new Date().getFullYear() - this.year) >= 60){
        return true;
      }
      return false;
    }
  


  userdata() {
    this.IsLoading = true
    const registeruser = {
      userId: 0,
      fullName: this.user.value['fullName'],
      genderId: this.user.value['gender'],
      aceNumber: this.user.value['aceNumber'],
      emailAddress: this.user.value['emailAddress'],
      password: this.user.value['password'],
      dateOfBirth: this.user.value['dateOfBirth'],
      verifyStatusID: 3,
      isReviewer: false,
      userRoleId: 2,
      designationId: this.user.value['DesignationValidate'],
      createdOn:new Date(),
      updatedBy: 0, 
    }
    
    this.connection.Register(registeruser)
      .subscribe({
        next: (data) => {
        },
        error: (error) => {
          this.error = error.error.message;
          this.IsLoading=false;
        },
        complete: () => {
          this.toaster.open({ text: 'User Registered Successfully', position: 'top-center', type: 'success' })
          this.router.navigateByUrl("");
        }
      });
  }
}
