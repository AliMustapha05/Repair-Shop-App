import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {

        if (error.status === 400) {
          console.error('Bad Request', error.error);
        }

        if (error.status === 404) {
          console.error('Not Found');
        }

        if (error.status === 409) {
          console.error('Conflict');
        }

        if (error.status === 500) {
          console.error('Server Error');
        }

        return throwError(() => error);
      })
    );
  }
}