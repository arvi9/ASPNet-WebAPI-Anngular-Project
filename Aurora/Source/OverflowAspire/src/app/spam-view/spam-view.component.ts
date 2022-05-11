import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-spam-view',
  templateUrl: './spam-view.component.html',
  styleUrls: ['./spam-view.component.css']
})
export class SpamViewComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
  title = "Stack and Heap";
  content="What is Stack and Heap? Programming language books explain that value types are created on the stack, and reference types are created on the heap, without explaining what these two things are. I haven't read a clear explanation of this. I understand what a stack is. But, Where and what are they (physically in a real computer's memory)? To what extent are they controlled by the OS or language run-time?  What is their scope What determines the size of each of them? What makes one faster";
  Reason="Reason for report"
  coment:any[]=[{comment:"Very Good",Name:"Pooja"},{comment:"Good for learning",Name:"venky"},{comment:"Need for study",Name:"sandhiya"},{comment:"Intersting to learn",Name:"Girish"},{comment:"Need for study",Name:"ponvizhi"}]
}
