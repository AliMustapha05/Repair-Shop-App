import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmptyStateService, EmptyStateData } from '../../services/empty-state.service';

@Component({
  selector: 'app-empty-state',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './empty-state.component.html',
  styleUrls: ['./empty-state.component.css']
})
export class EmptyStateComponent {

  state: EmptyStateData | null = null;

  constructor(private emptyService: EmptyStateService) {
    this.emptyService.state$.subscribe(data => {
      this.state = data;
    });
  }

  hide() {
    this.emptyService.hide();
  }
}