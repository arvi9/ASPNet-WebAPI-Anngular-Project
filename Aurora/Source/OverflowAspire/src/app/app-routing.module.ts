import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdmindashboardComponent } from './Admin Pages/admindashboard/admindashboard.component';
import { EmployeePageComponent } from './Admin Pages/employee-page/employee-page.component';
import { SpamViewComponent } from './Admin Pages/spam-view/spam-view.component';
import { SpamreportpageComponent } from './Admin Pages/spamreportpage/spamreportpage.component';
import { UserverificationpageComponent } from './Admin Pages/userverificationpage/userverificationpage.component';
import { ArticalreviewedpageComponent } from './Reviewer Pages/articalreviewedpage/articalreviewedpage.component';
import { ArticlereviewedspecificpageComponent } from './Reviewer Pages/articlereviewedspecificpage/articlereviewedspecificpage.component';
import { ReviewerdashboardComponent } from './Reviewer Pages/reviewerdashboard/reviewerdashboard.component';
import { ToreviewpageComponent } from './Reviewer Pages/toreviewpage/toreviewpage.component';
import { ToreviewspecificpageComponent } from './Reviewer Pages/toreviewspecificpage/toreviewspecificpage.component';
import { LoginComponent } from './Shared Pages/login/login.component';
import { RegisterComponent } from './Shared Pages/register/register.component';
import { ArticlesComponent } from './User Pages/articles/articles.component';
import { CreateArticlePageComponent } from './User Pages/create-article-page/create-article-page.component';
import { EditarticleComponent } from './User Pages/editarticle/editarticle.component';
import { HomepageComponent } from './User Pages/homepage/homepage.component';
import { LatestArticlepageComponent } from './User Pages/latest-articlepage/latest-articlepage.component';
import { LatestQueriesComponent } from './User Pages/latest-queries/latest-queries.component';
import { MyArticlesComponent } from './User Pages/my-articles/my-articles.component';
import { MyQueriesComponent } from './User Pages/my-queries/my-queries.component';
import { MyqueryspecificComponent } from './User Pages/myqueryspecific/myqueryspecific.component';
import { ProfileComponent } from './User Pages/profile/profile.component';
import { QueriesComponent } from './User Pages/queries/queries.component';
import { RaisequeryComponent } from './User Pages/raisequery/raisequery.component';
import { ReportSpamComponent } from './User Pages/report-spam/report-spam.component';
import { SpecificarticleComponent } from './User Pages/specificarticle/specificarticle.component';
import { SpecificqueryComponent } from './User Pages/specificquery/specificquery.component';
import { TrendingArticlepageComponent } from './User Pages/trending-articlepage/trending-articlepage.component';
import { TrendingQueriespageComponent } from './User Pages/trending-queriespage/trending-queriespage.component';
import { UpdateArticlePageComponent } from './User Pages/update-article-page/update-article-page.component';
import { PrivatearticlesComponent } from './User Pages/privatearticles/privatearticles.component';
import { RejectreasonComponent } from './Reviewer Pages/rejectreason/rejectreason.component';



const routes: Routes = [
  {path:'',component:LoginComponent},
  {path:'Register',component:RegisterComponent},
  {path:'Home',component:HomepageComponent},
  {path:'MyProfile',component:ProfileComponent}, 
  {path:'Articles',component:ArticlesComponent},
  {path:'LatestArticles',component:LatestArticlepageComponent},
  {path:'TrendingArticles',component:TrendingArticlepageComponent},
  {path:'specificarticle/:articleId',component:SpecificarticleComponent},
  {path:'MyArticles',component:MyArticlesComponent},
  {path:'CreateArticle',component:CreateArticlePageComponent},
  {path:'Queries',component:QueriesComponent},
  {path:'LatestQueries',component:LatestQueriesComponent},
  {path:'TrendingQueries',component:TrendingQueriespageComponent},
  {path:'SpecificQuery/:queryId',component:SpecificqueryComponent},
  {path:'RaiseQuery',component:RaisequeryComponent},
  {path:'MyQueries',component:MyQueriesComponent},
  {path:'RaiseQuery',component:RaisequeryComponent},
  {path:'ReviewerDashboard',component:ReviewerdashboardComponent},
  {path:'ToReview',component:ToreviewpageComponent},
  {path:'ArticleReviewed',component:ArticalreviewedpageComponent},
  {path:'ToReviewSpecific/:articleId',component:ToreviewspecificpageComponent},
  {path:'ArticleReviewedSpecific/:articleId',component:ArticlereviewedspecificpageComponent},
  {path:'SpamView/:queryId',component:SpamViewComponent},
  {path:'AdminDashboard',component:AdmindashboardComponent},
  {path:'Employee',component:EmployeePageComponent},
  {path:'SpamReport',component:SpamreportpageComponent},
  {path:'ReportSpam/:queryId',component:ReportSpamComponent},
  {path:'UserVerification',component:UserverificationpageComponent},
  {path:'UpdateArticle/:articleId',component:UpdateArticlePageComponent},
  {path:'EditArticle/:articleId',component:EditarticleComponent},
  {path:'myqueryspecific/:queryId',component:MyqueryspecificComponent},
  {path:'PrivateArticles',component:PrivatearticlesComponent},
  {path:'ReasonReject/:articleId',component:RejectreasonComponent}, 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
