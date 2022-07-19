import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';



@Component({
  selector: 'app-admin-navbar',
  templateUrl: './admin-navbar.component.html',
  styleUrls: ['./admin-navbar.component.css']
})
export class AdminNavbarComponent implements OnInit {
  status: boolean = false;
  sideStatus: boolean = true;
  constructor(private route: Router) { }

  ngOnInit(): void {
  
  }

  clickEvent() {
    this.status = !this.status;
  }

  clickSideEvent() {
    this.sideStatus = !this.sideStatus;
  }

  //Here the user can logout.
  LogOut() {
    localStorage.clear();
    this.route.navigateByUrl("")
    return 100;
  }

}
