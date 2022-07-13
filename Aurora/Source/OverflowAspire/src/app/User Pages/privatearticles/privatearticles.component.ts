import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Article } from 'Models/Article';
import { AuthService } from 'src/app/Services/auth.service';
import { ConnectionService } from 'src/app/Services/connection.service';

@Component({
  selector: 'app-privatearticles',
  templateUrl: './privatearticles.component.html',
  styleUrls: ['./privatearticles.component.css']
})
export class PrivatearticlesComponent implements OnInit {
  url: string = "PrivateArticles";
  totalLength: any;
  page: number = 1;
  searchTitle = "";
  searchAuthor = "";
  FromDate = new Date("0001-01-01");
  ToDate = new Date("0001-01-01");
  userId:any=3;
  public data: any[] = [];
  public filteredData: Article[] = [];
 
constructor(private route: ActivatedRoute,private routes:Router,private connection:ConnectionService) { }

//Get private articles.
ngOnInit(): void {
  if (AuthService.GetData("token") == null) this.routes.navigateByUrl("")
  }
  
 
  samplefun(searchTitle: string, FromDate: any, ToDate: any) {
    if (searchTitle.length == 0 &&  FromDate == new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) this.data = this.filteredData
    //1.Search by title
    if (searchTitle.length != 0 &&  FromDate == new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => item.title.toLowerCase().includes(searchTitle.toLowerCase()));
    }
    //3.Search by FromDate
    else if (searchTitle == '' &&  FromDate != new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => new Date(item.date) >= new Date(FromDate));
    }
    //4.Search by ToDate
    else if (searchTitle == '' && FromDate == new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => new Date(item.date) <= new Date(ToDate));
    }
    //6.search by title and fromdate
    else if (searchTitle.length != 0 &&  FromDate != new Date("0001-01-01").toString() && ToDate == new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && new Date(item.date) >= new Date(FromDate) });
    }
    //7.search by title and Todate
    else if (searchTitle.length != 0 &&  FromDate == new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && new Date(item.date) <= new Date(ToDate) });
    }
    //8.search by fromdate and todate
    else if (searchTitle == '' &&  FromDate != new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return new Date(item.date) >= new Date(FromDate) && new Date(item.date) <= new Date(ToDate) });
    }
     //9.search by Title,Fromdate and Todate
     else if (searchTitle.length != 0 &&  FromDate != new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && new Date(item.date) >= new Date(FromDate)&&new Date(item.date) <= new Date(ToDate) });
    }
    //10.search by Title,Author,Fromdate and Todate
    else if (searchTitle.length != 0 && FromDate != new Date("0001-01-01").toString() && ToDate != new Date("0001-01-01").toString()) {
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase())&& new Date(item.date) >= new Date(FromDate)&&new Date(item.date) <= new Date(ToDate) });
    }
  }
}
