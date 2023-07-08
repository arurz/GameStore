import { InjectionToken } from '@angular/core';
import { ErrorInterceptor } from './error-interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

export const InterceptorsModule: { provide: InjectionToken<{}>, useClass: any, multi: boolean }[] = [
  { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
];
