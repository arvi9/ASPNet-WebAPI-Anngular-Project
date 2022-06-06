import { Component, Input, OnInit } from '@angular/core';
import { User } from 'Models/User';
import { Router } from '@angular/router';
import { application } from 'Models/Application';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../auth.service';
import { ReactiveFormsModule } from '@angular/forms';
import { FormGroup, FormControl, Validators } from '@angular/forms'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  IsLoading:boolean=false;
  Registersrc: string = `${application.URL}/User/CreateUser`;

  designationDetails: any;
  departmentDetails: any;
  GenderDetails: any;
  Designationlist: any[] = []
  Designationlist1: any[] = []
  user = new FormGroup({
    fullName: new FormControl('', [Validators.required, Validators.maxLength(20), Validators.pattern("^[A-Za-z ]+$")]),
    gender: new FormControl('', [Validators.required]),
    aceNumber: new FormControl('', [Validators.required, Validators.pattern("ACE+[0-9]{4}")]),
    departmentValidate: new FormControl('', [Validators.required]),
    emailAddress: new FormControl('', [Validators.required, Validators.pattern("([a-zA-Z0-9-_\.]+)@(aspiresys.com|aspiresystems.biz)")]),
    DesignationValidate: new FormControl('', [Validators.required]),
    dateOfBirth: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required, Validators.maxLength(10)]),

  })

  get fullName() {
    return this.user.get('fullName');
  }
  get gender() {
    return this.user.get('gender');
  }
  get aceNumber() {
    return this.user.get('aceNumber')
  }
  get departmentValidate() {
    return this.user.get('departmentValidate')

  }
  get emailAddress() {
    return this.user.get('emailAddress')
  }
  get DesignationValidate() {
    return this.user.get('DesignationValidate')
  }
  get dateOfBirth() {
    return this.user.get('dateOfBirth')
  }
  get password() {
    return this.user.get('password')
  }
  constructor(private http: HttpClient, private router: Router) { }
  // user: any = {
  //   userId: 0,
  //   fullName: '',
  //   genderId: 0,
  //   aceNumber: '',
  //   emailAddress: '',
  //   password: '',
  //   dateOfBirth: new Date(),
  //   verifyStatusID: 3,
  //   isReviewer: false,
  //   userRoleId: 2,
  //   designationId: 0,
  //   createdOn: Date.now,
  //   updatedBy: 0,
  //   updatedOn: null,
  //   designation: null,
  //   gender: null,
  //   userRole: null,
  //   verifyStatus: null,
  //   queries: null,
  //   queryComments: null,
  //   articleComments: null,
  //   articles: null,
  //   likes: null
  // }

  dobmessage: string = ''
  message: string = ''

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
      .subscribe((data) => {
        this.designationDetails = data;
        console.log(this.designationDetails)

      });
    this.http
      .get<any>(this.DepartmentUrl)
      .subscribe((data) => {
        this.departmentDetails = data;
        console.log(this.departmentDetails)

      });
    this.http
      .get<any>(this.GenderUrl)
      .subscribe((data) => {
        this.GenderDetails = data;
        console.log(this.GenderDetails)

      });

  }

  FilterDesignation() {
    this.Designationlist = [];
    for (let item of this.designationDetails) {
      console.log(item.departmentId)
      if (item.departmentId == this.departmentValidate?.value) {
        console.log(item.departmentId)
        this.Designationlist.push(item)
        console.log(item)
      }
    }
  }

  validateDateOfBirth() {
    if (Date.now.toString() > this.dateOfBirth?.value) {
      this.dobmessage = "Date of birth should be valid";
      console.log(Date.now)
      console.log(this.dateOfBirth?.value);
      console.log(this.dobmessage);
      // return true;
    }
    //  else{
    //    return false;
    //  }
  }


  userdata() {
    this.IsLoading=true
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',

    })
    console.log(AuthService.GetData("token"))
    console.log(this.user)
    this.http.post<any>(`${application.URL}/User/CreateUser`, this.user, { headers: headers })
      .subscribe((data) => {

        console.log(data)
        this.router.navigateByUrl("");

      });

  }





}
