import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastService, ToastMessage } from '../../services/toast.service';

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.css']
})
export class ToastComponent {

  toasts: (ToastMessage & { id: number })[] = [];
  counter = 0;

  constructor(private toastService: ToastService) {

    this.toastService.toast$.subscribe(toast => {
      const id = ++this.counter;

      this.toasts.push({ ...toast, id });

      setTimeout(() => {
        this.remove(id);
      }, toast.duration || 3000);
    });

  }

  remove(id: number) {
    this.toasts = this.toasts.filter(t => t.id !== id);
  }
}