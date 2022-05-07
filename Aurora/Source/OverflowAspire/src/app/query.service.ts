import { Injectable } from '@angular/core';
@Injectable({
  providedIn: 'root'
})
export class QueryService {

public querytitle:string="Stack And Heap";



  public Query:string = "What is Stack and Heap? programming language books explain that value types are created on the stack, and reference types are created on the heap, without explaining what these two things are. I haven't read a clear explanation of this. I understand what a stack is. But, Where and what are they (physically in a real computer's memory)? To what extent are they controlled by the OS or language run-time? What is their scope? What determines the size of each of them? What makes one faster."
public code:string = "int main() // All these variables get memory // allocated on stack int a; int b[10]; int n = 20; int c[n];"
  getquery(): string {
    return this.Query
  }
  getcode():string{
    return this.code
  }

  gettitle():string{
    return this.querytitle
  }


  constructor() {
  }
}