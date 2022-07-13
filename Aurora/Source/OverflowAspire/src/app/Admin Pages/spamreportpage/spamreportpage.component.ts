import { Component,OnInit } from '@angular/core';
import { AuthService } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';
import { ConnectionService } from 'src/app/Services/connection.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-spamreportpage',
  templateUrl: './spamreportpage.component.html',
  styleUrls: ['./spamreportpage.component.css']
})

export class SpamreportpageComponent implements OnInit {
  public data: any[] = []
  constructor(private connection: ConnectionService, private route: Router) { }

  //Get reported spams.
  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.route.navigateByUrl("")
    if (!AuthService.IsAdmin()) {
      this.route.navigateByUrl("")
    }
    this.connection.GetSpams()
      .subscribe({
        next: (data: any[]) => {
          this.data = data;
          $(function(){
            $("#emptable").DataTable();
           });
        }
      });
  }
}
