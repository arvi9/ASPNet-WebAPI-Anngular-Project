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

const routes: Routes = [
  {path:'Login',component:LoginComponent},
  {path:'',component:RegisterComponent},
  {path:'Home',component:HomepageComponent},
  {path:'MyProfile',component:ProfileComponent},
  
  {path:'Articles',component:ArticlesComponent},
  {path:'LatestArticles',component:LatestArticlepageComponent},
  {path:'TrendingArticles',component:TrendingArticlepageComponent},
  {path:'SpecificArticle',component:SpecificarticleComponent},
  {path:'MyArticles',component:MyArticlesComponent},

  {path:'Queries',component:QueriesComponent},
  {path:'LatestQueries',component:LatestQueriesComponent},
  {path:'TrendingQueries',component:TrendingQueriespageComponent},
  {path:'SpecificQuery',component:SpecificqueryComponent},
  {path:'RaiseQuery',component:RaisequeryComponent},
  {path:'MyQueries',component:MyQueriesComponent},
  {path:'RaiseQuery',component:RaisequeryComponent}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
