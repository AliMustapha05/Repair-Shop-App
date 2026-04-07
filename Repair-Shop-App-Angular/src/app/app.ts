import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
// Shared Components
import { LayoutComponent } from './layout/layout/layout.component';
import { LoaderComponent } from './shared/components/loader/loader.component';
import { ToastComponent } from './shared/components/toast/toast.component';
import { ConfirmDialogComponent } from './shared/components/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    LayoutComponent,
    LoaderComponent,
    ToastComponent,
    ConfirmDialogComponent
  ],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class AppComponent { }