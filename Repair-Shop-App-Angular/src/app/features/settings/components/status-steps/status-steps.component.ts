import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StatusStepsService } from '../../../../core/services/status-step.service';
import { StatusStepDto } from '../../../../shared/models/status-step.model';

import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';

import { EmptyStateComponent } from '../../../../shared/components/empty-state/empty-state.component';

@Component({
  selector: 'app-status-steps',
  standalone: true,
  imports: [CommonModule, EmptyStateComponent],
  templateUrl: './status-steps.component.html',
  styleUrls: ['./status-steps.component.css']
})
export class StatusStepsComponent implements OnInit {

  steps: StatusStepDto[] = [];

  constructor(
    private service: StatusStepsService,
    private loader: LoaderService,
    private toast: ToastService
  ) {}

  ngOnInit(): void {
    this.loadSteps();
  }

  loadSteps() {
    this.loader.show();

    this.service.getAll().subscribe({
      next: (res) => {
        this.steps = res;
        this.loader.hide();
      },
      error: () => {
        this.loader.hide();
        this.toast.showError('Failed to load status steps');
      }
    });
  }

}