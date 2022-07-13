import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Article } from 'Models/Article';
import { Toaster } from 'ngx-toast-notifications';
import { catchError } from 'rxjs';
import { AuthService } from 'src/app/Services/auth.service';
import { ConnectionService } from 'src/app/Services/connection.service';


@Component({
  selector: 'app-update-article-page',
  templateUrl: './update-article-page.component.html',
  styleUrls: ['./update-article-page.component.css']
})

export class UpdateArticlePageComponent implements OnInit {
  @Input() articleId: number = 0
  imageError: string = "";
  IsLoadingSubmit: boolean = false;
  IsLoadingSaveDraft: boolean = false;
  isImageSaved: boolean = false;
  cardImageBase64: string = "";
  article: any = {
    articleId: 0,
    title: '',
    content: '',
    image: "",
    articleStatusID: 1,
    reviewerId: 0,
    createdBy: 1,
    sharedUsers:null,
    ImageString: this.cardImageBase64,
    updatedOn: new Date(),
    Reason:null,
  }
  public data: Article = new Article();

  constructor(private route: ActivatedRoute, private connection: ConnectionService, private router: Router, private toaster: Toaster) { }

  //Get article by its id.
  ngOnInit(): void {
    if (AuthService.GetData("token") == null) this.router.navigateByUrl("")
    this.route.params.subscribe(params => {
      this.articleId = params['articleId'];
      this.connection.GetArticle(this.articleId)
        .subscribe({
          next: (data: { articleId: any; title: any; content: any; image: any;sharedUsers:any }) => {
            console.log(data)
            this.article.articleId = data.articleId;
            this.article.title = data.title;
            this.article.content = data.content;
            this.article.ImageString = data.image
            this.article.sharedUsers=data.sharedUsers
            console.log(this.article.sharedUsers)
          }
        });
    });
  }

  // Update article and submit.
  onSubmit() {
    this.article.articleStatusID = 2;
    this.connection.UpdateArticle(this.article)
      .pipe(catchError(this.handleError)).subscribe({
        next: (data: any) => {
          this.toaster.open({ text: 'Article Submitted Succesfully', position: 'top-center', type: 'success' })
          this.router.navigateByUrl("/MyArticles");
        }
      });
  }

  saveToDraft() {
    this.connection.UpdateArticle(this.article)
      .subscribe({
        next: (data: any) => {
          this.toaster.open({ text: 'Article saved to draft', position: 'top-center', type: 'warning' })
          this.router.navigateByUrl("/MyArticles");
        }
      });

  }


  handleError(error: any) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.error.message}`;
    }
    return "";
  }

  //Add image to the article.
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
