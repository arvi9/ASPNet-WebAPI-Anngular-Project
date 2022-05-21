import { Component,Input, OnInit } from '@angular/core';
import { application } from 'Models/Application';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-spam-view',
  templateUrl: './spam-view.component.html',
  styleUrls: ['./spam-view.component.css']
})
export class SpamViewComponent implements OnInit {
queryId: number = 0


@Input() Querysrc : string = `${application.URL}/Query/GetListOfSpams`;
  data: any;

constructor(private route: ActivatedRoute,private http: HttpClient) { }

ngOnInit(): void {
  this.route.params.subscribe(params => {
    this.queryId = params['queryId'];
  this.http
  .get<any>(this.Querysrc)
  .subscribe((data)=>{
    this.data =data;
    this.data=this.data.filter((item: { query: { queryId: number; }; })=> item.query.queryId==this.queryId)
    console.log(data);
  });
});
}
}