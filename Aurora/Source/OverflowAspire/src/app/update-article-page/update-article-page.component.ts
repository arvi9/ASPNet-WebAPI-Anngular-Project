import { Component, OnInit } from '@angular/core';
import { application } from 'Models/Application';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Article } from 'Models/Article';
import { catchError } from 'rxjs';


@Component({
  selector: 'app-update-article-page',
  templateUrl: './update-article-page.component.html',
  styleUrls: ['./update-article-page.component.css']
})
export class UpdateArticlePageComponent implements OnInit {
  imageError: string = "";
  isImageSaved: boolean = false;
  cardImageBase64: string = "";


  articleId: number = 0
  
  constructor(private route: ActivatedRoute, private http: HttpClient,private routing:Router) { }
  article: any = {
    articleId: 0,
    title: '',
    content: '',
    image: "",
    articleStatusID: 1,
    reviewerId: 0,
    datetime: Date.now,
    createdBy: 1,
    createdOn: Date.now,
    updatedBy: 0,
    ImageString: this.cardImageBase64,
    updatedOn: '',
    articleStatus: null,
    user: null,

    articleComments: [],
    articleLikes: null
  }
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.articleId = params['articleId'];
    console.log(this.articleId)
    this.http
      .get<any>(`${application.URL}/Article/GetArticleById?ArticleId=${this.articleId}`)
      .subscribe((data) => {
        this.article = data;
        console.log(data);
      });
    });
  }
  public data: Article = new Article();

  onSubmit() {
    const headers = { 'content-type': 'application/json' }
   
    this.article.articleStatusID = 2;
   
    this.http.put<any>(`${application.URL}/Article/UpdateArticle`, this.article, { headers: headers })
      .pipe(catchError(this.handleError)).subscribe((data) => {
        console.log(data)
      });
      this.routing.navigateByUrl("/MyArticles");
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
    const headers = { 'content-type': 'application/json' }
    this.http.put<any>(`${application.URL}/Article/UpdateArticle`, this.article, { headers: headers })
      .subscribe((data) => {
        console.log(data)
      });
      this.routing.navigateByUrl("/MyArticles");
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
