import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';
import { Observable, finalize } from 'rxjs';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  private totalRequests = 0;

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.totalRequests++;

    // TODO: show loader here

    return next.handle(req).pipe(
      finalize(() => {
        this.totalRequests--;

        if (this.totalRequests === 0) {
          // TODO: hide loader here
        }
      })
    );
  }
}