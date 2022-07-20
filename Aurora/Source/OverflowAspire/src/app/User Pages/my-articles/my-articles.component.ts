import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';

import { ConnectionService } from 'src/app/Services/connection.service';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'app-my-articles',
  templateUrl: './my-articles.component.html',
  styleUrls: ['./my-articles.component.css']
})
export class MyArticlesComponent implements OnInit {
  @Input() ShowStatus: boolean = true;
  @Input() Showprivate: boolean = false;
  isSpinner = true;
  totalLength: any;
  page: number = 1;
  searchTitle = "";
  FromDate = new Date("0001-01-01");
  ToDate = new Date("0001-01-01");
  userId: any = 0;
  maxDate:any;
  public data: any[] = [];
  public filteredData: any[] = [];

  constructor(private routes: Router, private connection: ConnectionService,private spinner: NgxSpinnerService) { }
  
  //Get my articles.
  ngOnInit(): void {
    this.dateValidation();
    if (AuthService.GetData("token") == null) this.routes.navigateByUrl("")
    this.spinner.show();
    this.connection.GetMyArticles()
      .subscribe({
        next: (data: any[]) => {
          this.data = data;
          this.filteredData = data;
          this.totalLength = data.length;
        },
        complete: () => {
          this.isSpinner = false;
          this.spinner.hide();
        }
      });
  }

  dateValidation() {
    var date: any = new Date();
    var toDate: any = date.getDate();
    if (toDate < 10) {
      toDate = "0" + toDate;
    }
    var month = date.getMonth() + 1;
    if (month < 10) {
      month = '0' + month;
    }
    var year = date.getFullYear(); 
    this.maxDate = year + "-" + month + "-" + toDate;
    return true;
  }
  samplefun(searchTitle: string, FromDate: any, ToDate: any) {
    if (searchTitle.length == 0 && FromDate == new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) this.data = this.filteredData
    //1.Search by title
    if (searchTitle.length != 0 && FromDate == new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => item.title.toLowerCase().includes(searchTitle.toLowerCase()));
    }
    //3.Search by FromDate
    else if (searchTitle == '' && FromDate != new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => new Date(item.date) >= new Date(FromDate));
    }
    //4.Search by ToDate
    else if (searchTitle == '' && FromDate == new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => new Date(item.date) <= new Date(ToDate));
    }
    //6.search by title and fromdate
    else if (searchTitle.length != 0 && FromDate != new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && new Date(item.date) >= new Date(FromDate) });
    }
    //7.search by title and Todate
    else if (searchTitle.length != 0 && FromDate == new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && new Date(item.date) <= new Date(ToDate) });
    }
    //8.search by fromdate and todate
    else if (searchTitle == '' && FromDate != new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return new Date(item.date) >= new Date(FromDate) && new Date(item.date) <= new Date(ToDate) });
    }
    //9.search by Title,Fromdate and Todate
    else if (searchTitle.length != 0 && FromDate != new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && new Date(item.date) >= new Date(FromDate) && new Date(item.date) <= new Date(ToDate) });
    }
    //10.search by Title,Author,Fromdate and Todate
    else if (searchTitle.length != 0 && FromDate != new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && new Date(item.date) >= new Date(FromDate) && new Date(item.date) <= new Date(ToDate) });
    }
    this.searchTitle = '';
    this.FromDate=new Date("0001-01-01");
    this.ToDate=new Date("0001-01-01");
  }
}
