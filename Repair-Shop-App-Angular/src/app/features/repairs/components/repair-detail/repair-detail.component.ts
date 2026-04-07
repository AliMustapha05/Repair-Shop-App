import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RepairsService } from '../../../../core/services/repair.service';
import { RepairStatusHistoryDto } from '../../../../shared/models/repair.model';

import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';

@Component({
  selector: 'app-repair-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './repair-detail.component.html',
  styleUrls: ['./repair-detail.component.css']
})
export class RepairDetailComponent implements OnInit {

  history: RepairStatusHistoryDto[] = [];

  constructor(
    private service: RepairsService,
    private loader: LoaderService,
    private toast: ToastService
  ) {}

  ngOnInit(): void {
    this.loadHistory();
  }

  loadHistory() {
    this.loader.show();

    // later we pass real repairId
    this.service.getHistory(1).subscribe({
      next: (res) => {
        this.history = res;
        this.loader.hide();
      },
      error: () => {
        this.loader.hide();
        this.toast.showError('Failed to load history');
      }
    });
  }

}