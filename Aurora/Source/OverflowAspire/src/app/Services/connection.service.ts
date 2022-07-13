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

  initializeTokenHeader(token:string|null){
    this.headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    })
  }
  //Get Current User
  GetCurrentApplicationUser(): any {
    return this.http.get<any>(this.URL + 'User/GetCurrentApplicationUser', { headers: this.headers });
  }

  //Get admin dashboard
  GetAdminDashboard(): any {
    return this.http.get<any>(this.URL + 'Dashboard/GetAdminDashboard', { headers: this.headers });
  }
  
  // Get Employee page
  GetEmployeePage(): any {
    return this.http.get<any>(this.URL + 'User/GetUsersByUserRoleId?RoleId=2', { headers: this.headers });
  }
  // Get spams
  GetSpams(): any {
    return this.http.get<any>(this.URL + 'Query/GetListOfSpams', { headers: this.headers });
  }

  //Get Users
  GetUsers(): any {
    return this.http.get<any>(this.URL + 'User/GetUsersByVerifyStatusId?VerifyStatusID=3', { headers: this.headers });
  }

  // Get Reviewed Articles.
  GetReviewedArticles(): any {
    return this.http.get<any>(this.URL + 'Article/GetArticlesByArticleStatusId?ArticleStatusID=4', { headers: this.headers });
  }

  //Get Under Review Articles.
  GetUnderReviewArticles(): any {
    return this.http.get<any>(this.URL + 'Article/GetArticlesByArticleStatusId?ArticleStatusID=3', { headers: this.headers });
  }

  //Get to review articles.
  GetToReviewArticles(): any {
    return this.http.get<any>(this.URL + 'Article/GetArticlesByArticleStatusId?ArticleStatusID=2', { headers: this.headers });
  }

    //Get articles.
  GetArticle(articleid:number): any {
    return this.http.get<any>(this.URL + `Article/GetArticleById?ArticleId=${articleid}`, { headers: this.headers });
  }

  //Get reviewer dashboard.
  GetReviewerDashboard(): any {
    return this.http.get<any>(this.URL + 'Dashboard/GetReviewerDashboard?ReviewerId=1', { headers: this.headers });
  }

  //Get Homepage
  GetHomePage(): any {
    return this.http.get<any>(this.URL + 'Dashboard/GetHomePage?DataRange=3', { headers: this.headers });
  }

    //Get My Articles.
  GetMyArticles(): any {
    return this.http.get<any>(this.URL + 'Article/GetArticlesByUserId', { headers: this.headers });
  }

  //Get Private articles.
  GetPrivateArticles(): any {
    return this.http.get<any>(this.URL + 'Article/GetPrivateArticles', { headers: this.headers });
  }

    //Get My Queries.
  GetMyQueries(): any {
    return this.http.get<any>(this.URL + 'Query/GetQueriesByUserId', { headers: this.headers });
  } 

    //Get Query.
  GetQuery(queryid:number): any {
    return this.http.get<any>(this.URL + `Query/GetQuery?QueryId=${queryid}`, { headers: this.headers });
  }

    //Get User.
  GetUserById(UserId:number): any {
    return this.http.get<any>(this.URL + `User/GetUserById=${UserId}`, { headers: this.headers });
    
  }


  //Admin can disable user 
  DisableUser(userId:number): any {
    return this.http.patch(this.URL + `User/ChangeUserVerifyStatus?UserId=${userId}&IsVerified=false`,Object,{ headers: this.headers })  
  }

    //Admin can mark user as reviewer. 
  MarkAsReviewer(userId:number): any {
    return this.http.patch(this.URL + `User/UpdateUserByIsReviewer?UserId=${userId}&IsReviewer=true`,Object,{ headers: this.headers })  
  }

    //Admin can unmark user as reviewer.
  UnmarkAsReviewer(userId:number): any {
    return this.http.patch(this.URL + `User/UpdateUserByIsReviewer?UserId=${userId}&IsReviewer=False`,Object,{ headers: this.headers })  
  }
  
  //Admin can approve spam
  ApproveSpam(queryId:number): any {
    return this.http.patch(this.URL + `Query/UpdateSpamStatus?QueryId=${queryId}&VerifyStatusID=1`,Object,{ headers: this.headers })  
  }

    //Admin can reject spam query.
  RejectSpam(queryId:number): any {
    return this.http.patch(this.URL + `Query/UpdateSpamStatus?QueryId=${queryId}&VerifyStatusID=2`,Object,{ headers: this.headers })  
  }
  
  //Admin can remove query.
  RemoveQuery(queryId:number): any {
    return this.http.delete(this.URL + `Query/RemoveQueryByQueryId?QueryId=${queryId}`,{ headers: this.headers })  
  }
  //Admin can approve user.
  ApproveUser(userId:number): any {
    return this.http.patch(this.URL + `User/ChangeUserVerifyStatus?UserId=${userId}&IsVerified=true`,Object,{ headers: this.headers })  
  }

    //Admin can remove user.
  RemoveUser(userId:number): any {
    return this.http.delete(this.URL + `User/RemoveUser?UserId=${userId}`,{ headers: this.headers })  
  }

    //Reviewer can approve article.
  ApproveArticle(articleId:number): any {
    return this.http.patch(this.URL + `Article/ChangeArticleStatus?ArticleId=${articleId}&ArticleStatusID=4`,Object,{ headers: this.headers })  
  
  }
  //Change article status to under review.
  ChangeToUnderReview(articleId:number): any {
    return this.http.patch(this.URL + `Article/ChangeArticleStatus?ArticleId=${articleId}&ArticleStatusID=3`,Object,{ headers: this.headers })  
  }

  // Reviewer can reject article
  RejectArticle(articleId:number): any {
    return this.http.patch(this.URL + `Article/ChangeArticleStatus?ArticleId=${articleId}&ArticleStatusID=1`,Object,{ headers: this.headers })  
  }

  //User can create article.
  CreateArticle(article: any) {
    return this.http.post<any>(this.URL + 'Article/CreateArticle', article, { headers: this.headers })
  }

    // User can create private article
  CreatePrivateArticle(privatearticle: any) {
    return this.http.post<any>(this.URL + 'Article/CreatePrivateArticle', privatearticle, { headers: this.headers })
  }

  //User can mark query as solved.
  MarkQueryAsSolved(queryId:number): any {
    return this.http.patch(this.URL + `Query/MarkQueryAsSolved?QueryId=${queryId}`,Object,{ headers: this.headers })  
  }

  //User can create query.
  CreateQuery(query: any) {
    return this.http.post<any>(this.URL + 'Query/CreateQuery', query, { headers: this.headers })
  }

  //User can report query as spam.
  ReportSpam(spam: any) {
    return this.http.post<any>(this.URL + 'Query/AddSpam', spam, { headers: this.headers })
  }

  //User can add like to article.
  AddLike(like:any){
    return this.http.post<any>(this.URL + 'Article/AddLikeToArticle', like, { headers: this.headers })
  }

  //User can post comment to article.
  PostArticleComment(Comment:any){
    return this.http.post<any>(this.URL + 'Article/CreateComment', Comment, { headers: this.headers })
  }

  //User can post comment to query.
  PostQueryComment(Comment:any){
    return this.http.post<any>(this.URL + 'Query/CreateComment', Comment, { headers: this.headers })
  }

  //User can update draft article. 
  UpdateArticle(article:any){
    return this.http.put<any>(this.URL + 'Article/UpdateArticle', article, { headers: this.headers })
  }

  //User can update draft article. 
  UpdatePrivateArticle(article:any){
    return this.http.put<any>(this.URL + 'Article/UpdatePrivateArticle', article, { headers: this.headers })
  }
    //User can Get all articles.
  GetAllArticles(){
    return this.http.get<any>(this.URL + 'Article/GetArticlesByArticleStatusId?ArticleStatusID=4', { headers: this.headers });
  }

    //User can get latest articles.
  GetLatestArticles(){
    return this.http.get<any>(this.URL + 'Article/GetLatestArticles', { headers: this.headers });
  }

    //User can get Trending articles.
  GetTrendingArticles(){
    return this.http.get<any>(this.URL + 'Article/GetTrendingArticles?Range=0', { headers: this.headers });
  }

  //User can get all queries.
  GetAllQueries(){
    return this.http.get<any>(this.URL + 'Query/GetAll', { headers: this.headers });
  }

  //User can get latest queries.
  GetLatestQueries(){
    return this.http.get<any>(this.URL + 'Query/GetLatestQueries', { headers: this.headers });
  }

    //User can get trending queries.
  GetTrendingQueries(){
    return this.http.get<any>(this.URL + 'Query/GetTrendingQueries', { headers: this.headers });
  }

  //User can login.
  Login(user:any){
    return this.http.post<any>(this.URL + 'Token/AuthToken', user, { headers: this.headers })
  }

  //New user can register.
  Register(user:any){
    return this.http.post<any>(this.URL + 'User/CreateUser', user, { headers: this.headers })
  }

  //Get genders
  GetGenders(){
    return this.http.get<any>(this.URL + 'User/GetGenders', { headers: this.headers });
  }

  //Get designation.
  GetDesignations(){
    return this.http.get<any>(this.URL + 'User/GetDesignations', { headers: this.headers });
  }

  //Get departments.
  GetDepartments(){
    return this.http.get<any>(this.URL + 'User/GetDepartments', { headers: this.headers });
  }
  


}
