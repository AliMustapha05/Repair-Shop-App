import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfirmDialogService } from '../../services/confirm-dialog.service';

@Component({
  selector: 'app-confirm-dialog',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.css']
})
export class ConfirmDialogComponent {

  isVisible = false;
  title = '';
  message = '';

  constructor(private confirmService: ConfirmDialogService) {

    this.confirmService.open$.subscribe(data => {
      this.title = data.title;
      this.message = data.message;
      this.isVisible = true;
    });

  }

  confirm() {
    this.isVisible = false;
    this.confirmService.confirm();
  }

  cancel() {
    this.isVisible = false;
    this.confirmService.cancel();
  }
}