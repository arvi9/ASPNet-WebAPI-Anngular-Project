import { Component,Input, OnInit } from '@angular/core';
import { Query } from 'Models/Query';
import { AuthService } from 'src/app/Services/auth.service';
import { ConnectionService } from 'src/app/Services/connection.service';

@Component({
  selector: 'app-my-queries',
  templateUrl: './my-queries.component.html',
  styleUrls: ['./my-queries.component.css']
})
export class MyQueriesComponent implements OnInit {
  totalLength: any;
  page: number = 1;
  searchTitle="";
  searchUnSolvedQueries=false;
  searchSolvedQueries=false;

  constructor(private connection:ConnectionService) { }
  ngOnInit(): void {
   this.connection.GetMyQueries()
      .subscribe({next:(data:any[]) => {
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
}


