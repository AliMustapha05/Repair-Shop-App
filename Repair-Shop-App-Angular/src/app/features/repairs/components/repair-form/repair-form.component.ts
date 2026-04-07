import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { RepairsService } from '../../../../core/services/repair.service';
import { CreateRepairDto } from '../../../../shared/models/repair.model';

import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';

@Component({
  selector: 'app-repair-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './repair-form.component.html',
  styleUrls: ['./repair-form.component.css']
})
export class RepairFormComponent {

  repair: CreateRepairDto = {
    deviceId: 0,
    issueDescription: ''
  };

  constructor(
    private service: RepairsService,
    private loader: LoaderService,
    private toast: ToastService
  ) {}

  submit() {
    this.loader.show();

    this.service.create(this.repair).subscribe({
      next: () => {
        this.loader.hide();
        this.toast.showSuccess('Repair created');
        this.resetForm();
      },
      error: () => {
        this.loader.hide();
        this.toast.showError('Create failed');
      }
    });
  }

  resetForm() {
    this.repair = {
      deviceId: 0,
      issueDescription: ''
    };
  }

}