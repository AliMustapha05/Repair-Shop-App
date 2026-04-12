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

        switch (error.status) {
          case 400:
            console.error('Bad Request:', error.error);
            break;

          case 401:
            console.error('Unauthorized');
            break;

          case 403:
            console.error('Forbidden');
            break;

          case 404:
            console.error('Not Found');
            break;

          case 409:
            console.error('Conflict');
            break;

          case 500:
            console.error('Server Error');
            break;

          default:
            console.error('Unexpected Error:', error);
            break;
        }

        return throwError(() => error);
      })
    );
  }
}