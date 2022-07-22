import { Component, OnInit} from '@angular/core';
import { AuthService } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';
import { Toaster } from 'ngx-toast-notifications';
import { ConnectionService } from 'src/app/Services/connection.service';
import { catchError } from 'rxjs';


export interface sharedItem {
  display: string,
  value: number
}

declare var CKEDITOR: any;

@Component({
  selector: 'app-create-article-page',
  templateUrl: './create-article-page.component.html',
  styleUrls: ['./create-article-page.component.css']
})

export class CreateArticlePageComponent implements OnInit {
  constructor(private connection: ConnectionService, private route: Router, private toaster: Toaster) { }

  sharedUsersId: any = []
  IsLoadingSubmit: boolean = false;
  IsLoadingSaveDraft: boolean = false;
  imageError: string = "";
  isImageSaved: boolean = false;
  cardImageBase64: string = "";
  error = "";
  article: any = {
    articleId: 0,
    title: '',
    content: '',
    image: "",
    articleStatusID: 1,
    datetime: new Date(),
    createdBy: 0,
    createdOn: new Date(),
    updatedBy: null,
    isPrivate: false,
    ImageString: this.cardImageBase64,
    reviewerId: null,
    Reason: null,
  }

  public items = [
    { display: '', value: 0 },
  ];

  public goalitems: sharedItem[] = []

  privateArticle: any = {
    article: this.article,
    SharedusersId: []
  }



  ngOnInit(): void {
    
 if (AuthService.GetData("token") == null) {
this.toaster.open({ text: 'Your Session has been Expired', position: 'top-center', type: 'warning' })
      this.route.navigateByUrl("")
    }
    CKEDITOR.on("instanceCreated", (event: { editor: any; }, data: any) => {
      var editor = event.editor;
      editor.name = "content";
      this.connection.GetEmployeePage().subscribe((data: any[]) => {
        data.forEach(item => this.items.push({ display: item.email, value: item.userId }))
      })
    });
  }

  //Create and post the article.
  onSubmit() {
    this.IsLoadingSubmit = true;
    this.article.articleStatusID = 2;
    if (this.goalitems.length == 0) {
      this.connection.CreateArticle(this.article)
        .pipe(catchError(this.handleError)).subscribe({
          next: (data: any) => {
            this.toaster.open({ text: 'Article submitted successfully', position: 'top-center', type: 'success' })
            this.route.navigateByUrl("/MyArticles");
          },
          error: (error) => {
            this.error = error.error.message;
            this.IsLoadingSubmit = false;
          }
        });
    }
    //Create private article.
    else {
      this.article.isPrivate = true;
      this.goalitems.forEach(item => this.sharedUsersId.push(item.value))
      this.privateArticle = {
        article: this.article,
        sharedUsersId: this.sharedUsersId
      }
      this.connection.CreatePrivateArticle(this.privateArticle)
        .pipe(catchError(this.handleError)).subscribe({
          next: (data: any) => {
            this.toaster.open({ text: 'Article submitted successfully', position: 'top-center', type: 'success' })
            this.route.navigateByUrl("/MyArticles");
          },
          error: (error) => {
            this.error = error.error.message;
            this.IsLoadingSubmit = false;
          }
        });
    }

  }

  handleError(error: any) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Error: ${error.error.message}`;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.error}`;
    }
    return "";
  }

  //Create article and save it to draft.
  saveToDraft() {
    this.IsLoadingSaveDraft = true;
    if (this.goalitems.length == 0) {
      this.connection.CreateArticle(this.article)
        .subscribe({
          next: (data: any) => {
            this.toaster.open({ text: 'Article saved to draft', position: 'top-center', type: 'success' })
            this.route.navigateByUrl("/MyArticles");
          },
          error: (error) => {
            this.error = error.error.message;
            this.IsLoadingSaveDraft = false;

          }
        });
    }
    else {
      this.article.isPrivate = true;
      this.goalitems.forEach(item => this.sharedUsersId.push(item.value))
      this.privateArticle = {
        article: this.article,
        sharedUsersId: this.sharedUsersId
      }
      this.connection.CreatePrivateArticle(this.privateArticle)
        .pipe(catchError(this.handleError)).subscribe({
          next: (data: any) => {
            this.toaster.open({ text: 'Article submitted successfully', position: 'top-center', type: 'success' })
            this.route.navigateByUrl("/MyArticles");
          },
          error: (error) => {
            this.error = error.error.message;
            this.IsLoadingSubmit = false;
          }
        });
    }

  }
  //Add image in article.
  fileChangeEvent(fileInput: any) {
    this.imageError = "";
    if (fileInput.target.files && fileInput.target.files[0]) {
      const max_size = 500000;
      const allowed_types = ['image/png', 'image/jpeg'];
      if (fileInput.target.files[0].size > max_size) {
        this.imageError =
          'Maximum size allowed is ' + max_size / 100000 + 'Mb';
        return false;
      }

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
          this.cardImageBase64 = this.cardImageBase64.replace("data:image/png;base64,", "");
          this.cardImageBase64 = this.cardImageBase64.replace("data:image/jpg;base64,", "");
          this.cardImageBase64 = this.cardImageBase64.replace("data:image/jpeg;base64,", "");
          this.article.ImageString = this.cardImageBase64;
          this.isImageSaved = true;
        }
      };
      reader.readAsDataURL(fileInput.target.files[0]);
    } return false
  }

}
