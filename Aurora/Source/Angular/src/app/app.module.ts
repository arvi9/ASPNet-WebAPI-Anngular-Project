import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbCollapseModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { MatTableModule} from '@angular/material/table';
import { NgxPaginationModule } from 'ngx-pagination';
import { FormsModule } from '@angular/forms';
import { MatCardModule} from '@angular/material/card';
import { NgChartsModule} from 'ng2-charts';
import { DataTablesModule } from 'angular-datatables';
import { MatButtonModule} from '@angular/material/button';
import { CKEditorModule } from 'ckeditor4-angular';
import { AppComponent } from './app.component';
import { QueryCardComponent } from './query-card/query-card.component';
import { ButtonComponent } from './button/button.component';
import { ArticleCardComponent } from './article-card/article-card.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { ReviewerdashboardComponent } from './reviewerdashboard/reviewerdashboard.component';
import { AdmindashboardComponent } from './admindashboard/admindashboard.component';
import { ToreviewspecificpageComponent } from './toreviewspecificpage/toreviewspecificpage.component';
import { ArticlereviewedspecificpageComponent } from './articlereviewedspecificpage/articlereviewedspecificpage.component';

import { QueryFilterComponent } from './query-filter/query-filter.component';
import { QueriesComponent } from './queries/queries.component';
import { AdminNavbarComponent } from './admin-navbar/admin-navbar.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SpecificarticleComponent } from './specificarticle/specificarticle.component';
import { ArticlesComponent } from './articles/articles.component';
import { ArticleFilterComponent } from './article-filter/article-filter.component';
import { LatestQueriesComponent } from './latest-queries/latest-queries.component';
import { TrendingQueriespageComponent } from './trending-queriespage/trending-queriespage.component';
import { LatestArticlepageComponent } from './latest-articlepage/latest-articlepage.component';
import { TrendingArticlepageComponent } from './trending-articlepage/trending-articlepage.component';
import { PiechartComponent } from './piechart/piechart.component';
import { ProfileComponent } from './profile/profile.component';
import { SpecificqueryComponent } from './specificquery/specificquery.component';
import { RaisequeryComponent } from './raisequery/raisequery.component';
import { ReportSpamComponent} from './report-spam/report-spam.component';
import { MyArticlesComponent } from './my-articles/my-articles.component';
import { MyQueriesComponent } from './my-queries/my-queries.component';
import { TableComponent } from './table/table.component';

import { HomepageComponent } from './homepage/homepage.component';
import { ArticlecardhomeComponent } from './articlecardhome/articlecardhome.component';
import { QuerycardhomeComponent } from './querycardhome/querycardhome.component';

import { EmployeePageComponent } from './employee-page/employee-page.component';
import { SidenavComponent } from './sidenav/sidenav.component';
import { SpamreportpageComponent } from './spamreportpage/spamreportpage.component';
import { UserverificationpageComponent } from './userverificationpage/userverificationpage.component';
import { ToreviewpageComponent } from './toreviewpage/toreviewpage.component';
import { ArticalreviewedpageComponent } from './articalreviewedpage/articalreviewedpage.component';
import { ReviewersidenavComponent } from './reviewersidenav/reviewersidenav.component';
import {ScrollingModule} from '@angular/cdk/scrolling';
import { EditorComponent } from './editor/editor.component';
import { CreateArticlePageComponent } from './create-article-page/create-article-page.component';
import { UpdateArticlePageComponent } from './update-article-page/update-article-page.component'; 


@NgModule({
  declarations: [
    AppComponent,
    QueryCardComponent,
    ButtonComponent,
    ArticleCardComponent,
    QueryFilterComponent,
    QueriesComponent,
    NavbarComponent,
    FooterComponent,
    AdminNavbarComponent,
    LoginComponent,
    RegisterComponent,
    SpecificarticleComponent,
    ArticlesComponent,
    ArticleFilterComponent,
    LatestQueriesComponent,
    TrendingQueriespageComponent,
    LatestArticlepageComponent,
    TrendingArticlepageComponent,
    PiechartComponent,
    ProfileComponent,
    SpecificqueryComponent,
    RaisequeryComponent,
    ReportSpamComponent,
    TableComponent,
    EmployeePageComponent,
    SidenavComponent,
    HomepageComponent,
    ArticlecardhomeComponent,
    QuerycardhomeComponent,
    SpamreportpageComponent,
    UserverificationpageComponent,
    ToreviewpageComponent,
    ArticalreviewedpageComponent,
    ReviewersidenavComponent,
    MyArticlesComponent,
    MyQueriesComponent,
    EditorComponent,
    CreateArticlePageComponent,
    UpdateArticlePageComponent, 
    ReviewerdashboardComponent,
    AdmindashboardComponent,
    ToreviewspecificpageComponent,
    ArticlereviewedspecificpageComponent

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    DataTablesModule,
    HttpClientModule,
    MatTableModule,
    NgxPaginationModule,
    MatCardModule,NgbCollapseModule,
    FormsModule,
    NgChartsModule,
    MatButtonModule,
    ScrollingModule,
    CKEditorModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
