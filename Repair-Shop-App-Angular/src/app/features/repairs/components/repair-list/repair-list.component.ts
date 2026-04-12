import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { RepairsService } from '../../../../core/services/repair.service';
import { DeviceService } from '../../../../core/services/device.service';
import { StatusStepsService } from '../../../../core/services/status-step.service';

import { RepairDto } from '../../../../shared/models/repair.model';
import { DeviceDto } from '../../../../shared/models/device.model';
import { StatusStepDto } from '../../../../shared/models/status-step.model';

import { LoaderService } from '../../../../shared/services/loader.service';
import { ToastService } from '../../../../shared/services/toast.service';

import { EmptyStateComponent } from '../../../../shared/components/empty-state/empty-state.component';

import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-repair-list',
  standalone: true,
  imports: [CommonModule, RouterModule, EmptyStateComponent],
  templateUrl: './repair-list.component.html',
  styleUrls: ['./repair-list.component.css']
})
export class RepairListComponent implements OnInit {

  repairs: RepairDto[] = [];
  devices: DeviceDto[] = [];
  statuses: StatusStepDto[] = [];

  isLoaded = false;

  constructor(
    private repairsService: RepairsService,
    private deviceService: DeviceService,
    private statusService: StatusStepsService,
    private loader: LoaderService,
    private toast: ToastService
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  // ✅ FIXED LOADING (NO BUGS, NO DOUBLE CLICK ISSUE)
  async loadData() {
    this.loader.show();

    try {
      const [repairs, devices, statuses] = await Promise.all([
        firstValueFrom(this.repairsService.getAll()),
        firstValueFrom(this.deviceService.getAll()),
        firstValueFrom(this.statusService.getAll())
      ]);

      this.repairs = repairs ?? [];
      this.devices = devices ?? [];
      this.statuses = statuses ?? [];

      this.isLoaded = true;

    } catch (err) {
      console.error(err);
      this.toast.error('Failed to load data');
    } finally {
      this.loader.hide();
    }
  }

  // =========================
  // DEVICE HELPERS
  // =========================
  getDeviceName(id: number): string {
    const d = this.devices.find(x => x.id === id);
    return d ? `${d.brand} ${d.model}` : 'Unknown';
  }

  getOwnerName(id: number): string {
    const d = this.devices.find(x => x.id === id);
    return d ? d.ownerName : '-';
  }

  getDeviceIcon(deviceId: number): string {
    const d = this.devices.find(x => x.id === deviceId);

    switch (d?.deviceTypeId) {
      case 1: return '📱';
      case 2: return '💻';
      case 3: return '📲';
      case 4: return '🖥️';
      default: return '📦';
    }
  }

  // =========================
  // STATUS (IMPORTANT FIX)
  // =========================
  getStatusClass(statusName: string | null | undefined): string {
    if (!statusName) return 'gray';

    const name = statusName.toLowerCase().trim();

    switch (name) {
      case 'ready':
      case 'ready for pickup':
        return 'green';

      case 'in progress':
        return 'orange';

      case 'waiting for parts':
        return 'yellow';

      case 'diagnosed':
        return 'blue';

      case 'cancelled':
        return 'red';

      default:
        return 'gray';
    }
  }
}