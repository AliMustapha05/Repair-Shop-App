import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

export interface ConfirmDialogData {
  title: string;
  message: string;
}

@Injectable({
  providedIn: 'root'
})
export class ConfirmDialogService {

  private _open = new Subject<ConfirmDialogData>();
  open$ = this._open.asObservable();

  private _result = new Subject<boolean>();
  result$ = this._result.asObservable();

  open(data: ConfirmDialogData) {
    this._open.next(data);
  }

  confirm() {
    this._result.next(true);
  }

  cancel() {
    this._result.next(false);
  }
}