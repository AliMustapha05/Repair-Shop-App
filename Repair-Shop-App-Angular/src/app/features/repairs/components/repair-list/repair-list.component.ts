import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { RepairsService } from '../../../../core/services/repair.service';
import { RepairDto } from '../../../../shared/models/repair.model';

import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';

import { EmptyStateComponent } from '../../../../shared/components/empty-state/empty-state.component';

@Component({
  selector: 'app-repair-list',
  standalone: true,
  imports: [CommonModule, RouterModule, EmptyStateComponent],
  templateUrl: './repair-list.component.html',
  styleUrls: ['./repair-list.component.css']
})
export class RepairListComponent implements OnInit {

  repairs: RepairDto[] = [];

  constructor(
    private service: RepairsService,
    private loader: LoaderService,
    private toast: ToastService
  ) {}

  ngOnInit(): void {
    this.loadRepairs();
  }

  loadRepairs() {
    this.loader.show();

    this.service.getAll().subscribe({
      next: (res: RepairDto[]) => {
        this.repairs = res;
        this.loader.hide();
      },
      error: () => {
        this.loader.hide();
        this.toast.showError('Failed to load repairs');
      }
    });
  }

  delete(id: number) {
    if (!confirm('Are you sure?')) return;

    this.loader.show();

    this.service.delete(id).subscribe({
      next: () => {
        this.toast.showSuccess('Repair deleted');
        this.loadRepairs();
      },
      error: () => {
        this.loader.hide();
        this.toast.showError('Delete failed');
      }
    });
  }

}