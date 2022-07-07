import { Component, OnInit,Input } from '@angular/core';
import { Query } from 'Models/Query';
import { ConnectionService } from 'src/app/Services/connection.service';


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

  constructor(private connection: ConnectionService) { }
  ngOnInit(): void {
    if(this.Querysrc=="allQueries"){
      this.connection.GetAllQueries()
       .subscribe({next:(data) => {
         this.data = data;
         this.filteredData = data;
         this.totalLength = data.length;
       }});
     }
     else if(this.Querysrc=="latestQueries")
     {
       this.connection.GetLatestQueries()
       .subscribe({next:(data) => {
         this.data = data;
         this.filteredData = data;
         this.totalLength = data.length;
       }});
     }
     else if(this.Querysrc=="trendingQueries")
     {
       this.connection.GetTrendingQueries()
       .subscribe({next:(data) => {
         this.data = data;
         this.filteredData = data;
         this.totalLength = data.length;
       }});
     }
     else{
      this.connection.GetAdminDashboard()
     }
  }

  
  public data: Query[] = [];
  public filteredData: Query[] = [];

  samplefun(searchTitle: string, searchSolvedQueries: boolean, searchUnSolvedQueries:boolean) {

    if (searchTitle.length == 0 && searchSolvedQueries==false && searchUnSolvedQueries==false) this.data = this.filteredData


    //1.Search by title
    if (searchTitle.length != 0 && searchSolvedQueries==false && searchUnSolvedQueries==false) {
      this.data = this.filteredData.filter(item => item.title.toLowerCase().includes(searchTitle.toLowerCase()));
    }
    //2.Search by SolvedQueries
    if (searchTitle == '' && searchSolvedQueries!=false && searchUnSolvedQueries==false) {
      this.data = this.filteredData.filter((item) => item.isSolved== true);
    }
    //3. Search by UnsolvedQueries
    if (searchTitle == '' && searchSolvedQueries==false && searchUnSolvedQueries!=false) {
        this.data = this.filteredData.filter(item=> item.isSolved == false);
    }
    //4. Search by title and unsolved Queries
    if (searchTitle.length != 0 && searchSolvedQueries==false && searchUnSolvedQueries!=false) {
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) &&  item.isSolved == false });
    }
    //5. search by title and Solved Queries
    if (searchTitle.length != 0 && searchSolvedQueries!=false && searchUnSolvedQueries==false) {
      this.data = this.filteredData.filter(item => { return item.title.toLowerCase().includes(searchTitle.toLowerCase()) &&  item.isSolved == true});
    }
}

// reset_filter() {
//   this.searchUnSolvedQueries = false;
//   this.searchSolvedQueries = false;
//   this.filteredData=this.data
// }
}

