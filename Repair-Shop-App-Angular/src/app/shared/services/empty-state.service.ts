import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export interface EmptyStateData {
  title: string;
  message: string;
  icon?: string;
  actionText?: string;
}

@Injectable({
  providedIn: 'root'
})
export class EmptyStateService {

  private _state = new BehaviorSubject<EmptyStateData | null>(null);
  state$ = this._state.asObservable();

  show(data: EmptyStateData) {
    this._state.next(data);
  }

  hide() {
    this._state.next(null);
  }
}