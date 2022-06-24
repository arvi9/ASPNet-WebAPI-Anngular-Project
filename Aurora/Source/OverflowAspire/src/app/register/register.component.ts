import { Component, Input, OnInit } from '@angular/core';
import { User } from 'Models/User';
import { Router } from '@angular/router';
import { application } from 'Models/Application';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../auth.service';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms'
import { data } from 'jquery';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  IsLoading: boolean = false;
  Registersrc: string = `${application.URL}/User/CreateUser`;

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
    password: ['', [Validators.required, Validators.maxLength(10)]],

  })
  todayAsString: any;

  age: Number = 0;
  year: number = 0;

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) { }


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

  DesignationUrl: string = `${application.URL}/User/GetDesignations`
  DepartmentUrl: string = `${application.URL}/User/GetDepartments`
  GenderUrl: string = `${application.URL}/User/GetGenders`


  ngOnInit(): void {

    this.http
      .get<any>(this.DesignationUrl)
      .subscribe({next:(data) => {
        this.designationDetails = data;
        console.log(this.designationDetails)

  }});
    this.http
      .get<any>(this.DepartmentUrl)
      .subscribe({
        next:(data) => {
        this.departmentDetails = data;
        console.log(this.departmentDetails)
        this.http
      .get<any>(this.GenderUrl)
      .subscribe({next:(data) => {
        this.GenderDetails = data;
        console.log(this.GenderDetails)

        }});


      }});
    this.http
      .get<any>(this.GenderUrl)
      .subscribe({next:(data) => {
        this.GenderDetails = data;
        console.log(this.GenderDetails)

   } });

  }

  FilterDesignation() {
    this.Designationlist = [];
    for (let item of this.designationDetails) {
      console.log(item.departmentId)
      if (item.departmentId == this.user.value['departmentValidate']) {
        console.log(this.user.value['departmentValidate'])

        this.Designationlist.push(item)
        console.log(item)
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
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',

    })
    console.log(AuthService.GetData("token"))
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
    console.log(registeruser);
    console.log(application.URL);
    this.http.post<any>(`${application.URL}/User/CreateUser`, registeruser, { headers: headers })
      .subscribe({next:(data) => {

        console.log(data)


  }});
    this.router.navigateByUrl("");
  }





}
