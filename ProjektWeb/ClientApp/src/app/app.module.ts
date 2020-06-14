import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatButtonModule, MatChipsModule, MatDialogModule, MatGridListModule} from '@angular/material';
import { MainNavComponent } from './main-nav/main-nav.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material';
import { LoginComponent } from './login/login.component';
import {ReactiveFormsModule} from '@angular/forms';
import {fakeBackendProvider} from './_helpers/fake-backend-interceptor';
import {ErrorInterceptor} from './_helpers/error-interceptor';
import {JwtInterceptor} from './_helpers/jwt-interceptor';
import {AuthGuard} from './_helpers/auth.guard';
import { DialogComponent } from './dialog/dialog.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import {Role} from './_models/role';
import { HomeComponent } from './home/home.component';
import { MoviePanelComponent } from './home/movie-panel/movie-panel.component';
import { MatPaginatorModule, MatSortModule, MatProgressSpinnerModule} from '@angular/material';
import { MovieComponent } from './admin-panel/movie/movie.component';
import {MovieResolver} from './admin-panel/movie/movie-resolver';

@NgModule({
  declarations: [
    AppComponent,
    MainNavComponent,
    LoginComponent,
    DialogComponent,
    AdminPanelComponent,
    HomeComponent,
    MoviePanelComponent,
    MovieComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, canActivate: [AuthGuard] },
      { path: 'login', component: LoginComponent, pathMatch: 'full' },
      { path: 'admin', component: AdminPanelComponent, canActivate: [AuthGuard], data: {roles: [Role.Admin]} },
      { path: 'movie', component: MovieComponent, canActivate: [AuthGuard], data: {roles: [Role.Admin]} },
      {
        path: 'movie/:movieId',
        component: MovieComponent,
        resolve: {
          book: MovieResolver
        }
      },
      { path: 'home', redirectTo: '' },

      // otherwise redirect to home
      { path: '**', redirectTo: '/login' }
    ]),
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatButtonModule,
    LayoutModule,
    MatToolbarModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatCardModule,
    MatInputModule,
    MatDialogModule,
    MatGridListModule,
    MatChipsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },

    // provider used to create fake backend
    //fakeBackendProvider
  ],
  bootstrap: [AppComponent],
  entryComponents: [DialogComponent]
})
export class AppModule { }
