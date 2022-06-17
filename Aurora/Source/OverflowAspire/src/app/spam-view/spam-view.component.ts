import { Component, Input, OnInit } from '@angular/core';
import { application } from 'Models/Application';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-spam-view',
  templateUrl: './spam-view.component.html',
  styleUrls: ['./spam-view.component.css']
})
export class SpamViewComponent implements OnInit {
  queryId: number = 0;


  @Input() Querysrc: string = `${application.URL}/Query/GetListOfSpams`;
  constructor(private routing:Router,private route: ActivatedRoute, private http: HttpClient) { }

  ngOnInit(): void {
    if(AuthService.GetData("token")==null) this.routing.navigateByUrl("")
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    this.route.params.subscribe(params => {
      this.queryId = params['queryId'];
      this.http
        .get<any>(this.Querysrc, { headers: headers })
        .subscribe((data) => {
          this.data = data;
          this.data = this.data.filter(item => item.query.queryId == this.queryId)
          console.log(data);
        });
    });
  }

  public data: any[] = []


  onAccept() {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    console.log("ge")
    this.http
    .patch(`https://localhost:7197/Query/UpdateSpamStatus?SpamId=6&VerifyStatusID=1`,Object,{headers:headers})  
    .subscribe((data)=>{
      console.log(data);
    });
    this.routing.navigateByUrl("/SpamReport");
  }


  
  onReject() {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    console.log("ge")
    this.http
    .patch(`https://localhost:7197/Query/UpdateSpamStatus?SpamId=6&VerifyStatusID=2`,Object,{headers:headers})  
    .subscribe((data)=>{
      console.log(data);
    });
    this.routing.navigateByUrl("/SpamReport");
  }
}