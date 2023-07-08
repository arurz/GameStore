import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    constructor(private toastr: ToastrService, private translateService: TranslateService,
        ) {

    }
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError((err: any) => {
                const error = 'toastrError.' + err.error.errorMessages[0].domainErrorCode;
                this.showError(error);
                return throwError(() => err);
            })
        );
    }

    showError(errorText: string, timeout?: number) {
        this.toastr.error(this.translateService.instant(errorText), undefined, { timeOut: (timeout === null || timeout === undefined) ? 5000 : timeout });
    }
}
