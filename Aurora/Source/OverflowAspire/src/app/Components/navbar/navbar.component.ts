import { Component,OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})

export class NavbarComponent implements OnInit {
  role=AuthService.GetData("Reviewer")

  constructor(private route:Router) { }

  ngOnInit(): void {
  }

  //Here the user can logout the application.
  LogOut(){
    AuthService.Logout();
    this.route.navigateByUrl("")
  }
 
}
