import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import{BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';

import { NgxPaginationModule } from 'ngx-pagination';
import { FormsModule } from '@angular/forms';
import { MatCardModule} from '@angular/material/card';
import { NgChartsModule} from 'ng2-charts';
import { DataTablesModule } from 'angular-datatables';
import { MatButtonModule} from '@angular/material/button';
import { CKEditorModule } from 'ckeditor4-angular';
import { AppComponent } from './app.component';
import { QueryCardComponent } from './query-card/query-card.component';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { ArticleCardComponent } from './article-card/article-card.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { ReviewerdashboardComponent } from './reviewerdashboard/reviewerdashboard.component';
import { AdmindashboardComponent } from './admindashboard/admindashboard.component';
import { ToreviewspecificpageComponent } from './toreviewspecificpage/toreviewspecificpage.component';
import { ArticlereviewedspecificpageComponent } from './articlereviewedspecificpage/articlereviewedspecificpage.component';
import { SpamViewComponent } from './spam-view/spam-view.component';
import {MatFormFieldModule} from '@angular/material/form-field';
import { QueriesComponent } from './queries/queries.component';
import { AdminNavbarComponent } from './admin-navbar/admin-navbar.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SpecificarticleComponent } from './specificarticle/specificarticle.component';
import { ArticlesComponent } from './articles/articles.component';

import { LatestQueriesComponent } from './latest-queries/latest-queries.component';
import { TrendingQueriespageComponent } from './trending-queriespage/trending-queriespage.component';
import { LatestArticlepageComponent } from './latest-articlepage/latest-articlepage.component';
import { TrendingArticlepageComponent } from './trending-articlepage/trending-articlepage.component';

import { ProfileComponent } from './profile/profile.component';
import { SpecificqueryComponent } from './specificquery/specificquery.component';
import { RaisequeryComponent } from './raisequery/raisequery.component';
import { ReportSpamComponent} from './report-spam/report-spam.component';
import { MyArticlesComponent } from './my-articles/my-articles.component';
import { MyQueriesComponent } from './my-queries/my-queries.component';
import { AdminPiechartComponent } from './admin-piechart/admin-piechart.component';
import { ReviewerPiechartComponent } from './reviewer-piechart/reviewer-piechart.component';
import { HomepageComponent } from './homepage/homepage.component';

import { EmployeePageComponent } from './employee-page/employee-page.component';
import { SidenavComponent } from './sidenav/sidenav.component';
import { SpamreportpageComponent } from './spamreportpage/spamreportpage.component';
import { UserverificationpageComponent } from './userverificationpage/userverificationpage.component';
import { ToreviewpageComponent } from './toreviewpage/toreviewpage.component';
import { ArticalreviewedpageComponent } from './articalreviewedpage/articalreviewedpage.component';

import {ScrollingModule} from '@angular/cdk/scrolling';
import { EditorComponent } from './editor/editor.component';
import { CreateArticlePageComponent } from './create-article-page/create-article-page.component';
import { UpdateArticlePageComponent } from './update-article-page/update-article-page.component';

import { HtmlToPlaintextPipe } from './html-to-plaintext.pipe';
import{MatDialogModule} from '@angular/material/dialog';
import { DialogComponent } from './dialog/dialog.component';

import { EditarticleComponent } from './editarticle/editarticle.component';


@NgModule({
  declarations: [
    AppComponent,
    QueryCardComponent,
    SpamViewComponent,
    ArticleCardComponent,
    ReviewerPiechartComponent,
    QueriesComponent,
    NavbarComponent,
    FooterComponent,
    AdminNavbarComponent,
    LoginComponent,
    RegisterComponent,
    SpecificarticleComponent,
    ArticlesComponent,
    AdminPiechartComponent,
    LatestQueriesComponent,
    TrendingQueriespageComponent,
    LatestArticlepageComponent,
    TrendingArticlepageComponent,
 
    ProfileComponent,
    SpecificqueryComponent,
    RaisequeryComponent,
    ReportSpamComponent,
   
    EmployeePageComponent,
    SidenavComponent,
    HomepageComponent,
    
    SpamreportpageComponent,
    UserverificationpageComponent,
    ToreviewpageComponent,
    ArticalreviewedpageComponent,
  
    MyArticlesComponent,
    MyQueriesComponent,
    EditorComponent,
    CreateArticlePageComponent,
    UpdateArticlePageComponent, 
    ReviewerdashboardComponent,
    AdmindashboardComponent,
    ToreviewspecificpageComponent,
    ArticlereviewedspecificpageComponent,

    HtmlToPlaintextPipe,
      DialogComponent,
      EditarticleComponent,
   

  ],
  entryComponents:[DialogComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    DataTablesModule,
    HttpClientModule,
   BrowserAnimationsModule,
   MatButtonModule,
   MatDialogModule,

    NgxPaginationModule,
    MatCardModule,NgbCollapseModule,
    FormsModule,
    NgChartsModule,
    MatButtonModule,
    ScrollingModule,
    CKEditorModule,
    Ng2SearchPipeModule,
    
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
