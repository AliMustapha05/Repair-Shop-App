import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmptyStateService {
  private _isEmpty = new BehaviorSubject<boolean>(false);
  public readonly isEmpty$ = this._isEmpty.asObservable();

  show() {
    this._isEmpty.next(true);
  }

  hide() {
    this._isEmpty.next(false);
  }
}