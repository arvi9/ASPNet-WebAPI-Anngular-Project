import { Template } from '@angular/compiler/src/render3/r3_ast';
import { Component, OnInit,Input } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Query } from 'Models/Query';
import { AuthService } from '../auth.service';



@Component({
  selector: 'app-query-card',
  templateUrl: './query-card.component.html',
  styleUrls: ['./query-card.component.css']


})
export class QueryCardComponent implements OnInit {

 @Input() Querysrc: string ="";
  totalLength: any;
  page: number = 1;
  searchTitle="";
  searchUnSolvedQueries=false;
  searchSolvedQueries=false;

  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    console.log(AuthService.GetData("token"))
    this.http
      .get<any>(this.Querysrc, {headers: headers} )
      .subscribe({next:(data) => {
        this.data = data;
        this.filteredData = data;
        this.totalLength = data.length;

  }});
  }
  public data: Query[] = [];
  public filteredData: Query[] = [];

  samplefun(searchTitle: string, searchSolvedQueries: boolean, searchUnSolvedQueries:boolean) {

    if (searchTitle.length == 0 && searchSolvedQueries==false && searchUnSolvedQueries==false) this.data = this.filteredData


    //1.Search by title
    if (searchTitle.length != 0 && searchSolvedQueries==false && searchUnSolvedQueries==false) {
      console.log("title")
      this.data = this.filteredData.filter(item => item.title.toLowerCase().includes(searchTitle.toLowerCase()));
    }
    //2.Search by SolvedQueries
    if (searchTitle == '' && searchSolvedQueries!=false && searchUnSolvedQueries==false) {
      console.log("Solved Queries")
      this.data = this.filteredData.filter((item) => item.isSolved== true);
    }
    //3. Search by UnsolvedQueries
    if (searchTitle == '' && searchSolvedQueries==false && searchUnSolvedQueries!=false) {
      console.log("UnSolved Queries")
      
      this.data = this.filteredData.filter(item=> item.isSolved == false);
    }
    //4. Search by title and unsolved Queries
    if (searchTitle.length != 0 && searchSolvedQueries==false && searchUnSolvedQueries!=false) {
      console.log("title and Unsolved")
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) &&  item.isSolved == false });
    }
    //5. search by title and Solved Queries
    if (searchTitle.length != 0 && searchSolvedQueries!=false && searchUnSolvedQueries==false) {
      console.log("title and solved")
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) &&  item.isSolved == true});
    }
}
}

