import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ArticlesComponent } from './articles/articles.component';
import { HomepageComponent } from './homepage/homepage.component';
import { LatestArticlepageComponent } from './latest-articlepage/latest-articlepage.component';
import { LatestQueriesComponent } from './latest-queries/latest-queries.component';
import { LoginComponent } from './login/login.component';
import { MyArticlesComponent } from './my-articles/my-articles.component';
import { MyQueriesComponent } from './my-queries/my-queries.component';
import { ProfileComponent } from './profile/profile.component';
import { QueriesComponent } from './queries/queries.component';
import { RaisequeryComponent } from './raisequery/raisequery.component';
import { RegisterComponent } from './register/register.component';
import { SpecificarticleComponent } from './specificarticle/specificarticle.component';
import { SpecificqueryComponent } from './specificquery/specificquery.component';
import { TrendingArticlepageComponent } from './trending-articlepage/trending-articlepage.component';
import { TrendingQueriespageComponent } from './trending-queriespage/trending-queriespage.component';
import { CreateArticlePageComponent } from './create-article-page/create-article-page.component';
import { ReviewerdashboardComponent } from './reviewerdashboard/reviewerdashboard.component';
import { ToreviewpageComponent } from './toreviewpage/toreviewpage.component';
import { ArticalreviewedpageComponent } from './articalreviewedpage/articalreviewedpage.component';
import { ToreviewspecificpageComponent } from './toreviewspecificpage/toreviewspecificpage.component';
import { ArticlereviewedspecificpageComponent } from './articlereviewedspecificpage/articlereviewedspecificpage.component';
import { SpamViewComponent } from './spam-view/spam-view.component';
import { AdmindashboardComponent } from './admindashboard/admindashboard.component';
import { EmployeePageComponent } from './employee-page/employee-page.component';
import { SpamreportpageComponent } from './spamreportpage/spamreportpage.component';
import { UserverificationpageComponent } from './userverificationpage/userverificationpage.component';
import { ReportSpamComponent } from './report-spam/report-spam.component';
import { DialogComponent } from './dialog/dialog.component';
import{EditarticleComponent} from './editarticle/editarticle.component';
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
{path:'dialog',component:DialogComponent},
{path:'Editarticle/:articleId',component:EditarticleComponent}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
