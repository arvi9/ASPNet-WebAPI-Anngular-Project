import { HttpClient } from '@angular/common/http';
import { Byte } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { Article } from 'Models/Article';
declare var CKEDITOR: any;

@Component({
  selector: 'app-create-article-page',
  templateUrl: './create-article-page.component.html',
  styleUrls: ['./create-article-page.component.css']
})
export class CreateArticlePageComponent implements OnInit {


  imageError: string = "";
  isImageSaved: boolean = false;
  cardImageBase64: string = "";
  test: any;
  public ckeditorContent: string = "";
 

  constructor(private http: HttpClient) { }
  article: any = {
    articleId:0,
    title:'',
    content:'',
    image: [],
    articleStatusID:1,
    reviewerId: 0,
    datetime:'',
    createdBy:1,
    createdOn:'',
    updatedBy:0,
    ImageString:this.cardImageBase64,
    updatedOn:'',
    articleStatus:null,
    user:null,
    articleComments:[],
    articleLikes: null,
  }


  ngOnInit(): void {
    // Set The Name of ckEditor
    CKEDITOR.on("instanceCreated", function (event: { editor: any; }, data: any) {
      var editor = event.editor,
        element = editor.element;
      editor.name = "content";
    });
  }
  getData() {
    console.log(CKEDITOR.instances.content.getData());
  }


  onSubmit() {
    const headers = { 'content-type': 'application/json' }

    console.log(this.article)
    this.http.post<any>('https://localhost:7197/Article/CreateArticle', this.article, { headers: headers })
      .subscribe((data) => {

        console.log(data)

      });
  }








  fileChangeEvent(fileInput: any) {
    this.imageError = "";
    if (fileInput.target.files && fileInput.target.files[0]) {

      const max_size = 20971520;
      const allowed_types = ['image/png', 'image/jpeg'];

      if (fileInput.target.files[0].size > max_size) {
        this.imageError =
          'Maximum size allowed is ' + max_size / 1000 + 'Mb';

        return false;
      }
      console.log(fileInput.target.files[0].type)

      if (!allowed_types.includes(fileInput.target.files[0].type)) {
        this.imageError = 'Only Images are allowed ( JPG | PNG )';
        return false;
      }
      const reader = new FileReader();
      reader.onload = (e: any) => {
        const image = new Image();
        image.src = e.target.result;
        image.onload = rs => {

          const imgBase64Path = e.target.result;
          this.cardImageBase64 = imgBase64Path;
          this.isImageSaved = true;
          console.log(this.cardImageBase64)

        }

      };

      reader.readAsDataURL(fileInput.target.files[0]);
    } return false
  }




}