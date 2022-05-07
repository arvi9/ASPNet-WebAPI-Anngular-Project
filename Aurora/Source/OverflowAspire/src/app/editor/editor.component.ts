import { Component, OnInit } from '@angular/core';
declare var CKEDITOR: any;

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.css']
})
export class EditorComponent implements OnInit {

  public ckeditorContent: string = "";

  ngOnInit(): void {
    // Set The Name of ckEditor
    CKEDITOR.on("instanceCreated", function(event: { editor: any; }, data: any) {
      var editor = event.editor,
        element = editor.element;
      editor.name = "content";
    });
  }

  getData() {
    console.log(CKEDITOR.instances.content.getData());
  }

}
