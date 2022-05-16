import { Component, OnInit,Input } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { User } from 'Models/User';
import { application } from 'Models/Application';

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
    this.http.post<any>(`${application.URL}/Token/AuthToken`,this.user,{headers:headers})
      .subscribe((data) => {
        localStorage.setItem("token",data.token)

        console.log(data)

      });
  }
}
