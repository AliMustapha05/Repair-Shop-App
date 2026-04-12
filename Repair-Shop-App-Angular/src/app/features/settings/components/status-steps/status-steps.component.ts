import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { StatusStepsService } from '../../../../core/services/status-step.service';
import { StatusStepDto, CreateStatusStepDto } from '../../../../shared/models/status-step.model';

import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';

@Component({
  selector: 'app-status-steps',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './status-steps.component.html',
  styleUrls: ['./status-steps.component.css']
})
export class StatusStepsComponent implements OnInit {

  steps: StatusStepDto[] = [];
  newStepName: string = '';

  constructor(
    private service: StatusStepsService,
    private loader: LoaderService,
    private toast: ToastService
  ) {}

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.loader.show();

    this.service.getAll().subscribe({
      next: (res: StatusStepDto[]) => {
        this.steps = res;
        this.loader.hide();
      },
      error: () => {
        this.loader.hide();
        this.toast.error('Failed to load status steps');
      }
    });
  }

  add() {
    if (!this.newStepName.trim()) return;

    const dto: CreateStatusStepDto = {
      name: this.newStepName
    };

    this.service.create(dto).subscribe({
      next: () => {
        this.toast.success('Status step added');
        this.newStepName = '';
        this.load();
      },
      error: () => {
        this.toast.error('Error adding status step');
      }
    });
  }

  deleteStatus(id: number): void {
    if (!id) {
      console.error('Invalid ID');
      return;
    }

    if (!confirm('Are you sure you want to delete this status?')) return;

    this.loader.show();

    this.service.delete(id).subscribe({
      next: () => {
        this.toast.success('Deleted successfully');
        this.load(); // reload list
        this.loader.hide();
      },
      error: (err) => {
        console.log('DELETE ERROR:', err);
        this.toast.error('Delete failed');
        this.loader.hide();
      }
    });
  }
}