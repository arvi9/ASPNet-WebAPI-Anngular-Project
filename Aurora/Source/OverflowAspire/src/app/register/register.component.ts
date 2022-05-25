import { Component, Input, OnInit } from '@angular/core';
import { User } from 'Models/User';
import { Router } from '@angular/router';
import { application } from 'Models/Application';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  Registersrc: string = `${application.URL}/User/CreateUser`;

  designationDetails: any;
  departmentDetails: any;
  GenderDetails: any;



  constructor(private http: HttpClient, private router: Router) { }
  user: any = {
    userId: 0,
    fullName: '',
    genderId: 0,
    aceNumber: '',
    emailAddress: '',
    password: '',
    dateOfBirth: new Date(),
    verifyStatusID: 3,
    isReviewer: false,
    userRoleId: 2,
    designationId: 0,
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

  dobmessage: string = ''
  message: string = ''
  department = ''




  DesignationUrl: string = `https://localhost:7197/User/GetDesignations`
  DepartmentUrl: string = `https://localhost:7197/User/GetDepartments`
  GenderUrl: string = `https://localhost:7197/User/GetGenders`


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
         
  }
  validateDateOfBirth() {
    if (Date.now.toString() > this.user.dateOfBirth) {
      this.dobmessage = "Date of birth should be valid";
      console.log(this.dobmessage);

    }
  }


  userdata() {
    const headers = { 'content-type': 'application/json' }
    console.log(this.user)
    this.http.post<any>(`${application.URL}/User/CreateUser`, this.user, { headers: headers })
      .subscribe((data) => {

        console.log(data)

      });
      this.router.navigateByUrl("");
  }

 
   


}
