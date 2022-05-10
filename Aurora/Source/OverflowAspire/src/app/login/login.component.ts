import { Component, OnInit,Input } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { User } from 'Models/User';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private http: HttpClient) { }
  user:any={
    
   Email: '',
    Password: '',
   
  }
   

  ngOnInit(): void {
  }
  
  onSubmit(){
    const headers = { 'content-type': 'application/json'}  
   
    console.log(this.user)
    this.http.post<any>('https://localhost:7197/Token/AuthToken',this.user,{headers:headers})
      .subscribe((data) => {
       
        console.log(data)
       
      });
  }
}