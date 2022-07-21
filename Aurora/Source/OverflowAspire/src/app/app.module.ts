import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { NgxPaginationModule } from 'ngx-pagination';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { NgChartsModule } from 'ng2-charts';
import { DataTablesModule } from 'angular-datatables';
import { MatButtonModule } from '@angular/material/button';
import { CKEditorModule } from 'ckeditor4-angular';
import { AppComponent } from './app.component';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { ToastNotificationsModule } from 'ngx-toast-notifications';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { HtmlToPlaintextPipe } from './Services/html-to-plaintext.pipe';
import { MatDialogModule } from '@angular/material/dialog';
import { NgxSpinnerModule } from "ngx-spinner";
import { ReactiveFormsModule } from '@angular/forms';
import { TagInputModule } from 'ngx-chips';
import { HashLocationStrategy,LocationStrategy } from '@angular/common';
import { LoadingBarHttpClientModule } from '@ngx-loading-bar/http-client';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AdmindashboardComponent } from './Admin Pages/admindashboard/admindashboard.component';
import { EmployeePageComponent } from './Admin Pages/employee-page/employee-page.component';
import { SpamViewComponent } from './Admin Pages/spam-view/spam-view.component';
import { SpamreportpageComponent } from './Admin Pages/spamreportpage/spamreportpage.component';
import { UserverificationpageComponent } from './Admin Pages/userverificationpage/userverificationpage.component';
import { AdminNavbarComponent } from './Components/admin-navbar/admin-navbar.component';
import { ArticleCardComponent } from './Components/article-card/article-card.component';
import { FooterComponent } from './Components/footer/footer.component';
import { NavbarComponent } from './Components/navbar/navbar.component';
import { QueryCardComponent } from './Components/query-card/query-card.component';
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
import { AgoPipe } from './Services/ago.pipe';
import { PlayFeatureComponent } from './Components/play-feature/play-feature.component';


@NgModule({
  declarations: [
    AppComponent,
    HtmlToPlaintextPipe,
    AdmindashboardComponent,
    EmployeePageComponent,
    SpamViewComponent,
    SpamreportpageComponent,
    UserverificationpageComponent,
    AdminNavbarComponent,
    ArticleCardComponent,
    FooterComponent,
    NavbarComponent,
    QueryCardComponent,
    ArticalreviewedpageComponent,
    ArticlereviewedspecificpageComponent,
    ReviewerdashboardComponent,
    ToreviewpageComponent,
    ToreviewspecificpageComponent,
    LoginComponent,
    RegisterComponent,
    ArticlesComponent,
    CreateArticlePageComponent,
    EditarticleComponent,
    HomepageComponent,
    LatestArticlepageComponent,
    LatestQueriesComponent,
    MyArticlesComponent,
    MyQueriesComponent,
    MyqueryspecificComponent,
    ProfileComponent,
    QueriesComponent,
    RaisequeryComponent,
    ReportSpamComponent,
    SpecificarticleComponent,
    SpecificqueryComponent,
    TrendingArticlepageComponent,
    TrendingQueriespageComponent,
    UpdateArticlePageComponent,
    PrivatearticlesComponent,
    RejectreasonComponent,
    AgoPipe,
    PlayFeatureComponent,
    
 
    
    
    
  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    DataTablesModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatDialogModule,
    NgxPaginationModule,
    MatCardModule, NgbCollapseModule,
    FormsModule,
    NgChartsModule,
    MatButtonModule,
    ScrollingModule,
    CKEditorModule,
    Ng2SearchPipeModule,
    NgxSpinnerModule,
    ReactiveFormsModule,
    ToastNotificationsModule.forRoot({duration: 6000, type: 'primary'}),
    TagInputModule,
    LoadingBarHttpClientModule,
    BrowserAnimationsModule

    
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [{provide:LocationStrategy, useClass:HashLocationStrategy}],
  bootstrap: [AppComponent]
})
export class AppModule { }
