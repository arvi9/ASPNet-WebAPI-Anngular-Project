import { Component, OnInit, Input } from '@angular/core';
import { Query } from 'Models/Query';
import { ConnectionService } from 'src/app/Services/connection.service';


@Component({
  selector: 'app-query-card',
  templateUrl: './query-card.component.html',
  styleUrls: ['./query-card.component.css']


})
export class QueryCardComponent implements OnInit {
  @Input() ShowStatus: boolean = true;
  @Input() Querysrc: string = "";
  totalLength: any;
  page: number = 1;
  searchTitle = "";
  searchUnSolvedQueries = false;
  searchSolvedQueries = false;

  constructor(private connection: ConnectionService) { }

  // Get all queries.
  ngOnInit(): void {
    if (this.Querysrc == "allQueries") {
      this.connection.GetAllQueries()
        .subscribe({
          next: (data) => {
            this.data = data;
            this.filteredData = data;
            this.totalLength = data.length;
          }
        });
    }
    // Get latest queries.
    else if (this.Querysrc == "latestQueries") {
      this.connection.GetLatestQueries()
        .subscribe({
          next: (data) => {
            this.data = data;
            this.filteredData = data;
            this.totalLength = data.length;
          }
        });
    }
    // Get Trending queries.
    else if (this.Querysrc == "trendingQueries") {
      this.connection.GetTrendingQueries()
        .subscribe({
          next: (data) => {
            this.data = data;
            this.filteredData = data;
            this.totalLength = data.length;
          }
        });
    }
    else {
      this.connection.GetAdminDashboard()
    }
  }

  //Filter query by Solved and unsolved queries.
  public data: Query[] = [];
  public filteredData: Query[] = [];

  samplefun(searchTitle: string, searchSolvedQueries: boolean, searchUnSolvedQueries: boolean) {

    if (searchTitle.length == 0 && searchSolvedQueries == false && searchUnSolvedQueries == false) this.data = this.filteredData


    //1.Search by title
    else if (searchTitle.length != 0 && searchSolvedQueries == false && searchUnSolvedQueries == false) {
      this.data = this.filteredData.filter(item => item.title.toLowerCase().includes(searchTitle.toLowerCase()));
    }
    //2.Search by SolvedQueries
    else if (searchTitle == '' && searchSolvedQueries != false && searchUnSolvedQueries == false) {
      this.data = this.filteredData.filter((item) => item.isSolved == true);
    }
    //3. Search by UnsolvedQueries
    else if (searchTitle == '' && searchSolvedQueries == false && searchUnSolvedQueries != false) {
      this.data = this.filteredData.filter(item => item.isSolved == false);
    }
    //4. Search by title and unsolved Queries
    else if (searchTitle.length != 0 && searchSolvedQueries == false && searchUnSolvedQueries != false) {
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && item.isSolved == false });
    }
    //5. search by title and Solved Queries
    else if (searchTitle.length != 0 && searchSolvedQueries != false && searchUnSolvedQueries == false) {
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) && item.isSolved == true });
    }
    this.searchTitle='';
    this.searchSolvedQueries=false;
    this.searchUnSolvedQueries=false;
  }

  // reset_filter() {
  //   this.searchUnSolvedQueries = false;
  //   this.searchSolvedQueries = false;
  //   this.filteredData=this.data
  // }
}

