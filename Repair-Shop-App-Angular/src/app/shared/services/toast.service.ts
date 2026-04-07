import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

export interface ToastMessage {
  message: string;
  type: 'success' | 'error' | 'info';
}

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  private _toast = new Subject<ToastMessage>();
  public readonly toast$ = this._toast.asObservable();

  showSuccess(message: string) {
    this._toast.next({ message, type: 'success' });
  }

  showError(message: string) {
    this._toast.next({ message, type: 'error' });
  }

  showInfo(message: string) {
    this._toast.next({ message, type: 'info' });
  }
}