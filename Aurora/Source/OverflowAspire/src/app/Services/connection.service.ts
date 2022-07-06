import { Injectable } from '@angular/core';
import { AuthService } from 'src/app/Services/auth.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ConnectionService {
  URL: string='https://localhost:7197/'
  constructor(private http: HttpClient) { }

  public headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${AuthService.GetData("token")}`
  })

  
  GetAdminDashboard(): any {
    return this.http.get<any>(this.URL + 'Dashboard/GetAdminDashboard', { headers: this.headers });
  }

  GetEmployeePage(): any {
    return this.http.get<any>(this.URL + 'User/GetUsersByUserRoleId?RoleId=2', { headers: this.headers });
  }

  GetSpams(): any {
    return this.http.get<any>(this.URL + 'Query/GetListOfSpams', { headers: this.headers });
  }

  GetUsers(): any {
    return this.http.get<any>(this.URL + 'User/GetUsersByVerifyStatusId?VerifyStatusID=3', { headers: this.headers });
  }

  GetReviewedArticles(): any {
    return this.http.get<any>(this.URL + 'Article/GetArticlesByArticleStatusId?ArticleStatusID=4', { headers: this.headers });
  }

  GetToReviewArticles(): any {
    return this.http.get<any>(this.URL + 'Article/GetArticlesByArticleStatusId?ArticleStatusID=2', { headers: this.headers });
  }

  GetArticle(articleid:number): any {
    return this.http.get<any>(this.URL + `Article/GetArticleById?ArticleId=${articleid}`, { headers: this.headers });
  }

  GetReviewerDashboard(): any {
    return this.http.get<any>(this.URL + 'Dashboard/GetReviewerDashboard?ReviewerId=1', { headers: this.headers });
  }

  GetHomePage(): any {
    return this.http.get<any>(this.URL + 'Dashboard/GetHomePage', { headers: this.headers });
  }

  GetMyArticles(): any {
    return this.http.get<any>(this.URL + 'Article/GetArticlesByUserId', { headers: this.headers });
  }

  GetMyQueries(): any {
    return this.http.get<any>(this.URL + 'Query/GetQueriesByUserId', { headers: this.headers });
  }

  GetQuery(queryid:number): any {
    return this.http.get<any>(this.URL + `Query/GetQuery?QueryId=${queryid}`, { headers: this.headers });
  }

  GetUser(): any {
    return this.http.get<any>(this.URL + `User/GetUser`, { headers: this.headers });
    
  }

  DisableUser(userId:number): any {
    return this.http.patch(this.URL + `User/ChangeUserVerifyStatus?UserId=${userId}&IsVerified=false`,Object,{ headers: this.headers })  
  }

  MarkAsReviewer(userId:number): any {
    return this.http.patch(this.URL + `User/UpdateUserByIsReviewer?UserId=${userId}&IsReviewer=true`,Object,{ headers: this.headers })  
  }

  UnmarkAsReviewer(userId:number): any {
    return this.http.patch(this.URL + `User/UpdateUserByIsReviewer?UserId=${userId}&IsReviewer=False`,Object,{ headers: this.headers })  
  }

  ApproveSpam(queryId:number): any {
    return this.http.patch(this.URL + `Query/UpdateSpamStatus?QueryId=${queryId}&VerifyStatusID=1`,Object,{ headers: this.headers })  
  }

  RejectSpam(queryId:number): any {
    return this.http.patch(this.URL + `Query/UpdateSpamStatus?QueryId=${queryId}&VerifyStatusID=2`,Object,{ headers: this.headers })  
  }
  
  RemoveQuery(queryId:number): any {
    return this.http.delete(this.URL + `Query/RemoveQueryByQueryId?QueryId=${queryId}`,{ headers: this.headers })  
  }

  ApproveUser(userId:number): any {
    return this.http.patch(this.URL + `User/ChangeUserVerifyStatus?UserId=${userId}&IsVerified=true`,Object,{ headers: this.headers })  
  }

  RemoveUser(userId:number): any {
    return this.http.delete(this.URL + `User/RemoveUser?UserId=${userId}`,{ headers: this.headers })  
  }

  ApproveArticle(articleId:number): any {
    return this.http.patch(this.URL + `Article/ChangeArticleStatus?ArticleId=${articleId}&ArticleStatusID=4`,Object,{ headers: this.headers })  
  }

  ChangeToUnderReview(articleId:number): any {
    return this.http.patch(this.URL + `Article/ChangeArticleStatus?ArticleId=${articleId}&ArticleStatusID=3`,Object,{ headers: this.headers })  
  }

  RejectArticle(articleId:number): any {
    return this.http.patch(this.URL + `Article/ChangeArticleStatus?ArticleId=${articleId}&ArticleStatusID=1`,Object,{ headers: this.headers })  
  }

  CreateArticle(article: any) {
    return this.http.post<any>(this.URL + 'Article/CreateArticle', article, { headers: this.headers })
  }

  CreatePrivateArticle(article: any) {
    return this.http.post<any>(this.URL + 'Article/CreatePrivateArticle', article, { headers: this.headers })
  }

  MarkQueryAsSolved(queryId:number): any {
    return this.http.patch(this.URL + `Query/MarkQueryAsSolved?QueryId=${queryId}`,Object,{ headers: this.headers })  
  }

  CreateQuery(query: any) {
    return this.http.post<any>(this.URL + 'Query/CreateQuery', query, { headers: this.headers })
  }

  ReportSpam(spam: any) {
    return this.http.post<any>(this.URL + 'Query/AddSpam', spam, { headers: this.headers })
  }

  AddLike(like:any){
    return this.http.post<any>(this.URL + 'Article/AddLikeToArticle', like, { headers: this.headers })
  }

  PostArticleComment(Comment:any){
    return this.http.post<any>(this.URL + 'Article/CreateComment', Comment, { headers: this.headers })
  }

  PostQueryComment(Comment:any){
    return this.http.post<any>(this.URL + 'Query/CreateComment', Comment, { headers: this.headers })
  }

  UpdateArticle(article:any){
    return this.http.put<any>(this.URL + 'Article/UpdateArticle', article, { headers: this.headers })
  }

  GetAllArticles(){
    return this.http.get<any>(this.URL + 'Article/GetArticlesByArticleStatusId?ArticleStatusID=4', { headers: this.headers });
  }

  GetLatestArticles(){
    return this.http.get<any>(this.URL + 'Article/GetLatestArticles', { headers: this.headers });
  }

  GetTrendingArticles(){
    return this.http.get<any>(this.URL + 'Article/GetTrendingArticles?Range=0', { headers: this.headers });
  }
  
  GetAllQueries(){
    return this.http.get<any>(this.URL + 'Query/GetAll', { headers: this.headers });
  }

  GetLatestQueries(){
    return this.http.get<any>(this.URL + 'Query/GetLatestQueries', { headers: this.headers });
  }

  GetTrendingQueries(){
    return this.http.get<any>(this.URL + 'Query/GetTrendingQueries', { headers: this.headers });
  }

  Login(user:any){
    return this.http.post<any>(this.URL + 'Token/AuthToken', user, { headers: this.headers })
  }

  Register(user:any){
    return this.http.post<any>(this.URL + 'User/CreateUser', user, { headers: this.headers })
  }

  GetGenders(){
    return this.http.get<any>(this.URL + 'User/GetGenders', { headers: this.headers });
  }

  GetDesignations(){
    return this.http.get<any>(this.URL + 'User/GetDesignations', { headers: this.headers });
  }

  GetDepartments(){
    return this.http.get<any>(this.URL + 'User/GetDepartments', { headers: this.headers });
  }
  


}
