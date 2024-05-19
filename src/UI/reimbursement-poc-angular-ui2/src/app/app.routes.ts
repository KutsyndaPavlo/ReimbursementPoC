import { Routes } from '@angular/router';
  
import { ProgramAddComponent   } from './program/add/add.component';
import { ProgramListComponent } from './program/list/list.component';
import { ProgramEditComponent } from './program/edit/edit.component';
import { ProgramCancelComponent } from './program/cancel/cancel.component';
import { ProgramDeleteComponent } from './program/delete/delete.component';
import { exitGuard   } from './program/exit.guard';

import { AuthorizeGuard } from './../../src/api-authorization/authorize.guard';
  
export const routes: Routes = [
    { path: '', redirectTo: 'program/list', pathMatch: 'full' },
    { path: 'program/list', component: ProgramListComponent  },
    { path: 'program/add', component: ProgramAddComponent, canActivate: [AuthorizeGuard], canDeactivate: [exitGuard]},
    { path: 'program/:id/edit', component: ProgramEditComponent , canActivate: [AuthorizeGuard] },    
    { path: 'program/:id/cancel', component: ProgramCancelComponent , canActivate: [AuthorizeGuard] },
    { path: 'program/:id/delete', component: ProgramDeleteComponent, canActivate: [AuthorizeGuard]  }
  ];

 
  // import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
 
  // import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
  // import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
  // import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
  
  // @NgModule({
  //   declarations: [
  //     AppComponent,
  //     NavMenuComponent,
  //     HomeComponent,
  //     CounterComponent,
  //     FetchDataComponent
  //   ],
  //   imports: [
  //     BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
  //     HttpClientModule,
  //     FormsModule,
  //     ApiAuthorizationModule,
  //     RouterModule.forRoot([
  //       { path: '', component: HomeComponent, pathMatch: 'full' },
  //       { path: 'counter', component: CounterComponent },
  //       { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
  //     ])
  //   ],
  //   providers: [
  //     { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  //   ],
  //   bootstrap: [AppComponent]
  // })
  // export class AppModule { }
  
  //https://www.itsolutionstuff.com/post/angular-17-crud-application-tutorial-exampleexample.html?utm_content=cmp-true#google_vignette