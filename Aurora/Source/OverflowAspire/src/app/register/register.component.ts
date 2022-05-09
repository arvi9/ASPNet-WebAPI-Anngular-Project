import { Component, OnInit } from '@angular/core';
import { User } from 'Models/User';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl:'./register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  register: any;

 
  
  constructor(private router:Router) { }
  user:User={
    userId: 0,
    fullName: '',
    genderId: 0,
    aceNumber: '',
    email: '',
    department:'',
    employeeId:'',
    designation:'',
    password: '',
    dateOfBirth: '',
    verifyStatusID: 0,
    isReviewer: false,
    userRoleId: 0,
    designationId: 0
  }
   
dobmessage:string=''
message:string=''
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
    validateDateOfBirth(){
     if(Date.now.toString()>this.user.dateOfBirth){
  
       this.dobmessage="Date of birth should be valid";
       console.log(this.dobmessage);
// return this.dobmessage;
    }
      }

  // validateconfirmpassword(){
  //   if(this.user.password==this.confirmpassword){
  //     return null;}
  //     else{
  //       this.message="Password do not match";
  //       return this.message;
  //     }

  //   }
  


gender=''
  Gender:string[]=['Male','Female','Others']



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

  userdata()
  {
    this.router.navigate(['/Login']);
    this.register.setMessage(this.user);
    localStorage.setItem(this.user.fullName, JSON.stringify(this.user));
    // this.login.userName(this.userModel);
    // this.login.display(this.userModel);
  }
}
