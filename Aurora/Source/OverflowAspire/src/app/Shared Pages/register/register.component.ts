import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms'
import { formatDate } from '@angular/common';
import { ConnectionService } from 'src/app/Services/connection.service';
import { Toaster } from 'ngx-toast-notifications';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  IsLoading: boolean = false;
  designationDetails: any;
  departmentDetails: any;
  GenderDetails: any;

  Designationlist: any[] = []
  Designationlist1: any[] = []
  user = this.fb.group({
    fullName: ['', [Validators.required, Validators.maxLength(20), Validators.pattern("^[A-Za-z ]+$")]],
    gender: ['', [Validators.required]],
    aceNumber: ['', [Validators.required, Validators.pattern("ACE+[0-9]{4}")]],
    departmentValidate: ['', [Validators.required]],
    emailAddress: ['', [Validators.required, Validators.pattern("([a-zA-Z0-9-_\.]+)@(aspiresys.com)")]],
    DesignationValidate: ['', [Validators.required]],
    dateOfBirth: ['', [Validators.required]],
    password: ['', [Validators.required, Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")]],

  })
  todayAsString: any;

  age: Number = 0;
  year: number = 0;

  constructor(private fb: FormBuilder, private connection: ConnectionService, private router: Router,private toaster: Toaster) { }


  dobmessage: string = ''
  message: string = ''
  minage: Number = 18;
  validateGendererror = true;
  validateGender(value: any) {
    if (value === 'default') {
      this.validateGendererror = true;
    }
    else {
      this.validateGendererror = false;
    }
  }



  ngOnInit(): void {

   this.connection.GetDesignations()
      .subscribe({
        next: (data) => {
          this.designationDetails = data;
        }
      });
    this.connection.GetDepartments()
      .subscribe({
        next: (data) => {
          this.departmentDetails = data;
          this.connection.GetGenders()
            .subscribe({
              next: (data) => {
                this.GenderDetails = data;
              }
            });


        }
      });
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
    this.todayAsString = formatDate(new Date(), 'yyyy-MM-dd', 'en');
    if (this.user.value['dateOfBirth'] < this.todayAsString) {
      this.year = parseInt(this.user.value['dateOfBirth']);
      if ((new Date().getFullYear() - this.year) <= 18) {
        return true;
      }
      return false;
    }
    else {
      return true;
    }
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
      createdOn: Date.now,
      updatedBy: 0,
      updatedOn: null,
      designation: null,
      gender: null,
      userRole: null,
      verifyStatus: null,
      queries: null,
      queryComments: null,
      articleComments: null,
      articles: null,
      likes: null
    }
    this.connection.Register(registeruser)
      .subscribe({
        next: (data) => {
        }
      });
      this.toaster.open({ text: 'User Registered Successfully', position: 'top-center', type: 'success' })
    this.router.navigateByUrl("");
  }

}
