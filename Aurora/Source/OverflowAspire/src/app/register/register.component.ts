import { Component, OnInit } from '@angular/core';
import { User } from 'Models/User';

@Component({
  selector: 'app-register',
  templateUrl:'./register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor() { }
  user:User={
    userId: 0,
    fullName: '',
    genderId: 0,
    aceNumber: '',
    emailAddress: '',
    password: '',
    dateOfBirth: '',
    verifyStatusID: 0,
    isReviewer: false,
    userRoleId: 0,
    designationId: 0
  }
  

  

  dept = '';
  _Designation = '';
  designation: any[] = [];
  

  ngOnInit(): void {

  }


  filterDropdown() {

  
    this.designation = [];
    for (let item of this.designationDetails) {
      if (item.departmentName == this.dept) {
        this.designation.push(item);
        console.log("true")
      }
    }

  

  }
  onSubmit(){

  }





  department: string[] = [ 'java', 'dotnet','BFS']

  designationDetails: any[] = [{
    departmentName: 'java',
    designationName: 'TeamLead'
  },
  {
    departmentName: 'java',
     designationName: 'Trainer'
  }, {
    departmentName: 'dotnet',
    designationName: 'ModuleLead'
  },
  {
    departmentName: 'dotnet',
    designationName: 'Trainer'
  }]


}
