import { Component, OnInit ,Input} from '@angular/core';

@Component({
  selector: 'app-article-content',
  template: '',
  styleUrls: ['./article-content.component.css']
})
export class ArticleContentComponent implements OnInit {
@Input() content:any="<P>Helllo</P>"
  constructor() { }
  ngOnInit(): void {
  }

}
