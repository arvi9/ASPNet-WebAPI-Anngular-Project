import { Component,OnInit } from '@angular/core';
import { AuthService } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-latest-articlepage',
  templateUrl: './latest-articlepage.component.html',
  styleUrls: ['./latest-articlepage.component.css']
})

//Show latest article page.
export class LatestArticlepageComponent implements OnInit {
  url: string = "latestArticles";

  constructor(private route: Router) { }

  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.route.navigateByUrl("")
  }
}