import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimations } from '@angular/platform-browser/animations';
  
import { provideHttpClient } from '@angular/common/http';
// import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideAnimations(), provideHttpClient()]
 };

// provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true 