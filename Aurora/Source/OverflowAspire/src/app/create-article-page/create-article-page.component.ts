import { HttpClient, HttpHeaders } from '@angular/common/http';

import { ApplicationInitStatus, Component, OnInit } from '@angular/core';
import { Article } from 'Models/Article';
import { application } from 'Models/Application'
import { catchError } from 'rxjs';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { Toaster } from 'ngx-toast-notifications';


declare var CKEDITOR: any;

@Component({
  selector: 'app-create-article-page',
  templateUrl: './create-article-page.component.html',
  styleUrls: ['./create-article-page.component.css']
})
export class CreateArticlePageComponent implements OnInit {

  IsLoadingSubmit:boolean=false;

  IsLoadingSaveDraft:boolean=false;

  imageError: string = "";
  isImageSaved: boolean = false;
  cardImageBase64: string = "";
  test: any;
  public ckeditorContent: string = "";


  constructor(private http: HttpClient,private route:Router,private toaster: Toaster) { }
  article: any = {
    articleId: 0,
    title: '',
    content: '',
    image: "",
    articleStatusID: 1,
    reviewerId: 0,
    datetime: Date.now,
    createdBy: 0,
    createdOn: Date.now,
    updatedBy: 0,
    ImageString: this.cardImageBase64,
    updatedOn: '',
    articleStatus: null,
    user: null,
    IsPrivate:true,
    articleComments: [],
    articleLikes: null,

  }
  // privatearticle:any={
  //   article:this.article,
  //   sharedUsersId:[4,2002]
  // }


  ngOnInit(): void {
    if(AuthService.GetData("token")==null) this.route.navigateByUrl("")
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
this.IsLoadingSubmit=true;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${AuthService.GetData("token")}`
    })
    this.article.articleStatusID = 2;
      this.http.post<any>(`${application.URL}/Article/CreateArticle`, this.article, { headers: headers })
      .pipe(catchError(this.handleError)).subscribe({next:(data) => {

        
        console.log(data)

      }});
      this.toaster.open({text: 'Article submitted successfully',position: 'top-center',type: 'success'})
      this.route.navigateByUrl("/MyArticles");
  }
  handleError(error:any) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.error}`;
    }
    console.log(errorMessage);

        return "";

  }

  saveToDraft() {
    this.IsLoadingSaveDraft=true;
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${AuthService.GetData("token")}`
      })
      console.log(AuthService.GetData("token"))



    this.http.post<any>(`${application.URL}/Article/CreateArticle`, this.article, { headers: headers })
      .subscribe({next:(data) => {
      
        console.log(data)

      }});
      this.toaster.open({text: 'Article saved to draft',position: 'top-center',type: 'success'})
      this.route.navigateByUrl("/MyArticles");
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
          this.cardImageBase64= this.cardImageBase64.replace("data:image/png;base64,", "");
          this.cardImageBase64= this.cardImageBase64.replace("data:image/jpg;base64,", "");
          this.cardImageBase64= this.cardImageBase64.replace("data:image/jpeg;base64,", "");
          this.article.ImageString=this.cardImageBase64;
          this.isImageSaved = true;


        }

      };

      reader.readAsDataURL(fileInput.target.files[0]);
    } return false
  }




}
