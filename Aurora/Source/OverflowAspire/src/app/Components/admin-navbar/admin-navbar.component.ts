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
    $("#menu-toggle").on("click",function(e) { 
      e.preventDefault();
      $("#wrapper").toggleClass("toggled");
    });
    $("#sidebar-wrapper").on("click",function(e) {
      e.preventDefault();
      $("#wrapper").toggleClass("toggled");
    });
    }

  LogOut(){
    localStorage.clear();
    this.route.navigateByUrl("")
    return 100;
  }
  
}
