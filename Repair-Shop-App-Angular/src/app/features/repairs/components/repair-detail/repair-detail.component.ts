import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';

import { RepairsService } from '../../../../core/services/repair.service';
import { DeviceService } from '../../../../core/services/device.service';
import { StatusStepsService } from '../../../../core/services/status-step.service';

import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';

import { RepairDto } from '../../../../shared/models/repair.model';
import { DeviceDto } from '../../../../shared/models/device.model';
import { StatusStepDto } from '../../../../shared/models/status-step.model';
import { RepairStatusHistoryDto } from '../../../../shared/models/repair-status-history.model';

@Component({
  selector: 'app-repair-detail',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './repair-detail.component.html',
  styleUrls: ['./repair-detail.component.css']
})
export class RepairDetailComponent implements OnInit {

  repair!: RepairDto;
  history: RepairStatusHistoryDto[] = [];

  devices: DeviceDto[] = [];
  statuses: StatusStepDto[] = [];

  form: any;

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private repairsService: RepairsService,
    private deviceService: DeviceService,
    private statusService: StatusStepsService,
    private loader: LoaderService,
    private toast: ToastService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.form = this.fb.group({
      statusStepId: [0, Validators.required],
      note: ['']
    });

    this.loadData(id);
    this.loadStatuses();
  }

  loadData(id: number) {
    this.loader.show();

    this.repairsService.getById(id).subscribe({
      next: (res) => {
        this.repair = res;
        this.loader.hide();
      },
      error: () => {
        this.loader.hide();
        this.toast.error('Failed to load repair');
      }
    });

    this.repairsService.getHistory(id).subscribe({
      next: (res) => this.history = res
    });

    this.deviceService.getAll().subscribe({
      next: (res) => this.devices = res
    });

    this.statusService.getAll().subscribe({
      next: (res) => this.statuses = res
    });
  }

  loadStatuses() {
    this.statusService.getAll().subscribe({
      next: (res) => {
        this.statuses = res;
      },
      error: () => {
        this.toast.error('Failed to load statuses');
      }
    });
  }

  addStatus() {
    if (this.form.invalid) return;

    const dto = {
      statusStepId: this.form.value.statusStepId,
      note: this.form.value.note
    };

    this.repairsService.addStatus(this.repair.id, dto).subscribe({
      next: () => {
        this.toast.success('Status updated');
        this.form.reset();
        this.loadData(this.repair.id);
      },
      error: () => {
        this.toast.error('Failed to update status');
      }
    });
  }

  getDeviceName(id: number): string {
    const d = this.devices.find(x => x.id === id);
    return d ? `${d.brand} ${d.model}` : 'Unknown';
  }

  getOwnerName(id: number): string {
    const d = this.devices.find(x => x.id === id);
    return d ? d.ownerName : '-';
  }

  getStatusName(id: number): string {
    const s = this.statuses.find(x => x.id === id);
    return s ? s.name : 'Unknown';
  }
}