import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Query } from 'Models/Query';
import { NgxSpinnerService } from 'ngx-spinner';
import { ConnectionService } from 'src/app/Services/connection.service';


@Component({
  selector: 'app-query-card',
  templateUrl: './query-card.component.html',
  styleUrls: ['./query-card.component.css']
})

export class QueryCardComponent implements OnInit {
  @Input() ShowStatus: boolean = true;
  @Input() Querysrc: string = "";
  isSpinner=true;
  totalLength: any;
  page: number = 1;
  searchTitle = "";
  searchUnSolvedQueries = false;
  searchSolvedQueries = false;
  public data: Query[] = [];
  public filteredData: Query[] = [];

  constructor(private connection: ConnectionService, private route: Router,private spinner: NgxSpinnerService) { }

  // Get queries.
  ngOnInit(): void {
    this.spinner.show();
    if (this.Querysrc == "allQueries") {
      //Get all the queries
      this.connection.GetAllQueries()
        .subscribe({
          next: (data) => {
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

    // Get latest queries.
    else if (this.Querysrc == "latestQueries") {
      this.connection.GetLatestQueries()
        .subscribe({
          next: (data) => {
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

    // Get Trending queries.
    else if (this.Querysrc == "trendingQueries") {
      this.connection.GetTrendingQueries()
        .subscribe({
          next: (data) => {
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
  }

  //Filter query by Solved and unsolved queries.
  samplefun(searchTitle: string, searchSolvedQueries: boolean, searchUnSolvedQueries: boolean) {

    //No filter is applied this condition will executed
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
    this.searchTitle = '';
    this.searchSolvedQueries = false;
    this.searchUnSolvedQueries = false;
  }
}

