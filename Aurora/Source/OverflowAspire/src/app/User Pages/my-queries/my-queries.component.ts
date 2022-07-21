import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Query } from 'Models/Query';
import { NgxSpinnerService } from 'ngx-spinner';
import { Toaster } from 'ngx-toast-notifications';
import { AuthService } from 'src/app/Services/auth.service';
import { ConnectionService } from 'src/app/Services/connection.service';

@Component({
  selector: 'app-my-queries',
  templateUrl: './my-queries.component.html',
  styleUrls: ['./my-queries.component.css']
})

export class MyQueriesComponent implements OnInit {
  @Input() ShowStatus: boolean = true;
  totalLength: any;
  page: number = 1;
  searchTitle = "";
  isSpinner = true;
  searchUnSolvedQueries = false;
  searchSolvedQueries = false;
  public data: Query[] = [];
  public filteredData: Query[] = [];
  error: any
  
  //Get My queries.
  constructor(private connection: ConnectionService, private route: Router, private toaster: Toaster, private spinner: NgxSpinnerService) { }
  ngOnInit(): void {
    if (AuthService.GetData("token") == null) {
      this.toaster.open({ text: 'Your Session has been Expired', position: 'top-center', type: 'warning' })
      this.route.navigateByUrl("")
    }
    this.spinner.show();
    this.connection.GetMyQueries()
      .subscribe({
        next: (data: any[]) => {
          this.data = data;
          this.filteredData = data;
          this.totalLength = data.length;
        },
        error: (error: any) => this.error = error.error.message,
        complete: () => {
          this.isSpinner = false;
          this.spinner.hide();
        }
      });
  }


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
    this.searchTitle = '';
    this.searchSolvedQueries = false;
    this.searchUnSolvedQueries = false;
  }
}


