
import { Component, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Query } from 'Models/Query';
import{QueryComment} from 'Models/Query';

declare type myarray = Array<{ content: string, coding: string, name: string }>
@Component({
  selector: 'app-specificquery',
  templateUrl: './specificquery.component.html',
  styleUrls: ['./specificquery.component.css']
})
export class SpecificqueryComponent implements OnInit {
 @Input() Querysrc : string="https://localhost:7197/Query/GetQuery?QueryId=1021";
 totalLength :any;
 page : number= 1;

 constructor(private http: HttpClient){}

 ngOnInit(): void {
   this.http
   .get<any>(this.Querysrc)
   .subscribe((data)=>{
     this.data =data;
     this.totalLength=data.length;
     console.log(data);
   });
 }


 public data:Query =new Query();
public data1:QueryComment=new QueryComment();


 
 displayStyle = "none";
 openpopup() {
   this.displayStyle = "block";
 }
 dismisspopup() {
   this.displayStyle = "none";
 }
 closePopup() {
   this.displayStyle = "none";
 }
 PostComment(){  
      
  console.log(this.data1.comment)
    }
    PostCode(){    
      console.log(this.data1.code)
        }
}