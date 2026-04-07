import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConfirmDialogService {

  private _confirm = new Subject<boolean>();
  confirm$ = this._confirm.asObservable();

  open() {
    this._confirm.next(true); // trigger dialog open
  }

  confirm() {
    this._confirm.next(true);
  }

  cancel() {
    this._confirm.next(false);
  }
}