import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-admin-navbar',
  templateUrl: './admin-navbar.component.html',
  styleUrls: ['./admin-navbar.component.css']
})
export class AdminNavbarComponent implements OnInit {

  constructor(private route:Router) { }

  ngOnInit(): void {
    
    }
    status: boolean = false;
  clickEvent(){
      this.status = !this.status;       
  }
  sideStatus:boolean=true;
  clickSideEvent(){
    this.sideStatus=!this.sideStatus;
  }

  LogOut(){
    localStorage.clear();
    this.route.navigateByUrl("")
    return 100;
  }
  
}
