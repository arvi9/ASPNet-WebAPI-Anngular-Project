import { Component, OnInit } from '@angular/core';
import {Chart} from 'chart.js';
@Component({
  selector: 'app-piechart',
  templateUrl: './piechart.component.html',
  styleUrls: ['./piechart.component.css']
})
export class PiechartComponent implements OnInit {

  Piechart=[];
  
  ngOnInit() : void{
    new Chart('piechart',{
      type:'pie',
      data:{
      labels:["Total Number of Articles","Total Number of Queries"],
      datasets:[{
        data:[7000,3000],

      
      }]
    },
    options: {
      plugins: {
        legend: {
          position: 'top',
          display:true,
        },
      },
      
    }
  });

}
}