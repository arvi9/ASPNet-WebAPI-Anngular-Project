import { Component, OnInit } from '@angular/core';
import { User } from 'Models/User';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  register: any;



  constructor(private router: Router) { }
  user: User = {
    userId: 0,
    fullName: '',
    genderId: 0,
    aceNumber: '',
    email: '',
    department: '',
    employeeId: '',
    designation: '',
    password: '',
    dateOfBirth: '',
    verifyStatusID: 0,
    isReviewer: false,
    userRoleId: 0,
    designationId: 0
  }

  dobmessage: string = ''
  message: string = ''
dept=''
  _Designation = '';

designation:any[]=[]

  ngOnInit(): void {

  }



  filterDropdown() {


    this.designation=[];
    for (let item of this.designationDetails) {
      if (item.departmentID == this.dept) {
        this.designation.push(item);
     
      }
    }
  }
  validateDateOfBirth() {
    if (Date.now.toString() > this.user.dateOfBirth)
    {
      this.dobmessage = "Date of birth should be valid";
      console.log(this.dobmessage);
    
    }
  }

  Gender: any = [
    {
      id: 1,
      gender: 'Male'
    },
    {
      id: 2,
      gender: 'Female'
    }
  ]



  department: any = [{
    id: 1,
    department: 'Java'
  },
  {
    id: 2,
    department: 'Dotnet'
  },
  {
    id: 3,
    department: 'BFS'
  }
  ]
  designationDetails: any = [
    {
      designationid: 1,
      designationName: 'Team Lead',
       departmentID:1
    },
    {
      designationid: 2,
      designationName: 'Team Lead',
       departmentID:2
    },
    {
      designationid: 3,
      designationName: 'Trainer',
      departmentID:1
    },
    {
      designationid: 4,
      designationName: 'ModuleLead',
      departmentID:3
    },
    {
      designationid: 5,
      designationName: 'Senior Engineer',
      departmentID:2
    },
  ]



  userdata() {
    console.log(this.user.genderId)
    // this.router.navigate(['/Login']);
    // this.register.setMessage(this.user);
    // localStorage.setItem(this.user.fullName, JSON.stringify(this.user));
    // this.login.userName(this.userModel);
    // this.login.display(this.userModel);
  }
}
