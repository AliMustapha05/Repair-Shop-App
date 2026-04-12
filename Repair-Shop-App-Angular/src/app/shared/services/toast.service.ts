import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

export interface ToastMessage {
  message: string;
  type: 'success' | 'error' | 'info';
  duration?: number;
}

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  private _toast = new Subject<ToastMessage>();
  toast$ = this._toast.asObservable();

  show(message: string, type: 'success' | 'error' | 'info' = 'info', duration = 3000) {
    this._toast.next({ message, type, duration });
  }

  success(message: string) {
    this.show(message, 'success');
  }

  error(message: string) {
    this.show(message, 'error');
  }

  info(message: string) {
    this.show(message, 'info');
  }
}